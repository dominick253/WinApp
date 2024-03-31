using System.Diagnostics;
using System.Timers;
using System.Windows.Forms;

namespace WinApp
{
    class FpsCounter
    {
        private Stopwatch stopwatch;
        private int frameCount;

        public FpsCounter()
        {
            stopwatch = new Stopwatch();
            frameCount = 0;
        }

        public void Start()
        {
            stopwatch.Start();
        }

        public void Frame()
        {
            frameCount++;
        }

        public void Update()
        {
            if (stopwatch.Elapsed.TotalSeconds >= 1)
            {
                stopwatch.Restart();
                frameCount = 0;
            }
        }

        public float GetFps()
        {
            float elapsedSeconds = (float)stopwatch.Elapsed.TotalSeconds;
            float fps = frameCount / elapsedSeconds;
            return fps;
        }
    }

    public partial class Form1 : Form
    {
        private FpsCounter fpsCounter;
        private Label fpsLabel;
        private System.Windows.Forms.Timer timer;

        public Form1()
        {
            InitializeComponent();

            this.TopMost = true;
            this.BackColor = System.Drawing.Color.Black;
            this.TransparencyKey = System.Drawing.Color.Black;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new System.Drawing.Point(0, 0);
            this.Size = new Size(100, 100);

            fpsCounter = new FpsCounter();
            fpsCounter.Start();

            fpsLabel = new Label();
            fpsLabel.Location = new System.Drawing.Point(20, 20);
            fpsLabel.BackColor = Color.Transparent; // make the label background transparent
            fpsLabel.ForeColor = Color.White;

            Controls.Add(fpsLabel);

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 500;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, System.EventArgs e)
        {
            Invalidate();
            fpsLabel.Text = $"FPS: {Math.Floor(fpsCounter.GetFps())}";
            fpsCounter = new FpsCounter();
            fpsCounter.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            fpsCounter.Frame();
            fpsLabel.Text = $"FPS: {Math.Floor(fpsCounter.GetFps())}";
            fpsCounter.Update();
        }
    }
}
