using System.Diagnostics;
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

            fpsCounter = new FpsCounter();
            fpsCounter.Start();

            fpsLabel = new Label();
            fpsLabel.Location = new System.Drawing.Point(10, 10);
            Controls.Add(fpsLabel);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            fpsCounter.Frame();
            fpsLabel.Text = $"FPS: {fpsCounter.GetFps()}";
            fpsCounter.Update();
        }
    }
}
