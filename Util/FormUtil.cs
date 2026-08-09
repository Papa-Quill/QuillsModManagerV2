using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using QuillsModManagerV2.Properties;

namespace QuillsModManagerV2.Util
{
    public static class FormUtil
    {
        #region Variables
        public static int modalX,
            modalY;
        #endregion

        private class GlitchSlice
        {
            public Rectangle Destination;
            public Rectangle Source;
        }

        private class GlitchFrame
        {
            public List<GlitchSlice> Slices = new List<GlitchSlice>();
            public List<Point> Pixels = new List<Point>();
            public List<int> ScanOffsets = new List<int>();
        }

        #region Modal forms
        public static void ShowModalForm(Form baseForm, Form modalForm, bool showDialog = false)
        {
            bool resetTopMost = !baseForm.TopMost;
            baseForm.TopMost = true;

            Form modalBackground = new Form();
            SetModalBackgroundForm(modalBackground, baseForm);
            modalBackground.Show();

            Timer fadeOutTimer = new Timer { Interval = 10 };
            SetupModal(fadeOutTimer, modalForm, baseForm, modalBackground, resetTopMost);

            Timer fadeTimer = new Timer { Interval = 15 };
            SetModalBackgroundFormFadeTimer(fadeTimer, modalBackground, modalForm);
            fadeTimer.Start();

            if (showDialog)
                modalForm.ShowDialog();
            else
                modalForm.Show();
        }

        public static void SetModalBackgroundForm(Form modalBackgroundForm, Form baseForm)
        {
            modalBackgroundForm.StartPosition = FormStartPosition.Manual;
            modalBackgroundForm.Location = baseForm.Location;
            modalBackgroundForm.Size = baseForm.Size;
            modalBackgroundForm.FormBorderStyle = FormBorderStyle.None;
            modalBackgroundForm.Opacity = 0;
            modalBackgroundForm.BackColor = Color.Black;
            modalBackgroundForm.ShowInTaskbar = false;
            modalBackgroundForm.TopMost = true;
            modalBackgroundForm.Owner = baseForm;
            modalBackgroundForm.Text = "Modal Background";

            if (baseForm.Region != null)
                modalBackgroundForm.Region = baseForm.Region.Clone();
            else
                ApplyRoundedForm(modalBackgroundForm, Math.Max(Settings.Default.BorderRadius, 1));
        }

        public static void SetModalBackgroundFormFadeTimer(
            Timer timer,
            Form modalBackgroundForm,
            Form modalForm
        )
        {
            Action startSequence = null;

            startSequence = () =>
            {
                try
                {
                    modalForm.Update();

                    Rectangle finalBounds = modalForm.Bounds;

                    const int totalFrames = 12;

                    int frame = 0;
                    Random rand = new Random();

                    List<GlitchFrame> glitchFrames = new List<GlitchFrame>();

                    for (int i = 0; i < totalFrames; i++)
                    {
                        GlitchFrame glitchFrame = new GlitchFrame();

                        int y = 0;

                        while (y < modalForm.Height)
                        {
                            int sliceHeight = rand.Next(2, 6);

                            int shift = rand.NextDouble() < .65 ? rand.Next(-10, 11) : 0;

                            glitchFrame.Slices.Add(
                                new GlitchSlice
                                {
                                    Destination = new Rectangle(
                                        shift,
                                        y,
                                        modalForm.Width,
                                        sliceHeight
                                    ),
                                    Source = new Rectangle(0, y, modalForm.Width, sliceHeight)
                                }
                            );

                            y += sliceHeight;
                        }

                        for (int p = 0; p < 80; p++)
                        {
                            glitchFrame.Pixels.Add(
                                new Point(rand.Next(modalForm.Width), rand.Next(modalForm.Height))
                            );
                        }

                        for (int s = 0; s < modalForm.Height; s += 3)
                        {
                            glitchFrame.ScanOffsets.Add(rand.Next(-2, 3));
                        }

                        glitchFrames.Add(glitchFrame);
                    }

                    Bitmap cached = new Bitmap(modalForm.Width, modalForm.Height);
                    try
                    {
                        modalForm.DrawToBitmap(
                            cached,
                            new Rectangle(0, 0, cached.Width, cached.Height)
                        );
                    }
                    catch { }

                    BufferedPanel overlay = new BufferedPanel
                    {
                        Bounds = new Rectangle(0, 0, modalForm.Width, modalForm.Height),
                        BackColor = Color.Transparent,
                        Enabled = false,
                        Parent = modalForm
                    };
                    overlay.BringToFront();

                    overlay.Paint += (s, e) =>
                    {
                        if (frame >= totalFrames)
                            return;

                        Graphics g = e.Graphics;

                        float t = frame / (float)totalFrames;

                        g.CompositingMode = CompositingMode.SourceOver;
                        g.CompositingQuality = CompositingQuality.HighSpeed;
                        g.InterpolationMode = InterpolationMode.NearestNeighbor;
                        g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                        g.SmoothingMode = SmoothingMode.None;

                        GlitchFrame current = glitchFrames[Math.Min(frame, totalFrames - 1)];

                        foreach (GlitchSlice slice in current.Slices)
                        {
                            try
                            {
                                g.DrawImage(
                                    cached,
                                    slice.Destination,
                                    slice.Source,
                                    GraphicsUnit.Pixel
                                );
                            }
                            catch { }
                        }

                        int alpha = (int)(70f * (1f - frame / (float)totalFrames));

                        using (
                            Pen scanPen = new Pen(
                                Color.FromArgb(alpha, Settings.Default.DetailActive)
                            )
                        )
                        {
                            for (int i = 0; i < current.ScanOffsets.Count; i++)
                            {
                                int y = i * 3;
                                try
                                {
                                    g.DrawLine(
                                        scanPen,
                                        current.ScanOffsets[i],
                                        y,
                                        cached.Width + current.ScanOffsets[i],
                                        y
                                    );
                                }
                                catch { }
                            }
                        }

                        using (
                            SolidBrush pixelBrush = new SolidBrush(
                                Color.FromArgb(alpha / 2, Settings.Default.DetailActive)
                            )
                        )
                        {
                            foreach (Point p in current.Pixels)
                            {
                                try
                                {
                                    g.FillRectangle(pixelBrush, p.X, p.Y, 1, 1);
                                }
                                catch { }
                            }
                        }

                        using (
                            Pen border = new Pen(
                                Color.FromArgb(alpha + 30, Settings.Default.DetailActive),
                                2
                            )
                        )
                        {
                            try
                            {
                                g.DrawRectangle(border, 1, 1, cached.Width - 3, cached.Height - 3);
                            }
                            catch { }
                        }
                    };

                    modalForm.Opacity = 0;

                    timer.Interval = 15;

                    timer.Tick += (s, e) =>
                    {
                        if (modalForm.IsDisposed || modalBackgroundForm.IsDisposed)
                        {
                            try
                            {
                                timer.Stop();
                            }
                            catch { }
                            try
                            {
                                cached.Dispose();
                            }
                            catch { }
                            return;
                        }

                        frame++;

                        float t = frame / (float)totalFrames;
                        t = 1f - (float)Math.Pow(1f - t, 3);

                        modalBackgroundForm.Opacity = .5f * t;
                        modalForm.Opacity = 1;

                        overlay.Size = modalForm.ClientSize;
                        overlay.Invalidate();

                        if (frame >= totalFrames)
                        {
                            try
                            {
                                overlay.Dispose();
                            }
                            catch { }
                            try
                            {
                                cached.Dispose();
                            }
                            catch { }

                            modalForm.Bounds = finalBounds;
                            modalForm.Opacity = 1;
                            modalBackgroundForm.Opacity = .5f;

                            try
                            {
                                timer.Stop();
                            }
                            catch { }
                        }
                    };

                    try
                    {
                        if (!timer.Enabled)
                            timer.Start();
                    }
                    catch { }
                }
                catch { }
            };

            if (!modalForm.Visible)
            {
                EventHandler shown = null;
                shown = (s, e) =>
                {
                    try
                    {
                        modalForm.Shown -= shown;
                    }
                    catch { }
                    try
                    {
                        modalForm.BeginInvoke(new Action(() => startSequence()));
                    }
                    catch
                    {
                        startSequence();
                    }
                };
                try
                {
                    modalForm.Shown += shown;
                }
                catch
                { /* fallback to immediate */
                    startSequence();
                }
            }
            else
            {
                startSequence();
            }
        }

        public static void SetupModal(
            Timer fadeOutTimer,
            Form modalForm,
            Form baseForm,
            Form modalBackground,
            bool resetTopMost
        )
        {
            Action center = () =>
            {
                try
                {
                    modalForm.Location = new Point(
                        baseForm.Location.X + (baseForm.Width / 2) - (modalForm.Width / 2),
                        baseForm.Location.Y + (baseForm.Height / 2) - (modalForm.Height / 2)
                    );
                }
                catch { }
            };

            modalForm.Owner = modalBackground;
            modalForm.TopMost = true;
            modalForm.Opacity = 0;

            if (modalForm.Visible)
            {
                center();
            }
            else
            {
                EventHandler shown = null;
                shown = (s, e) =>
                {
                    try
                    {
                        modalForm.Shown -= shown;
                    }
                    catch { }
                    try
                    {
                        center();
                    }
                    catch { }
                };
                try
                {
                    modalForm.Shown += shown;
                }
                catch
                {
                    center();
                }
            }

            AttachButtonClickHandler(
                modalForm,
                "BtnClose",
                fadeOutTimer,
                baseForm,
                modalBackground,
                resetTopMost
            );
            AttachButtonClickHandler(
                modalForm,
                "BtnFinishSelection",
                fadeOutTimer,
                baseForm,
                modalBackground,
                resetTopMost
            );

            modalForm.FormClosing += (s, e) =>
            {
                if (!modalForm.Tag?.Equals("Closing") ?? true)
                {
                    e.Cancel = true;
                    modalForm.Tag = "Closing";

                    if (modalForm.Controls["BtnClose"] is Guna2ControlBox closeButton)
                    {
                        closeButton.PerformClick();
                    }
                    else
                    {
                        modalForm.Close();
                    }
                }
            };
        }

        private static void AttachButtonClickHandler(
            Form modalForm,
            string buttonName,
            Timer fadeOutTimer,
            Form baseForm,
            Form modalBackground,
            bool resetTopMost
        )
        {
            if (modalForm.Controls[buttonName] is Control button)
            {
                button.Click += (s, e) =>
                {
                    baseForm.Focus();
                    StartFadeOut(fadeOutTimer, baseForm, modalForm, modalBackground, resetTopMost);
                };
            }
        }

        private static void StartFadeOut(
            Timer fadeOutTimer,
            Form baseForm,
            Form modalForm,
            Form modalBackground,
            bool resetTopMost
        )
        {
            fadeOutTimer.Tick += (senderFadeOut, eFadeOut) =>
            {
                if (modalBackground != null && modalForm != null && modalBackground.Opacity > 0)
                {
                    modalBackground.Opacity -= 0.03;
                    modalForm.Opacity -= 0.03;
                }
                else
                {
                    fadeOutTimer.Stop();
                    modalBackground?.Dispose();
                    modalBackground = null;
                    baseForm.TopMost = false;
                    baseForm.Focus();
                    modalForm.Close();
                }
            };
            fadeOutTimer.Start();
        }
        #endregion

        #region Form button functions
        public static void Close(Form form)
        {
            foreach (var component in form.Controls.OfType<Component>())
            {
                if (component is Guna2BorderlessForm borderlessForm)
                    borderlessForm.Dispose();
            }
            ApplyRoundedForm(
                form,
                Settings.Default.BorderRadius > 0 ? Settings.Default.BorderRadius : 1
            );

            Timer fadeTimer = new Timer { Interval = 50 };
            fadeTimer.Tick += (sender, e) =>
            {
                if (form.Opacity <= 0)
                {
                    fadeTimer.Stop();
                    form.Close();
                }
                else
                    form.Opacity -= 0.3;
            };
            fadeTimer.Start();
            UpdateTheme.Unregister(form);
        }

        public static void CloseDialogOk(Form form, DialogResult dialog)
        {
            foreach (var component in form.Controls.OfType<Component>())
            {
                if (component is Guna2BorderlessForm borderlessForm)
                    borderlessForm.Dispose();
            }
            ApplyRoundedForm(
                form,
                Settings.Default.BorderRadius > 0 ? Settings.Default.BorderRadius : 1
            );

            try
            {
                form.DialogResult = dialog;
            }
            catch { }

            Timer fadeTimer = new Timer { Interval = 50 };
            fadeTimer.Tick += (sender, e) =>
            {
                if (form == null || form.IsDisposed)
                {
                    try
                    {
                        fadeTimer.Stop();
                    }
                    catch { }
                    return;
                }

                if (form.Opacity <= 0)
                {
                    try
                    {
                        fadeTimer.Stop();
                    }
                    catch { }
                    try
                    {
                        form.Close();
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        form.Opacity -= 0.3;
                    }
                    catch { }
                }
            };
            fadeTimer.Start();
            UpdateTheme.Unregister(form);
        }

        public static void MinimizeWindow()
        {
            Form form = Form.ActiveForm;
            if (form != null)
                form.WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region Helper functions
        public static void ShowForm<T>()
            where T : Form, new()
        {
            T form = new T();
            form.Show();
        }

        public static List<Form> GetAllOpenForms()
        {
            var openForms = new List<Form>();
            var col = Application.OpenForms;

            const int maxAttempts = 5;
            int attempt = 0;
            while (true)
            {
                try
                {
                    openForms.Clear();
                    for (int i = 0; i < col.Count; i++)
                    {
                        openForms.Add(col[i]);
                    }
                    return openForms;
                }
                catch (ArgumentOutOfRangeException)
                {
                    attempt++;
                    if (attempt >= maxAttempts)
                        throw;
                    continue;
                }
            }
        }

        public static void ApplyRoundedForm(Form form, int radius)
        {
            radius *= 3;
            form.FormBorderStyle = FormBorderStyle.None;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(form.Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(form.Width - radius, form.Height - radius, radius, radius, 0, 90);
                path.AddArc(0, form.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();

                form.Region = new Region(path);
            }
        }
        #endregion

        private class BufferedPanel : Panel
        {
            public BufferedPanel()
            {
                DoubleBuffered = true;
                SetStyle(
                    ControlStyles.UserPaint
                        | ControlStyles.AllPaintingInWmPaint
                        | ControlStyles.OptimizedDoubleBuffer,
                    true
                );
                UpdateStyles();
            }
        }
    }
}
