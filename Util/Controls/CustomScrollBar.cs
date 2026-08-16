using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuillsModManagerV2.Util.Controls
{
    public class CustomVScrollBar : Control
    {
        private int _minimum = 0;
        private int _maximum = 100;
        private int _value = 0;
        private int _smallChange = 1;
        private int _largeChange = 10;
        private bool _isDragging = false;
        private int _dragStartY = 0;
        private int _dragStartValue = 0;

        public event ScrollEventHandler Scroll;

        public int Minimum
        {
            get => _minimum;
            set
            {
                _minimum = value;
                UpdateVisibility();
                Invalidate();
            }
        }

        public int Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                UpdateVisibility();
                Invalidate();
            }
        }

        public int Value
        {
            get => _value;
            set
            {
                _value = Math.Max(_minimum, Math.Min(_maximum, value));
                Invalidate();
                Scroll?.Invoke(this, new ScrollEventArgs(ScrollEventType.ThumbPosition, _value));
            }
        }

        public int SmallChange
        {
            get => _smallChange;
            set => _smallChange = value;
        }

        public int LargeChange
        {
            get => _largeChange;
            set
            {
                _largeChange = value;
                UpdateVisibility();
            }
        }

        public CustomVScrollBar()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint
                    | ControlStyles.UserPaint
                    | ControlStyles.OptimizedDoubleBuffer
                    | ControlStyles.Selectable,
                true
            );
            Width = 10;
            try
            {
                BackColor = Properties.Settings.Default.BGTertiary;
                ForeColor = Properties.Settings.Default.TextColor;
            }
            catch { }
            UpdateVisibility();
            try
            {
                Properties.Settings.Default.PropertyChanged += Settings_PropertyChanged;
            }
            catch { }
        }

        private void Settings_PropertyChanged(
            object sender,
            System.ComponentModel.PropertyChangedEventArgs e
        )
        {
            try
            {
                BackColor = Properties.Settings.Default.BGTertiary;
                ForeColor = Properties.Settings.Default.TextColor;
                Invalidate();
            }
            catch { }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateVisibility();
        }

        private void UpdateVisibility()
        {
            int range = _maximum - _minimum;

            bool canScroll = range > 0;

            if (Visible != canScroll)
                Visible = canScroll;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor = Cursors.Default;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);

            if (_maximum <= _minimum)
                return;

            int trackHeight = Math.Max(1, Height);

            int totalItems = (_maximum - _minimum) + _largeChange;
            if (totalItems <= 0)
                totalItems = 1;
            double visibleRatio = Math.Max(0.0, Math.Min(1.0, (double)_largeChange / totalItems));
            int thumbHeight = (int)Math.Round(visibleRatio * trackHeight);
            thumbHeight = Math.Max(8, thumbHeight);
            thumbHeight = Math.Min(thumbHeight, trackHeight);

            int positionRange = _maximum - _minimum;
            if (positionRange <= 0)
                positionRange = 1;

            int thumbY = (int)
                Math.Round(
                    ((_value - _minimum) / (double)positionRange) * (trackHeight - thumbHeight)
                );
            if (thumbY < 0)
                thumbY = 0;
            if (thumbY > trackHeight - thumbHeight)
                thumbY = trackHeight - thumbHeight;

            Rectangle thumbRect = new Rectangle(
                0,
                thumbY,
                Math.Max(0, Width),
                Math.Max(0, thumbHeight)
            );

            Color thumbColor = ForeColor;
            if (_isDragging)
            {
                thumbColor = Color.FromArgb(
                    (int)(ForeColor.R * 0.8),
                    (int)(ForeColor.G * 0.8),
                    (int)(ForeColor.B * 0.8)
                );
            }

            using (Brush thumbBrush = new SolidBrush(thumbColor))
            {
                e.Graphics.FillRectangle(thumbBrush, thumbRect);
            }

            using (Pen outlinePen = new Pen(Properties.Settings.Default.DetailColor))
            {
                e.Graphics.DrawRectangle(outlinePen, thumbRect);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();

            int trackHeight = Math.Max(1, Height);

            int denom = _maximum - _minimum + _largeChange;
            if (denom <= 0)
                denom = 1;

            int thumbHeight = (int)((double)_largeChange / denom * trackHeight);
            thumbHeight = Math.Max(10, thumbHeight);
            thumbHeight = Math.Min(thumbHeight, trackHeight);

            int range = _maximum - _minimum;
            if (range <= 0)
                range = 1;

            int thumbY = (int)((double)(_value - _minimum) / range * (trackHeight - thumbHeight));
            if (thumbY < 0)
                thumbY = 0;
            if (thumbY > trackHeight - thumbHeight)
                thumbY = trackHeight - thumbHeight;

            if (e.Y >= thumbY && e.Y <= thumbY + thumbHeight)
            {
                _isDragging = true;
                _dragStartY = e.Y;
                _dragStartValue = _value;
                Invalidate();
            }
            else
            {
                if (e.Y < thumbY)
                {
                    Value -= _largeChange;
                }
                else
                {
                    Value += _largeChange;
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_isDragging)
            {
                int deltaY = e.Y - _dragStartY;
                int trackHeight = Math.Max(1, Height);

                int totalItems = (_maximum - _minimum) + _largeChange;
                if (totalItems <= 0)
                    totalItems = 1;
                double visibleRatio = Math.Max(
                    0.0,
                    Math.Min(1.0, (double)_largeChange / totalItems)
                );
                int thumbHeight = (int)Math.Round(visibleRatio * trackHeight);
                thumbHeight = Math.Max(8, thumbHeight);
                thumbHeight = Math.Min(thumbHeight, trackHeight);

                int positionRange = _maximum - _minimum;
                if (positionRange <= 0)
                    positionRange = 1;

                int newValue =
                    _dragStartValue
                    + (int)(
                        (double)deltaY / Math.Max(1, (trackHeight - thumbHeight)) * positionRange
                    );
                Value = newValue;
                Update();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _isDragging = false;
            Invalidate();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (e.Delta > 0)
            {
                Value -= _smallChange;
            }
            else
            {
                Value += _smallChange;
            }
        }
    }
}

namespace QuillsModManagerV2.Util.Controls
{
    public class CustomHScrollBar : Control
    {
        private int _minimum = 0;
        private int _maximum = 100;
        private int _value = 0;
        private int _smallChange = 1;
        private int _largeChange = 10;
        private bool _isDragging = false;
        private int _dragStartX = 0;
        private int _dragStartValue = 0;

        public event ScrollEventHandler Scroll;

        public int Minimum
        {
            get => _minimum;
            set
            {
                _minimum = value;
                UpdateVisibility();
                Invalidate();
            }
        }

        public int Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                UpdateVisibility();
                Invalidate();
            }
        }

        public int Value
        {
            get => _value;
            set
            {
                _value = Math.Max(_minimum, Math.Min(_maximum, value));
                Invalidate();
                Scroll?.Invoke(this, new ScrollEventArgs(ScrollEventType.ThumbPosition, _value));
            }
        }

        public int SmallChange
        {
            get => _smallChange;
            set => _smallChange = value;
        }

        public int LargeChange
        {
            get => _largeChange;
            set
            {
                _largeChange = value;
                UpdateVisibility();
            }
        }

        public CustomHScrollBar()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint
                    | ControlStyles.UserPaint
                    | ControlStyles.OptimizedDoubleBuffer
                    | ControlStyles.Selectable,
                true
            );
            Height = 10;
            try
            {
                BackColor = Properties.Settings.Default.DetailColor;
                ForeColor = Properties.Settings.Default.DetailActive;
            }
            catch { }
            UpdateVisibility();
            try
            {
                Properties.Settings.Default.PropertyChanged += Settings_PropertyChanged;
            }
            catch { }
        }

        private void Settings_PropertyChanged(
            object sender,
            System.ComponentModel.PropertyChangedEventArgs e
        )
        {
            try
            {
                BackColor = Properties.Settings.Default.DetailColor;
                ForeColor = Properties.Settings.Default.DetailActive;
                Invalidate();
            }
            catch { }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateVisibility();
        }

        private void UpdateVisibility()
        {
            int range = _maximum - _minimum;

            bool canScroll = range > 0;

            if (Visible != canScroll)
                Visible = canScroll;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Cursor = Cursors.Default;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                e.Graphics.Clear(Properties.Settings.Default.DetailColor);
            }
            catch
            {
                e.Graphics.Clear(BackColor);
            }

            if (_maximum <= _minimum)
                return;

            int trackWidth = Math.Max(1, Width);

            int totalItems = _maximum - _minimum + 1;
            if (totalItems <= 0)
                totalItems = 1;
            double visibleRatio = Math.Max(0.0, Math.Min(1.0, (double)_largeChange / totalItems));
            int thumbWidth = (int)Math.Round(visibleRatio * trackWidth);
            thumbWidth = Math.Max(8, thumbWidth);
            thumbWidth = Math.Min(thumbWidth, trackWidth);

            int positionRange = _maximum - _minimum;
            if (positionRange <= 0)
                positionRange = 1;

            int thumbX = (int)
                Math.Round(
                    ((_value - _minimum) / (double)positionRange) * (trackWidth - thumbWidth)
                );
            if (thumbX < 0)
                thumbX = 0;
            if (thumbX > trackWidth - thumbWidth)
                thumbX = trackWidth - thumbWidth;

            Rectangle thumbRect = new Rectangle(
                thumbX,
                0,
                Math.Max(0, thumbWidth),
                Math.Max(0, Height)
            );

            Color thumbColor = Properties.Settings.Default.DetailActive;
            if (_isDragging)
            {
                try
                {
                    thumbColor = Color.FromArgb(
                        Math.Max(0, (int)(Properties.Settings.Default.DetailActive.R * 0.8)),
                        Math.Max(0, (int)(Properties.Settings.Default.DetailActive.G * 0.8)),
                        Math.Max(0, (int)(Properties.Settings.Default.DetailActive.B * 0.8))
                    );
                }
                catch
                {
                    thumbColor = ForeColor;
                }
            }

            using (Brush thumbBrush = new SolidBrush(thumbColor))
            {
                e.Graphics.FillRectangle(thumbBrush, thumbRect);
            }

            using (Pen outlinePen = new Pen(Properties.Settings.Default.DetailActive))
            {
                e.Graphics.DrawRectangle(outlinePen, thumbRect);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Focus();

            int trackWidth = Math.Max(1, Width);

            int denom = _maximum - _minimum + _largeChange;
            if (denom <= 0)
                denom = 1;

            int thumbWidth = (int)((double)_largeChange / denom * trackWidth);
            thumbWidth = Math.Max(10, thumbWidth);
            thumbWidth = Math.Min(thumbWidth, trackWidth);

            int range = _maximum - _minimum;
            if (range <= 0)
                range = 1;

            int thumbX = (int)((double)(_value - _minimum) / range * (trackWidth - thumbWidth));
            if (thumbX < 0)
                thumbX = 0;
            if (thumbX > trackWidth - thumbWidth)
                thumbX = trackWidth - thumbWidth;

            if (e.X >= thumbX && e.X <= thumbX + thumbWidth)
            {
                _isDragging = true;
                _dragStartX = e.X;
                _dragStartValue = _value;
                Invalidate();
            }
            else
            {
                if (e.X < thumbX)
                {
                    Value -= _largeChange;
                }
                else
                {
                    Value += _largeChange;
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_isDragging)
            {
                int deltaX = e.X - _dragStartX;
                int trackWidth = Math.Max(1, Width);

                int totalItems = _maximum - _minimum + 1;
                if (totalItems <= 0)
                    totalItems = 1;
                double visibleRatio = Math.Max(
                    0.0,
                    Math.Min(1.0, (double)_largeChange / totalItems)
                );
                int thumbWidth = (int)Math.Round(visibleRatio * trackWidth);
                thumbWidth = Math.Max(8, thumbWidth);
                thumbWidth = Math.Min(thumbWidth, trackWidth);

                int positionRange = _maximum - _minimum;
                if (positionRange <= 0)
                    positionRange = 1;

                int newValue =
                    _dragStartValue
                    + (int)(
                        (double)deltaX / Math.Max(1, (trackWidth - thumbWidth)) * positionRange
                    );
                Value = newValue;
                Update();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _isDragging = false;
            Invalidate();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (e.Delta > 0)
            {
                Value -= _smallChange;
            }
            else
            {
                Value += _smallChange;
            }
        }
    }
}
