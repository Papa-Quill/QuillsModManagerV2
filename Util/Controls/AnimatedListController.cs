using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace QuillsModManagerV2.Util.Controls.AnimatedList
{
    public class AnimatedListController
    {
        protected readonly Timer _animTimer;
        protected readonly Stopwatch _animationClock = Stopwatch.StartNew();
        public readonly List<AnimatedItem> AnimatedItems = new List<AnimatedItem>();

        public readonly Dictionary<object, AnimatedItem> AnimationLookup =
            new Dictionary<object, AnimatedItem>();

        public readonly Stopwatch AnimationClock = Stopwatch.StartNew();

        protected Timer AnimationTimer;

        protected Panel Container;

        public AnimatedListController()
        {
            _animTimer = new Timer { Interval = 16 };

            _animTimer.Tick += AnimationTick;
        }

        public void Dispose()
        {
            _animTimer?.Dispose();
        }

        private void AnimationTick(object sender, EventArgs e)
        {
            UpdateAnimations();

            if (AnimatedItems.Count == 0)
                _animTimer.Stop();
        }

        protected virtual void UpdateAnimations()
        {
            bool animating = false;

            long now = _animationClock.ElapsedMilliseconds;

            foreach (AnimatedItem item in AnimatedItems)
            {
                long elapsed = now - item.AnimationStart - item.Delay;

                if (elapsed >= 0)
                {
                    float t = elapsed / 350f;

                    if (t > 1f)
                        t = 1f;

                    float eased = 1f - (float)Math.Pow(1f - t, 3);

                    item.Opacity = eased;
                    item.OffsetY = (1f - eased) * 8f;

                    if (t < 1f)
                        animating = true;
                }

                if (Math.Abs(item.CurrentY - item.TargetY) > 0.5f)
                {
                    item.CurrentY += (item.TargetY - item.CurrentY) * 0.25f;

                    animating = true;
                }
                else
                {
                    item.CurrentY = item.TargetY;
                }
            }

            if (!animating)
                _animTimer.Stop();

            OnAnimationUpdated();
        }

        protected virtual void OnAnimationUpdated()
        {
            AnimationUpdated?.Invoke();
        }

        public event Action AnimationUpdated;

        public void UpdateTargets(Func<object, int> indexResolver, int rowHeight)
        {
            foreach (var item in AnimatedItems)
            {
                int index = indexResolver(item.Key);

                item.TargetY = index * rowHeight;
            }

            Start();
        }

        protected virtual void ApplyItem(AnimatedItem item) { }

        public void Start()
        {
            if (!_animTimer.Enabled)
                _animTimer.Start();
        }

        public void RegisterItem(AnimatedItem item)
        {
            if (item == null)
                return;

            AnimatedItems.Add(item);

            if (item.Key != null)
                AnimationLookup[item.Key] = item;
        }

        public void ClearItems()
        {
            AnimatedItems.Clear();
            AnimationLookup.Clear();
        }

        protected Color WithOpacity(Color color, float opacity)
        {
            int alpha = (int)(255f * opacity);

            if (alpha < 0)
                alpha = 0;

            if (alpha > 255)
                alpha = 255;

            return Color.FromArgb(alpha, color);
        }
    }

    public class AnimatedItem
    {
        public object Key;
        public Panel Panel;

        public bool Visible = true;
        public bool Removing = false;

        public float Opacity = 1f;
        public float OffsetY = 0f;

        public float CurrentY = 0f;
        public float TargetY = 0f;

        public long AnimationStart = 0;
        public int Delay = 0;

        public bool RemoveAfterAnimation = false;

        public void UpdateOpacity(long now)
        {
            long elapsed = now - AnimationStart - Delay;

            if (elapsed < 0)
                return;

            float t = elapsed / 350f;

            if (t > 1f)
                t = 1f;

            float eased = 1f - (float)Math.Pow(1f - t, 3);

            Opacity = eased;
            OffsetY = (1f - eased) * 8f;
        }
    }

    public class AnimatedPanel : Panel
    {
        public float Opacity { get; set; } = 1f;

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Color c = Color.FromArgb((int)(255 * Opacity), BackColor);

            using (Brush b = new SolidBrush(c))
                e.Graphics.FillRectangle(b, ClientRectangle);
        }
    }
}
