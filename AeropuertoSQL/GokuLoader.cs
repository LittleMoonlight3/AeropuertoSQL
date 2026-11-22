using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace AeropuertoSQL
{
    internal sealed class GokuLoader : Form
    {
        private readonly Timer clock = new Timer();
        private readonly Image logo;
        private readonly Button pauseButton;
        private readonly Button replayButton;
        private int elapsed;
        private bool paused;

        public GokuLoader()
        {
            Text = "Cargando";
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(24, 43, 53);
            DoubleBuffered = true;
            KeyPreview = true;

            Stream stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("AeropuertoSQL.Assets.goku-logo.jpg");
            if (stream == null)
                throw new InvalidOperationException("No se encontró el recurso del logotipo de Goku.");

            using (stream)
                logo = Image.FromStream(stream);

            pauseButton = MakeButton("Pausar", "Pausar o reanudar la animación");
            replayButton = MakeButton("Repetir", "Reiniciar la animación");
            pauseButton.Click += (s, e) => TogglePause();
            replayButton.Click += (s, e) => Replay();
            Controls.Add(pauseButton);
            Controls.Add(replayButton);

            clock.Interval = 16;
            clock.Tick += (s, e) =>
            {
                elapsed += clock.Interval;
                if (elapsed >= 5600)
                {
                    elapsed = 5600;
                    clock.Stop();
                    Close();
                }
                Invalidate();
            };

            Resize += (s, e) => PositionButtons();
            Shown += (s, e) => { PositionButtons(); clock.Start(); };
            FormClosed += (s, e) => { clock.Stop(); logo.Dispose(); };
        }

        private Button MakeButton(string caption, string accessibleName)
        {
            Button button = new Button();
            button.Text = caption;
            button.AccessibleName = accessibleName;
            button.AutoSize = true;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = Color.FromArgb(35, 57, 68);
            button.ForeColor = Color.FromArgb(250, 246, 249);
            button.Font = new Font("Segoe UI", 9F);
            button.Padding = new Padding(12, 6, 12, 6);
            button.Cursor = Cursors.Hand;
            button.TabStop = true;
            return button;
        }

        private void PositionButtons()
        {
            replayButton.Location = new Point(ClientSize.Width - replayButton.Width - 24,
                ClientSize.Height - replayButton.Height - 22);
            pauseButton.Location = new Point(replayButton.Left - pauseButton.Width - 8,
                replayButton.Top);
        }

        private void TogglePause()
        {
            paused = !paused;
            if (paused)
                clock.Stop();
            else
                clock.Start();

            pauseButton.Text = paused ? "Reanudar" : "Pausar";
            PositionButtons();
        }

        private void Replay()
        {
            elapsed = 0;
            paused = false;
            pauseButton.Text = "Pausar";
            PositionButtons();
            clock.Start();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(BackColor);

            float maxWidth = Math.Min(ClientSize.Width * 0.68f, 760f);
            float width = Math.Min(maxWidth, ClientSize.Height * 1.75f);
            float height = width * logo.Height / logo.Width;
            RectangleF target = new RectangleF(
                (ClientSize.Width - width) / 2f,
                (ClientSize.Height - height) / 2f - 12f,
                width, height);

            if (elapsed < 3100)
            {
                DrawConstructingOutline(g, target, elapsed / 2650f);
            }
            else
            {
                float opacity;
                if (elapsed < 3800)
                    opacity = Ease((elapsed - 3100f) / 700f);
                else if (elapsed < 5000)
                    opacity = 1f;
                else
                    opacity = 1f - Ease((elapsed - 5000f) / 600f);

                DrawLogo(g, target, opacity);
            }
        }

        private void DrawConstructingOutline(Graphics g, RectangleF r, float progress)
        {
            PointF[] points = new PointF[]
            {
                P(r, .49f,.02f), P(r,.58f,.12f), P(r,.77f,.04f),
                P(r,.70f,.22f), P(r,.95f,.16f), P(r,.83f,.36f),
                P(r,.99f,.48f), P(r,.83f,.59f), P(r,.82f,.77f),
                P(r,.65f,.91f), P(r,.50f,.99f), P(r,.35f,.91f),
                P(r,.18f,.77f), P(r,.17f,.59f), P(r,.01f,.48f),
                P(r,.17f,.36f), P(r,.05f,.16f), P(r,.30f,.22f),
                P(r,.23f,.04f), P(r,.42f,.12f), P(r,.49f,.02f)
            };

            float total = 0;
            for (int i = 1; i < points.Length; i++)
                total += Distance(points[i - 1], points[i]);

            float remaining = total * Math.Max(0, Math.Min(1, progress));
            using (Pen pen = new Pen(Color.FromArgb(245, 237, 222), Math.Max(2f, r.Width * .006f)))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                pen.LineJoin = LineJoin.Round;
                for (int i = 1; i < points.Length && remaining > 0; i++)
                {
                    float length = Distance(points[i - 1], points[i]);
                    float portion = Math.Min(1f, remaining / length);
                    PointF end = new PointF(
                        points[i - 1].X + (points[i].X - points[i - 1].X) * portion,
                        points[i - 1].Y + (points[i].Y - points[i - 1].Y) * portion);
                    g.DrawLine(pen, points[i - 1], end);
                    remaining -= length;
                }
            }
        }

        private PointF P(RectangleF r, float x, float y)
        {
            return new PointF(r.Left + r.Width * x, r.Top + r.Height * y);
        }

        private float Distance(PointF a, PointF b)
        {
            float x = b.X - a.X;
            float y = b.Y - a.Y;
            return (float)Math.Sqrt(x * x + y * y);
        }

        private float Ease(float value)
        {
            value = Math.Max(0f, Math.Min(1f, value));
            return .5f - .5f * (float)Math.Cos(Math.PI * value);
        }

        private void DrawLogo(Graphics g, RectangleF target, float opacity)
        {
            using (ImageAttributes attributes = new ImageAttributes())
            {
                ColorMatrix matrix = new ColorMatrix();
                matrix.Matrix33 = Math.Max(0f, Math.Min(1f, opacity));
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                g.DrawImage(logo, Rectangle.Round(target), 0, 0, logo.Width, logo.Height,
                    GraphicsUnit.Pixel, attributes);
            }
        }
    }
}