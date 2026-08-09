using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Newtonsoft.Json;
using QuillsModManagerV2.Properties;
using QuillsModManagerV2.Util;
using QuillsModManagerV2.Util.Controls;
using QuillsModManagerV2.Util.Controls.ModManager;
using QuillsModManagerV2.Util.Debug;

namespace QuillsModManagerV2
{
    public partial class QMM : Form
    {
        private class Particle
        {
            public float X,
                Y,
                VX,
                VY;
            public Color Color;
            public float Size;
            public float Life;
            public int ShapeType;
            public Brush Brush;
            public float StartX,
                StartY;
            public float TargetX,
                TargetY;
            public float InitialVX,
                InitialVY;
            public float Age;
            public float TotalLife;
        }

        private Form _overlayForm;
        private bool _autoUndeployEnabled = false;
        private Process _gameProcess = null;
        private Timer _processWatcherTimer = null;
        private bool _gameProcessDetected = false;
        private Process _attachedGameProcess = null;
        private EventHandler _attachedExitedHandler = null;
        private int _activeDeploymentOps = 0;
        private int _pendingFirstPid = 0;
        private bool _awaitingSecondInstance = false;

        private enum ProfileStatus
        {
            NotDeployed,
            InProgress,
            Deployed
        }

        private ProfileStatus _currentProfileStatus = ProfileStatus.NotDeployed;
        private ProfileStatus _lastProfileStatus = ProfileStatus.NotDeployed;
        private Timer _profileStatusAnimTimer = null;
        private int _profileStatusAnimFrame = 0;
        private const int _profileStatusAnimFrames = 12;
        private Color _profileLabelBaseColor = Color.Empty;
        private Timer _attachedStartTimer = null;
        private bool _attachedProcessStarted = false;
        private System.Threading.CancellationTokenSource _unlockCts = null;
        public static bool DebugModeEnabled = Settings.Default.DebugMode;
        private bool bClosingCheck = false;
        private bool _labelVersionAnimating = false;

        public QMM()
        {
            if (Settings.Default.DebugMode)
            {
                Form debugConsole = new FormDebugConsole();
                debugConsole.Show();
                DebugUtil.Log(DebugLevel.INFO, DebugFilter.DEV, "Debug console initialized.");
                DebugUtil.Log(
                    DebugLevel.INFO,
                    DebugFilter.DEV,
                    "Appliaction version " + Settings.Default.Version + "."
                );
            }

            InitializeComponent();
            InitializeSettingsAndEvents();
            UpdateTheme.RegisterIcons(this);

            DeploymentManager.OperationStatusChanged += DeploymentManager_OperationStatusChanged;
            Text += " | " + Settings.Default.Version;

            var steamUser = Settings.Default.CurrentSteamUser;
            var profiles = ProfileManager.ListProfiles(steamUser);
            if (profiles.Count == 0)
            {
                ModBrowser.LoadModsFromFolder(Settings.Default.ModPath, applySavedEnabled: true);
                try
                {
                    var mods = ModBrowser.GetMods();
                    var bind = new System.ComponentModel.BindingList<ModEntry>(mods);
                    ProfileManager.SaveProfile(steamUser, "Default", bind);
                    ProfileManager.SetCurrentProfileName(steamUser, "Default");
                    try
                    {
                        LabelCurrentProfile.Text = "Default";
                        UpdateProfileDeploymentStatus();
                    }
                    catch { }
                }
                catch { }
            }
            else
            {
                ModBrowser.LoadModsFromFolder(Settings.Default.ModPath, applySavedEnabled: false);
                var last = ProfileManager.GetCurrentProfileName(steamUser);
                string toLoad = null;
                if (!string.IsNullOrWhiteSpace(last) && profiles.Contains(last))
                    toLoad = last;
                else
                    toLoad = profiles.First();
                try
                {
                    ModBrowser.ApplyProfile(toLoad);
                    ProfileManager.SetCurrentProfileName(steamUser, toLoad);
                    try
                    {
                        LabelCurrentProfile.Text = toLoad;
                        UpdateProfileDeploymentStatus();
                    }
                    catch { }
                }
                catch { }
            }
            try
            {
                LabelCurrentProfile.Click += LabelCurrentProfile_Click;
                UpdateModCountLabel();
            }
            catch { }
            try
            {
                LabelVersion.Click += LabelVersion_Click;
            }
            catch { }
            try
            {
                _profileLabelBaseColor = LabelCurrentProfile.ForeColor;
                try
                {
                    LabelCurrentProfile.Left = 32;
                }
                catch { }
                PanelProfileInfo.Paint += PanelProfileInfo_Paint;
            }
            catch { }
            if (Settings.Default.ApplicationMaximized)
            {
                Location = new Point(0, 10);
                BtnMaximize.PerformClick();
            }

            FormClosing += (s, e) =>
            {
                if (!bClosingCheck)
                {
                    e.Cancel = true;

                    if (Controls["BtnClose"] is Guna2ControlBox closeButton)
                    {
                        closeButton.PerformClick();
                        bClosingCheck = true;
                    }
                    else
                    {
                        bClosingCheck = true;
                        Close();
                    }
                }
            };
        }

        private void DeploymentManager_OperationStatusChanged(string opType, bool running)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    try
                    {
                        this.BeginInvoke(
                            (Action)(
                                () => DeploymentManager_OperationStatusChanged(opType, running)
                            )
                        );
                    }
                    catch { }
                    return;
                }

                if (running)
                    _activeDeploymentOps++;
                else
                    _activeDeploymentOps = Math.Max(0, _activeDeploymentOps - 1);

                try
                {
                    if (Settings.Default.FormShadows && QMMEProperties != null)
                    {
                        var desired =
                            _activeDeploymentOps > 0
                                ? Settings.Default.DetailColor
                                : Settings.Default.DetailActive;
                        try
                        {
                            QMMEProperties.ShadowColor = desired;
                        }
                        catch { }
                    }
                }
                catch { }

                try
                {
                    UpdateProfileDeploymentStatus();
                }
                catch { }
            }
            catch { }
        }

        private void LabelVersion_Click(object sender, EventArgs e)
        {
            try
            {
                var overlay = new Form
                {
                    StartPosition = FormStartPosition.Manual,
                    FormBorderStyle = FormBorderStyle.None,
                    ShowInTaskbar = false,
                    Owner = this,
                    Bounds = this.Bounds,
                    TopMost = true,
                    AllowTransparency = true,
                    BackColor = Color.Magenta,
                    TransparencyKey = Color.Magenta,
                    Opacity = 1
                };
                try
                {
                    var prop = typeof(Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic
                            | System.Reflection.BindingFlags.Instance
                    );
                    prop?.SetValue(overlay, true, null);
                }
                catch { }

                var particles = new List<Particle>();
                var rand = new Random();
                var brushPool = new Dictionary<int, Brush>();

                var themePalette = new Color[]
                {
                    Settings.Default.DetailActive,
                    Settings.Default.DetailColor,
                    Settings.Default.ButtonColor,
                    Settings.Default.TextColor,
                    Settings.Default.PlaceholderColor
                };

                Color VaryColor(Color c, double factor)
                {
                    int r = (int)Math.Max(0, Math.Min(255, c.R * factor));
                    int g = (int)Math.Max(0, Math.Min(255, c.G * factor));
                    int b = (int)Math.Max(0, Math.Min(255, c.B * factor));
                    return Color.FromArgb(c.A, r, g, b);
                }

                var labelScreen = LabelVersion.PointToScreen(
                    new Point(LabelVersion.Width / 2, LabelVersion.Height / 2)
                );
                var origin = overlay.PointToClient(labelScreen);

                for (int i = 0; i < 54; i++)
                {
                    var p = new Particle
                    {
                        X = origin.X + (float)(rand.NextDouble() * 40.0 - 20.0),
                        Y = origin.Y + (float)(rand.NextDouble() * 20.0 - 10.0)
                    };
                    double speed = 3.0 + rand.NextDouble() * 6.0;
                    double baseAngle = -60.0 * Math.PI / 180.0;
                    double spread = Math.PI / 6.0;
                    double angle = baseAngle + (rand.NextDouble() * 2.0 - 1.0) * spread;
                    p.InitialVX = (float)(Math.Cos(angle) * speed);
                    p.InitialVY = (float)(Math.Sin(angle) * speed);
                    p.Size = 6 + (float)(rand.NextDouble() * 12);
                    p.TotalLife = 60 + rand.Next(80);
                    p.Age = 0f;
                    if (i < 54 / 2)
                    {
                        var c = Settings.Default.DetailActive;
                        p.Color = Color.FromArgb(200, c.R, c.G, c.B);
                    }
                    else
                    {
                        var baseC = themePalette[rand.Next(themePalette.Length)];
                        double factor = 0.85 + rand.NextDouble() * 0.3;
                        var varied = VaryColor(baseC, factor);
                        p.Color = Color.FromArgb(200, varied.R, varied.G, varied.B);
                    }
                    p.ShapeType = i % 3;
                    int key = p.Color.ToArgb();
                    if (!brushPool.TryGetValue(key, out Brush pooled))
                    {
                        try
                        {
                            pooled = new SolidBrush(p.Color);
                        }
                        catch
                        {
                            pooled = new SolidBrush(Color.FromArgb(200, p.Color));
                        }
                        brushPool[key] = pooled;
                    }
                    p.Brush = pooled;
                    p.StartX = p.X;
                    p.StartY = p.Y;
                    float travelScale = 0.6f;
                    p.TargetX = p.StartX + p.InitialVX * p.TotalLife * travelScale;
                    p.TargetY = p.StartY + p.InitialVY * p.TotalLife * travelScale;
                    particles.Add(p);
                }

                int frame = 0;
                int duration = 120;

                overlay.Paint += (s, pe) =>
                {
                    var g = pe.Graphics;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

                    foreach (var p in particles)
                    {
                        if (p.Brush != null)
                        {
                            if (p.ShapeType == 0)
                            {
                                g.FillEllipse(
                                    p.Brush,
                                    p.X - p.Size / 2f,
                                    p.Y - p.Size / 2f,
                                    p.Size,
                                    p.Size
                                );
                            }
                            else if (p.ShapeType == 1)
                            {
                                var pts = new PointF[]
                                {
                                    new PointF(p.X, p.Y - p.Size / 2f),
                                    new PointF(p.X + p.Size / 2f, p.Y),
                                    new PointF(p.X, p.Y + p.Size / 2f),
                                    new PointF(p.X - p.Size / 2f, p.Y)
                                };
                                g.FillPolygon(p.Brush, pts);
                            }
                            else
                            {
                                float r = p.Size / 2f;
                                var pts = new PointF[]
                                {
                                    new PointF(p.X + 0, p.Y - r),
                                    new PointF(p.X + r * 0.4f, p.Y - r * 0.4f),
                                    new PointF(p.X + r, p.Y + 0),
                                    new PointF(p.X + r * 0.4f, p.Y + r * 0.4f),
                                    new PointF(p.X + 0, p.Y + r),
                                    new PointF(p.X - r * 0.4f, p.Y + r * 0.4f),
                                    new PointF(p.X - r, p.Y + 0),
                                    new PointF(p.X - r * 0.4f, p.Y - r * 0.4f)
                                };
                                g.FillPolygon(p.Brush, pts);
                            }
                        }

                        float lifeRatio =
                            (p.TotalLife > 0f)
                                ? Math.Max(0f, Math.Min(1f, p.Age / p.TotalLife))
                                : 1f;
                        float sparkle = (float)(0.5 + 0.5 * Math.Sin((1f - lifeRatio) * 6.0));
                        int glowA = (int)(
                            Math.Max(0, Math.Min(255, (1f - lifeRatio) * 200 * sparkle))
                        );
                        using (var sb = new SolidBrush(Color.FromArgb(glowA, 255, 255, 255)))
                        {
                            float sparkSize = Math.Max(1f, p.Size * 0.25f);
                            g.FillEllipse(
                                sb,
                                p.X - sparkSize / 2f,
                                p.Y - sparkSize / 2f,
                                sparkSize,
                                sparkSize
                            );
                        }
                    }
                };

                var timer = new Timer { Interval = 40 };
                timer.Tick += (s, ev) =>
                {
                    try
                    {
                        frame++;
                        for (int i = particles.Count - 1; i >= 0; i--)
                        {
                            var p = particles[i];
                            p.Age += 1f;
                            float lifeRatio = (p.TotalLife > 0f) ? p.Age / p.TotalLife : 1f;
                            float t = Math.Max(0f, Math.Min(1f, lifeRatio));
                            float ease = 1f - (1f - t) * (1f - t);
                            p.X = p.StartX + (p.TargetX - p.StartX) * ease;
                            p.Y = p.StartY + (p.TargetY - p.StartY) * ease;
                            if (p.Age >= p.TotalLife || p.Y > overlay.ClientSize.Height + 50)
                            {
                                particles.RemoveAt(i);
                            }
                        }
                        overlay.Invalidate();

                        if (frame > duration && particles.Count == 0)
                        {
                            timer.Stop();
                            try
                            {
                                foreach (var b in brushPool.Values)
                                    try
                                    {
                                        b.Dispose();
                                    }
                                    catch { }
                            }
                            catch { }
                            brushPool.Clear();
                            particles.Clear();
                            try
                            {
                                overlay.Close();
                            }
                            catch { }
                            try
                            {
                                overlay.Dispose();
                            }
                            catch { }
                            try
                            {
                                timer.Dispose();
                            }
                            catch { }
                            _labelVersionAnimating = false;
                        }
                    }
                    catch { }
                };

                if (_labelVersionAnimating)
                    return;
                _labelVersionAnimating = true;
                overlay.Show(this);
                timer.Start();
            }
            catch { }
        }

        private void UpdateProfileDeploymentStatus()
        {
            try
            {
                ProfileStatus status = ProfileStatus.NotDeployed;

                if (_activeDeploymentOps > 0)
                {
                    status = ProfileStatus.InProgress;
                }
                else
                {
                    try
                    {
                        string deploysDir = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            "QuillsModManagerV2",
                            "deployments"
                        );
                        string curPathFile = Path.Combine(deploysDir, "current_deploy.json");
                        if (File.Exists(curPathFile))
                        {
                            var cur = JsonConvert.DeserializeObject<dynamic>(
                                File.ReadAllText(curPathFile)
                            );
                            string root = (string)cur.Path;
                            if (!string.IsNullOrWhiteSpace(root))
                            {
                                string metaPath = Path.Combine(root, "meta.json");
                                if (File.Exists(metaPath))
                                {
                                    var metadata = JsonConvert.DeserializeObject<dynamic>(
                                        File.ReadAllText(metaPath)
                                    );
                                    if (metadata != null)
                                    {
                                        string currentProfile =
                                            ProfileManager.GetCurrentProfileName(
                                                Settings.Default.CurrentSteamUser
                                            );
                                        string prof = (string)metadata.ProfileName;
                                        if (
                                            !string.IsNullOrWhiteSpace(currentProfile)
                                            && !string.IsNullOrWhiteSpace(prof)
                                            && currentProfile == prof
                                        )
                                        {
                                            status = ProfileStatus.Deployed;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch { }
                }

                string current =
                    ProfileManager.GetCurrentProfileName(Settings.Default.CurrentSteamUser) ?? "";
                try
                {
                    if (LabelCurrentProfile.InvokeRequired)
                        LabelCurrentProfile.BeginInvoke(
                            (Action)(() => LabelCurrentProfile.Text = current)
                        );
                    else
                        LabelCurrentProfile.Text = current;
                }
                catch { }

                try
                {
                    if (status != _currentProfileStatus)
                    {
                        _lastProfileStatus = _currentProfileStatus;
                        _currentProfileStatus = status;
                        _profileStatusAnimFrame = 0;
                    }

                    if (_profileStatusAnimTimer == null)
                    {
                        _profileStatusAnimTimer = new Timer { Interval = 60 };
                        _profileStatusAnimTimer.Tick += (s, e) =>
                        {
                            try
                            {
                                _profileStatusAnimFrame++;
                                if (
                                    _currentProfileStatus != ProfileStatus.InProgress
                                    && _profileStatusAnimFrame > _profileStatusAnimFrames
                                )
                                {
                                    _profileStatusAnimTimer.Stop();
                                    _profileStatusAnimFrame = 0;
                                }
                                try
                                {
                                    if (
                                        PanelProfileInfo != null
                                        && !PanelProfileInfo.IsDisposed
                                        && !PanelProfileInfo.Disposing
                                    )
                                        PanelProfileInfo.Invalidate();
                                }
                                catch { }
                            }
                            catch { }
                        };
                    }

                    if (_currentProfileStatus == ProfileStatus.InProgress)
                    {
                        _profileStatusAnimTimer.Stop();
                        _profileStatusAnimTimer.Start();
                    }
                    else
                    {
                        _profileStatusAnimTimer.Stop();
                        _profileStatusAnimFrame = 0;
                        _profileStatusAnimTimer.Start();
                    }
                }
                catch { }
            }
            catch { }
        }

        private void PanelProfileInfo_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (!(sender is Control pnl))
                    return;

                var g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                int size = Math.Min(14, Math.Max(16, pnl.ClientSize.Height - 6));
                int x = 12;
                int y = Math.Max(8, (pnl.ClientSize.Height - size) / 2);
                var rect = new Rectangle(x, y, size, size);

                Color deployedColor = Settings.Default.DetailActive;
                Color inProgressColor = Color.FromArgb(230, 180, 60);
                Color notDeployedColor = Color.FromArgb(180, 60, 60);

                if (_currentProfileStatus == ProfileStatus.Deployed)
                {
                    var drawRect = rect;
                    drawRect.Inflate(-1, -1);
                    using (Brush b = new SolidBrush(deployedColor))
                        g.FillEllipse(b, drawRect);
                    using (Pen p = new Pen(Color.White, 2))
                    {
                        PointF p1 = new PointF(
                            drawRect.Left + drawRect.Width * 0.2f,
                            drawRect.Top + drawRect.Height * 0.55f
                        );
                        PointF p2 = new PointF(
                            drawRect.Left + drawRect.Width * 0.45f,
                            drawRect.Top + drawRect.Height * 0.8f
                        );
                        PointF p3 = new PointF(
                            drawRect.Left + drawRect.Width * 0.85f,
                            drawRect.Top + drawRect.Height * 0.25f
                        );
                        g.DrawLines(p, new[] { p1, p2, p3 });
                    }
                }
                else if (_currentProfileStatus == ProfileStatus.InProgress)
                {
                    var drawRect = rect;
                    drawRect.Inflate(-1, -1);
                    using (Pen p = new Pen(inProgressColor, 2))
                    {
                        g.DrawEllipse(p, drawRect);
                    }
                    using (Pen p2 = new Pen(inProgressColor, 2))
                    {
                        int start = (_profileStatusAnimFrame * 30) % 360;
                        int sweep = 90;
                        g.DrawArc(p2, drawRect, start, sweep);
                    }
                }
                else
                {
                    var drawRect = rect;
                    drawRect.Inflate(-1, -1);
                    using (Pen p = new Pen(notDeployedColor, 2))
                    {
                        g.DrawEllipse(p, drawRect);
                    }
                    using (Pen p2 = new Pen(notDeployedColor, 2))
                    {
                        g.DrawLine(
                            p2,
                            drawRect.Left + 3,
                            drawRect.Top + 3,
                            drawRect.Right - 3,
                            drawRect.Bottom - 3
                        );
                        g.DrawLine(
                            p2,
                            drawRect.Left + 3,
                            drawRect.Bottom - 3,
                            drawRect.Right - 3,
                            drawRect.Top + 3
                        );
                    }
                }
            }
            catch { }
        }

        #region Form base
        private void InitializeSettingsAndEvents()
        {
            if (
                Settings.Default.ApplicationSize.Width >= MinimumSize.Width
                && Settings.Default.ApplicationSize.Height >= MinimumSize.Height
            )
            {
                Size savedSize = Settings.Default.ApplicationSize;
                Rectangle workingArea = Screen.FromControl(this).WorkingArea;

                if (savedSize.Width > workingArea.Width || savedSize.Height > workingArea.Height)
                {
                    Size = MinimumSize;
                    return;
                }
                Size = new Size(
                    Math.Max(MinimumSize.Width, Math.Min(savedSize.Width, workingArea.Width)),
                    Math.Max(MinimumSize.Height, Math.Min(savedSize.Height, workingArea.Height))
                );
                Location = new Point(
                    workingArea.Right / 2 - Width / 2,
                    workingArea.Bottom / 2 - Height / 2
                );
            }

            SettingsChangedEvent(this, null);
            Settings.Default.SettingsLoaded += SettingsChangedEvent;
            SizeChanged += MainForm_SizePosUpdate;
            Move += MainForm_SizePosUpdate;

            #region SideBarButton event
            var SideBarButtons = new Control[]
            {
                BtnProfile,
                BtnOpenAppdata,
                BtnDeployMods,
                BtnUndeployMods,
                BtnStartGame,
                BtnSettings
            };
            foreach (var button in SideBarButtons)
            {
                button.MouseEnter += SideBarButton_MouseEnter;
            }
            #endregion
        }

        private void SideBarButton_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Control button)
                button.BringToFront();
        }

        private void SettingsChangedEvent(
            object sender,
            System.Configuration.SettingsLoadedEventArgs e
        )
        {
            var controlsToModify = new Control[]
            {
                BtnProfile,
                BtnOpenAppdata,
                BtnDeployMods,
                BtnUndeployMods,
                BtnStartGame,
                BtnSettings
            };
            UpdateTheme.Refresh(this, controlsToModify);
            QMMEProperties.ShadowColor = Settings.Default.DetailActive;
            if (!Settings.Default.FormShadows)
                QMMEProperties.ShadowColor = Color.Black;

            if (Settings.Default.BorderRadius == 0)
                BtnClose.Width = 36;
            else
                BtnClose.Width = 37;

            var SideBarButtons = new Control[] { };

            foreach (var button in SideBarButtons)
            {
                if (button is Guna2Button)
                {
                    Guna2Button gunaButton = button as Guna2Button;
                    gunaButton.CustomBorderColor = Settings.Default.DetailActive;
                    gunaButton.HoverState.BorderColor = Settings.Default.DetailActive;
                    gunaButton.HoverState.FillColor = UpdateTheme.AdjustBrightness(
                        Settings.Default.ButtonColor,
                        1.2f
                    );
                    gunaButton.DisabledState.BorderColor = Settings.Default.DetailColor;
                    gunaButton.DisabledState.FillColor = Settings.Default.ButtonColor;
                }
            }

            if (WindowState != FormWindowState.Maximized)
                QMMEProperties.BorderRadius = Settings.Default.BorderRadius * 3;
            QMMEProperties.BorderRadius =
                (WindowState == FormWindowState.Maximized) ? 0 : Settings.Default.BorderRadius * 3;
        }

        private void MainForm_SizePosUpdate(object sender, EventArgs e)
        {
            SettingsChangedEvent(this, null);
            ConstrainToVisibleArea(10);
        }

        private void ConstrainToVisibleArea(int minVisible)
        {
            if (minVisible < 0)
                minVisible = 0;

            if (WindowState == FormWindowState.Maximized)
                return;

            var scr = Screen.FromControl(this);
            Rectangle wa = scr.WorkingArea;

            int minX = wa.Left - Width + minVisible;
            int maxX = wa.Right - minVisible;
            if (minX > maxX)
                minX = maxX = wa.Left - (Width - minVisible);

            int minY = wa.Top - Height + minVisible;
            int maxY = wa.Bottom - minVisible;
            if (minY > maxY)
                minY = maxY = wa.Top - (Height - minVisible);

            int newX = Location.X;
            int newY = Location.Y;

            if (newX < minX)
                newX = minX;
            if (newX > maxX)
                newX = maxX;
            if (newY < minY)
                newY = minY;
            if (newY > maxY)
                newY = maxY;

            if (newX != Location.X || newY != Location.Y)
                Location = new Point(newX, newY);
        }
        #endregion

        private async void BtnClose_Click(object sender, EventArgs e)
        {
            if (bClosingCheck)
                return;

            if (_activeDeploymentOps > 0)
            {
                try
                {
                    ToastUtil.CreateToast("Waiting for deployment operations to complete...");
                }
                catch { }

                while (_activeDeploymentOps > 0)
                {
                    await Task.Delay(200);
                }
            }

            try
            {
                DeploymentManager.OperationStatusChanged -=
                    DeploymentManager_OperationStatusChanged;
            }
            catch { }

            if (Settings.Default.DebugMode)
            {
                if (
                    MessageBoxUtil.Show("Debug Mode", "Do you want to save the debug log?", true)
                    == DialogResult.Yes
                )
                {
                    DebugUtil.SaveLogs();
                }
            }
            if (DebugModeEnabled)
                Settings.Default.DebugMode = true;
            else
                Settings.Default.DebugMode = false;

            Settings.Default.ApplicationSize = Size;
            Settings.Default.ApplicationMaximized = (WindowState == FormWindowState.Maximized);
            Settings.Default.Save();
            try
            {
                if (_profileStatusAnimTimer != null)
                {
                    _profileStatusAnimTimer.Stop();
                    _profileStatusAnimTimer.Dispose();
                    _profileStatusAnimTimer = null;
                }
            }
            catch { }
            try
            {
                PanelProfileInfo.Paint -= PanelProfileInfo_Paint;
            }
            catch { }
            try
            {
                QMMEProperties.Dispose();
            }
            catch { }
            FormUtil.ApplyRoundedForm(
                this,
                Settings.Default.BorderRadius > 0 ? Settings.Default.BorderRadius : 1
            );

            List<Form> openForms = FormUtil.GetAllOpenForms();
            bClosingCheck = true;
            foreach (Form form in openForms)
            {
                FormUtil.Close(form);
            }
        }

        private void BtnToolTip_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Guna2Button button && button.AccessibleDescription is string tooltipText)
                ToolTipUtil.SetToolTip(button, tooltipText);
        }

        private void QMM_DoubleClick(object sender, EventArgs e)
        {
            BtnMaximize.PerformClick();
        }

        private void BtnOpenAppdata_Click(object sender, EventArgs e)
        {
            Process.Start(
                "explorer.exe",
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "QuillsModManagerV2"
                )
            );
        }

        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            ModBrowser.Search(TextBoxSearch.Text);
        }

        private async void BtnDeployMods_Click(object sender, EventArgs e)
        {
            try
            {
                if (_activeDeploymentOps > 0 || DeploymentManager.IsOperationRunning)
                {
                    try
                    {
                        ToastUtil.CreateToast(
                            Color.Orange,
                            "A deployment is already in progress. Please wait until it completes."
                        );
                    }
                    catch { }
                    return;
                }
            }
            catch { }

            var sw = Stopwatch.StartNew();
            var progress = new Progress<DeploymentManager.DeploymentProgress>(p =>
            {
                try
                {
                    if (LabelVersion.InvokeRequired)
                        LabelVersion.BeginInvoke(
                            (Action)(
                                () =>
                                {
                                    try
                                    {
                                        LabelVersion.Text = $"{p.Percent}% - {p.Message}";
                                    }
                                    catch { }
                                }
                            )
                        );
                    else
                        LabelVersion.Text = $"{p.Percent}% - {p.Message}";
                }
                catch { }
            });
            LabelVersion.Text = "0% - Starting deploy...";
            try
            {
                await ModBrowser.DeployModsAsync(progress);
                sw.Stop();
                LabelVersion.Text = $"Deploy completed in {sw.Elapsed.TotalSeconds:0.0}s!";
            }
            catch
            {
                sw.Stop();
                LabelVersion.Text = "Deploy failed.";
            }
            var t = new Timer { Interval = 3000 };
            t.Tick += (s, ev) =>
            {
                LabelVersion.Text = Settings.Default.Version;
                t.Stop();
            };
            t.Start();
        }

        private async void BtnUndeployMods_Click(object sender, EventArgs e)
        {
            try
            {
                if (_activeDeploymentOps > 0 || DeploymentManager.IsOperationRunning)
                {
                    try
                    {
                        ToastUtil.CreateToast(
                            Color.Orange,
                            "A deployment is already in progress. Please wait until it completes."
                        );
                    }
                    catch { }
                    return;
                }
            }
            catch { }

            var sw = Stopwatch.StartNew();
            var progress = new Progress<DeploymentManager.DeploymentProgress>(p =>
            {
                try
                {
                    if (LabelVersion.InvokeRequired)
                        LabelVersion.BeginInvoke(
                            (Action)(
                                () =>
                                {
                                    try
                                    {
                                        LabelVersion.Text = $"{p.Percent}% - {p.Message}";
                                    }
                                    catch { }
                                }
                            )
                        );
                    else
                        LabelVersion.Text = $"{p.Percent}% - {p.Message}";
                }
                catch { }
            });
            LabelVersion.Text = "0% - Starting undeploy...";
            try
            {
                await ModBrowser.UndeployModsAsync(progress);
                sw.Stop();
                LabelVersion.Text = $"Undeploy completed in {sw.Elapsed.TotalSeconds:0.0}s!";
            }
            catch
            {
                sw.Stop();
                LabelVersion.Text = "Undeploy failed.";
            }
            var t = new Timer { Interval = 3000 };
            t.Tick += (s, ev) =>
            {
                LabelVersion.Text = Settings.Default.Version;
                t.Stop();
            };
            t.Start();
        }

        public async Task StartGameAsync()
        {
            string exe = Path.Combine(Settings.Default.GamePath, "castle.exe");
            if (!File.Exists(exe))
                return;

            try
            {
                var procs = Process.GetProcessesByName("castle");
                foreach (var p in procs)
                {
                    try
                    {
                        string path = null;
                        try
                        {
                            path = p.MainModule.FileName;
                        }
                        catch { }
                        if (
                            !string.IsNullOrWhiteSpace(path)
                            && string.Equals(
                                Path.GetFullPath(path),
                                Path.GetFullPath(exe),
                                StringComparison.OrdinalIgnoreCase
                            )
                        )
                        {
                            try
                            {
                                ToastUtil.CreateToast("Game is already running.");
                            }
                            catch { }
                            return;
                        }
                    }
                    catch { }
                }
            }
            catch { }

            try
            {
                try
                {
                    if (DeploymentManager.IsOperationRunning)
                    {
                        DeploymentManager.QueuePostOperation(() =>
                        {
                            try
                            {
                                this.BeginInvoke(new Action(async () => await StartGameAsync()));
                            }
                            catch { }
                        });
                        try
                        {
                            ToastUtil.CreateToast(
                                Color.Orange,
                                "A deployment is in progress. Game launch will start after it completes."
                            );
                        }
                        catch { }
                        return;
                    }
                }
                catch { }

                var psi = new ProcessStartInfo(exe)
                {
                    WorkingDirectory = Settings.Default.GamePath
                };
                _gameProcess = Process.Start(psi);
                if (_gameProcess != null && Settings.Default.EfficiencyMode)
                {
                    EnterLockMode();
                    try
                    {
                        _pendingFirstPid = _gameProcess?.Id ?? 0;
                        _awaitingSecondInstance = true;
                        LogLock(
                            $"StartGameAsync: recorded first-launch PID={_pendingFirstPid}, awaiting second instance"
                        );
                    }
                    catch { }
                }
            }
            catch { }
        }

        private void EnterLockMode()
        {
            if (_overlayForm != null)
                return;
            _gameProcessDetected = false;
            try
            {
                this.SuspendLayout();
            }
            catch { }
            try
            {
                ModBrowser.Enabled = false;
                ModBrowser.Visible = false;
            }
            catch { }

            _overlayForm = new Form
            {
                StartPosition = FormStartPosition.Manual,
                Location = this.Location,
                Size = this.Size,
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.Black,
                Opacity = 0.6,
                ShowInTaskbar = false,
                Owner = this,
                TopMost = false
            };
            FormUtil.ApplyRoundedForm(_overlayForm, Math.Max(Settings.Default.BorderRadius, 1));

            var btn = new Guna2Button { Text = "Unlock Mod Manager", AutoSize = true };
            btn.Click += (s, e) => ExitLockMode();
            btn.UseTransparentBackground = true;
            btn.ForeColor = Settings.Default.TextColor;
            btn.FillColor = Settings.Default.ButtonColor;
            btn.BorderColor = Settings.Default.DetailColor;
            btn.BorderRadius = Settings.Default.BorderRadius;
            btn.BorderThickness = 1;
            btn.Animated = true;
            btn.Cursor = Cursors.Hand;
            var controlsToModify = new Control[] { btn };
            UpdateTheme.Refresh(this, controlsToModify);
            _overlayForm.Controls.Add(btn);
            btn.Location = new Point(
                (_overlayForm.Width - btn.Width) / 2,
                (_overlayForm.Height - btn.Height) / 2
            );

            _overlayForm.Show();
            this.Enabled = false;
            _processWatcherTimer = new Timer { Interval = 1000 };
            _processWatcherTimer.Tick += (s, e) =>
            {
                try
                {
                    var procs = Process.GetProcessesByName("castle");
                    string exePath = Path.Combine(Settings.Default.GamePath, "castle.exe");

                    var matches = new List<Process>();
                    foreach (var p in procs)
                    {
                        try
                        {
                            string path = null;
                            try
                            {
                                path = p.MainModule.FileName;
                            }
                            catch { }
                            if (
                                !string.IsNullOrWhiteSpace(path)
                                && string.Equals(
                                    Path.GetFullPath(path),
                                    Path.GetFullPath(exePath),
                                    StringComparison.OrdinalIgnoreCase
                                )
                            )
                            {
                                matches.Add(p);
                            }
                        }
                        catch { }
                    }

                    if (_awaitingSecondInstance)
                    {
                        if (_pendingFirstPid == 0)
                        {
                            if (matches.Count > 0)
                            {
                                try
                                {
                                    _pendingFirstPid = matches
                                        .OrderBy(pr => pr.StartTime)
                                        .First()
                                        .Id;
                                    LogLock(
                                        $"Watcher: recorded pending first PID={_pendingFirstPid}"
                                    );
                                }
                                catch { }
                            }
                        }
                        else
                        {
                            var second = matches.FirstOrDefault(p => p.Id != _pendingFirstPid);
                            if (second != null)
                            {
                                LogLock(
                                    $"Watcher: found second castle process PID={second.Id}, attaching"
                                );
                                try
                                {
                                    AttachToProcess(second);
                                }
                                catch { }
                                _gameProcessDetected = true;
                                _awaitingSecondInstance = false;
                                _pendingFirstPid = 0;
                                _processWatcherTimer.Stop();
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (matches.Count > 0)
                        {
                            var proc = matches.OrderBy(pr => pr.StartTime).First();
                            try
                            {
                                LogLock($"Watcher: found castle process PID={proc.Id}");
                                AttachToProcess(proc);
                                _gameProcessDetected = true;
                                _processWatcherTimer.Stop();
                            }
                            catch { }
                        }
                    }
                }
                catch { }
            };
            try
            {
                _processWatcherTimer.Start();
            }
            catch { }
            this.LocationChanged += QMM_LocationOrSizeChanged;
            this.SizeChanged += QMM_LocationOrSizeChanged;
        }

        private void ExitLockMode()
        {
            if (_overlayForm == null)
                return;
            try
            {
                _overlayForm.Close();
            }
            catch { }
            _overlayForm = null;
            try
            {
                _processWatcherTimer?.Stop();
                _processWatcherTimer?.Dispose();
                _processWatcherTimer = null;
            }
            catch { }
            try
            {
                DetachFromProcess();
            }
            catch { }
            try
            {
                ModBrowser.Visible = true;
                ModBrowser.Enabled = true;
            }
            catch { }
            try
            {
                this.ResumeLayout(true);
            }
            catch { }
            this.Enabled = true;
            this.LocationChanged -= QMM_LocationOrSizeChanged;
            this.SizeChanged -= QMM_LocationOrSizeChanged;
        }

        private void LogLock(string message)
        {
            try
            {
                DebugUtil.Log(DebugLevel.INFO, DebugFilter.PROCESSHOOK, message);
            }
            catch { }
        }

        private void AttachToProcess(Process p)
        {
            try
            {
                DetachFromProcess();
                _attachedGameProcess = p;
                LogLock($"AttachToProcess: attaching to PID={p?.Id}");
                _attachedExitedHandler = (s, e) =>
                {
                    try
                    {
                        LogLock($"Process Exited event: PID={p?.Id}");
                        try
                        {
                            _unlockCts?.Cancel();
                            _unlockCts?.Dispose();
                        }
                        catch { }
                        _unlockCts = new System.Threading.CancellationTokenSource();
                        var token = _unlockCts.Token;
                        Task.Run(
                            async () =>
                            {
                                try
                                {
                                    LogLock($"Unlock task: waiting 5s before checking replacement");
                                    await Task.Delay(5000, token);
                                    if (token.IsCancellationRequested)
                                    {
                                        LogLock("Unlock task: cancelled before check");
                                        return;
                                    }
                                    LogLock($"Unlock task: checking for replacement process");
                                    var procs = Process.GetProcessesByName("castle");
                                    string exePath = Path.Combine(
                                        Settings.Default.GamePath,
                                        "castle.exe"
                                    );
                                    Process replacement = null;
                                    foreach (var pp in procs)
                                    {
                                        try
                                        {
                                            string path = null;
                                            try
                                            {
                                                path = pp.MainModule.FileName;
                                            }
                                            catch { }
                                            if (
                                                !string.IsNullOrWhiteSpace(path)
                                                && string.Equals(
                                                    Path.GetFullPath(path),
                                                    Path.GetFullPath(exePath),
                                                    StringComparison.OrdinalIgnoreCase
                                                )
                                            )
                                            {
                                                replacement = pp;
                                                break;
                                            }
                                        }
                                        catch { }
                                    }
                                    if (replacement != null)
                                    {
                                        LogLock(
                                            $"Unlock task: found replacement PID={replacement.Id}, attaching instead of unlocking"
                                        );
                                        try
                                        {
                                            this.BeginInvoke(
                                                new Action(() => AttachToProcess(replacement))
                                            );
                                        }
                                        catch
                                        {
                                            AttachToProcess(replacement);
                                        }
                                        return;
                                    }
                                    if (_autoUndeployEnabled)
                                    {
                                        LogLock($"Unlock task: performing auto-undeploy");
                                        var progress =
                                            new Progress<DeploymentManager.DeploymentProgress>(pr =>
                                            {
                                                try
                                                {
                                                    LabelVersion.Text =
                                                        $"{pr.Percent}% - {pr.Message}";
                                                }
                                                catch { }
                                            });
                                        try
                                        {
                                            await ModBrowser.UndeployModsAsync(progress);
                                        }
                                        catch (Exception ex)
                                        {
                                            LogLock($"Unlock task: undeploy exception: {ex}");
                                        }
                                        LogLock($"Unlock task: auto-undeploy completed");
                                    }
                                    LogLock($"Unlock task: exiting lock mode now");
                                    try
                                    {
                                        this.BeginInvoke(new Action(() => ExitLockMode()));
                                    }
                                    catch
                                    {
                                        ExitLockMode();
                                    }
                                }
                                catch (OperationCanceledException)
                                {
                                    LogLock("Unlock task: cancelled");
                                }
                                catch (Exception ex)
                                {
                                    LogLock($"Unlock task exception: {ex}");
                                }
                            },
                            token
                        );
                        LogLock($"Process Exited: scheduled unlock task in 5s for PID={p?.Id}");
                    }
                    catch (Exception ex)
                    {
                        LogLock($"AttachToProcess Exited handler exception: {ex}");
                    }
                };
                try
                {
                    p.EnableRaisingEvents = true;
                }
                catch { }
                p.Exited += _attachedExitedHandler;
                _attachedProcessStarted = false;
                try
                {
                    _attachedStartTimer?.Stop();
                    _attachedStartTimer?.Dispose();
                }
                catch { }
                _attachedStartTimer = new Timer { Interval = 500 };
                int checks = 0;
                _attachedStartTimer.Tick += (ss, ee) =>
                {
                    checks++;
                    try
                    {
                        if (p.HasExited)
                        {
                            _attachedProcessStarted = false;
                            _attachedStartTimer.Stop();
                            LogLock(
                                $"Attached process PID={p.Id} exited before start confirmation"
                            );
                            return;
                        }
                        bool hasWindow = false;
                        try
                        {
                            hasWindow = p.MainWindowHandle != System.IntPtr.Zero;
                        }
                        catch { }
                        var runningLong = false;
                        try
                        {
                            runningLong = (DateTime.Now - p.StartTime).TotalMilliseconds > 2000;
                        }
                        catch { }
                        if (hasWindow || runningLong || checks >= 10)
                        {
                            _attachedProcessStarted = true;
                            _attachedStartTimer.Stop();
                            LogLock(
                                $"Attached process PID={p.Id} confirmed started (hasWindow={hasWindow}, runningLong={runningLong}, checks={checks})"
                            );
                        }
                    }
                    catch (Exception ex)
                    {
                        LogLock($"Attached start timer exception: {ex}");
                    }
                };
                _attachedStartTimer.Start();
            }
            catch { }
        }

        private void DetachFromProcess()
        {
            try
            {
                if (_attachedGameProcess != null && _attachedExitedHandler != null)
                {
                    try
                    {
                        _attachedGameProcess.Exited -= _attachedExitedHandler;
                    }
                    catch { }
                }
            }
            catch { }
            _attachedGameProcess = null;
            _attachedExitedHandler = null;
            _gameProcessDetected = false;
            try
            {
                _attachedStartTimer?.Stop();
                _attachedStartTimer?.Dispose();
                _attachedStartTimer = null;
            }
            catch { }
            try
            {
                _unlockCts?.Cancel();
                _unlockCts?.Dispose();
                _unlockCts = null;
            }
            catch { }
        }

        private void QMM_LocationOrSizeChanged(object sender, EventArgs e)
        {
            if (_overlayForm == null)
                return;
            _overlayForm.Location = this.Location;
            _overlayForm.Size = this.Size;
            FormUtil.ApplyRoundedForm(_overlayForm, Math.Max(Settings.Default.BorderRadius, 1));
        }

        private async void BtnStartGame_Click(object sender, EventArgs e)
        {
            if (_activeDeploymentOps != 0)
            {
                ToastUtil.CreateToast(
                    "Cannot start game while deployment operations are in progress."
                );
                return;
            }

            try
            {
                string exe = Path.Combine(Settings.Default.GamePath, "castle.exe");
                var procs = Process.GetProcessesByName("castle");
                foreach (var p in procs)
                {
                    try
                    {
                        string path = null;
                        try
                        {
                            path = p.MainModule.FileName;
                        }
                        catch { }
                        if (
                            !string.IsNullOrWhiteSpace(path)
                            && string.Equals(
                                Path.GetFullPath(path),
                                Path.GetFullPath(exe),
                                StringComparison.OrdinalIgnoreCase
                            )
                        )
                        {
                            try
                            {
                                EnterLockMode();
                            }
                            catch { }
                            try
                            {
                                AttachToProcess(p);
                            }
                            catch { }
                            return;
                        }
                    }
                    catch { }
                }
            }
            catch { }

            await StartGameAsync();
        }

        private void SideBarButton_Click(object sender, EventArgs e)
        {
            if (sender is Guna2Button button && button.Tag is Type formType)
            {
                Form form = (Form)Activator.CreateInstance(formType);
                if (button.RightToLeft == RightToLeft.Yes)
                    form.Show();
                else
                    FormUtil.ShowModalForm(this, form);
            }
            foreach (Guna2Button SideBarButton in PanelSideBar.Controls)
            {
                SideBarButton.Enabled = false;
                var timer = new Timer { Interval = 350 };
                timer.Tick += (s, args) =>
                {
                    SideBarButton.Enabled = true;
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
            }
        }

        public void UpdateModCountLabel()
        {
            try
            {
                LabelModCount.Text = $"Mods: {ModBrowser.GetMods().Count}";
            }
            catch { }
        }

        private void LabelCurrentProfile_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Form open in Application.OpenForms)
                {
                    if (
                        open is FormProfileManager existing
                        && !existing.IsDisposed
                        && !existing.Disposing
                    )
                    {
                        try
                        {
                            existing.BringToFront();
                            existing.Activate();
                        }
                        catch { }
                        return;
                    }
                }
            }
            catch { }

            var form = new FormProfileManager(
                () => new System.ComponentModel.BindingList<ModEntry>(ModBrowser.GetMods()),
                name =>
                {
                    try
                    {
                        ModBrowser.ApplyProfile(name);
                        LabelCurrentProfile.Text = name;
                        ProfileManager.SetCurrentProfileName(
                            Settings.Default.CurrentSteamUser,
                            name
                        );
                        try
                        {
                            UpdateProfileDeploymentStatus();
                        }
                        catch { }
                    }
                    catch { }
                }
            );
            FormUtil.ShowModalForm(this, form);
        }

        private void BtnMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                Size = new Size(925, 504);
                CenterToScreen();
            }
        }

        private void LabelMadeBy_Click(object sender, EventArgs e)
        {
            Process.Start(
                new ProcessStartInfo("https://github.com/Papa-Quill/QuillsModManagerV2")
                {
                    UseShellExecute = true
                }
            );
        }
    }
}
