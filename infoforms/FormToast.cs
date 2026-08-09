using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuillsModManagerV2.InfoForms
{
    public partial class FormToast : Form
    {
        #region Variables
        private readonly Timer CloseTimer = new Timer();
        private readonly Timer FadeOutTimer = new Timer();
        private static readonly List<FormToast> activeToasts = new List<FormToast>();

        private int remainingTime;
        private DateTime endTime;
        private double displayedRemaining;

        private int targetTop;
        private double animatedTop;
        private static readonly Timer AnimationTimer = new Timer();
        private static bool animationTimerInitialized = false;
        #endregion

        public FormToast()
        {
            InitializeComponent();
            InitializeComponentsAndEvents();
        }

        #region Hotkeys
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Escape))
            {
                Close();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.W))
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region Form base
        private void InitializeComponentsAndEvents()
        {
            int minValue = 2000 + GetTextWidth(Properties.Settings.Default.TxtNotif) * 15;
            ToastProgressbar.Maximum = Math.Max(minValue, 2000);
            remainingTime = Math.Max(minValue, 2000);
            displayedRemaining = remainingTime;
            endTime = DateTime.UtcNow.AddMilliseconds(remainingTime);

            ToastProgressbar.ProgressColor = Properties.Settings.Default.NotifColor;
            Width = GetTextWidth(Properties.Settings.Default.TxtNotif) + 55;
            ToastProgressbar.Width = Width;
            ToastBox.Width = Width;

            CloseTimer.Interval = 30;
            CloseTimer.Tick += TimerTick;
            CloseTimer.Start();

            FadeOutTimer.Interval = 40;
            FadeOutTimer.Tick += FadeOut;

            if (activeToasts.Any())
            {
                FormToast lastToast = activeToasts.Last();
                Location = new Point(10, lastToast.Bottom + 5);
            }
            else
            {
                Location = new Point(10, 10);
            }

            animatedTop = Top;
            targetTop = Top;

            activeToasts.Add(this);

            UpdateAllTargetsAndStartAnimation();

            FormClosed += Toast_FormClosed;
            TxtToast.Text = Properties.Settings.Default.TxtNotif;
        }
        #endregion

        #region Functions
        private static int GetTextWidth(string text)
        {
            using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                using (Font font = new Font("Segoe UI", 12))
                {
                    SizeF size = graphics.MeasureString(text, font);
                    return (int)size.Width;
                }
            }
        }

        private void Toast_FormClosed(object sender, FormClosedEventArgs e)
        {
            int index = activeToasts.IndexOf(this);
            if (index >= 0)
                activeToasts.RemoveAt(index);

            UpdateAllTargetsAndStartAnimation();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            int targetRemaining = (int)Math.Max(0, (endTime - DateTime.UtcNow).TotalMilliseconds);

            const double progressLerp = 0.22;
            displayedRemaining += (targetRemaining - displayedRemaining) * progressLerp;

            int displayValue = (int)
                Math.Round(Math.Min(ToastProgressbar.Maximum, Math.Max(0, displayedRemaining)));
            if (displayValue < ToastProgressbar.Minimum)
                displayValue = ToastProgressbar.Minimum;
            if (displayValue > ToastProgressbar.Maximum)
                displayValue = ToastProgressbar.Maximum;

            ToastProgressbar.Value = displayValue;
            remainingTime = targetRemaining;

            if (remainingTime <= 0)
            {
                CloseTimer.Stop();
                FadeOutTimer.Start();
            }
        }

        private void FadeOut(object sender, EventArgs e)
        {
            if (Opacity <= 0)
            {
                FadeOutTimer.Stop();
                Close();
            }
            else
            {
                Opacity = Math.Max(0, Opacity - 0.3);
            }
        }

        private static void UpdateAllTargetsAndStartAnimation()
        {
            for (int i = 0; i < activeToasts.Count; i++)
            {
                var toast = activeToasts[i];
                toast.targetTop = 10 + i * (toast.Height + 5);
                if (double.IsNaN(toast.animatedTop))
                    toast.animatedTop = toast.Top;
            }

            StartAnimationTimer();
        }

        private static void StartAnimationTimer()
        {
            if (!animationTimerInitialized)
            {
                AnimationTimer.Interval = 40;
                AnimationTimer.Tick += AnimationTimer_Tick;
                animationTimerInitialized = true;
            }
            if (!AnimationTimer.Enabled)
                AnimationTimer.Start();
        }

        private static void AnimationTimer_Tick(object sender, EventArgs e)
        {
            const double moveLerp = 0.22;
            bool anyAnimating = false;

            for (int i = 0; i < activeToasts.Count; i++)
            {
                var toast = activeToasts[i];

                toast.animatedTop += (toast.targetTop - toast.animatedTop) * moveLerp;

                int newTop = (int)Math.Round(toast.animatedTop);
                if (toast.Top != newTop)
                    toast.Top = newTop;

                if (Math.Abs(toast.targetTop - toast.animatedTop) > 0.5)
                    anyAnimating = true;
            }

            if (!anyAnimating)
                AnimationTimer.Stop();
        }
        #endregion

        #region Form interactions
        private void ToastBox_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ToastBox_MouseHover(object sender, EventArgs e)
        {
            CloseTimer.Enabled = false;
            FadeOutTimer.Enabled = false;
            Opacity = 1;
        }

        private void ToastBox_MouseLeave(object sender, EventArgs e)
        {
            if (remainingTime > 0)
            {
                endTime = DateTime.UtcNow.AddMilliseconds(remainingTime);
                CloseTimer.Enabled = true;
                FadeOutTimer.Enabled = false;
            }
            else
            {
                CloseTimer.Enabled = false;
                FadeOutTimer.Enabled = true;
            }
        }
        #endregion
    }
}
