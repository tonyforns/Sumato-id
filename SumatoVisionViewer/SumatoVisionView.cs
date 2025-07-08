using OpenCvSharp.Extensions;
using SumatoVisionCore;

namespace SumatoVisionViewer;
public partial class SumatoVisionView : Form
{
    private FrameQueue _queue;
    private FrameReader _reader;
    private Thread _displayThread;
    private bool _running = false;

    public SumatoVisionView()
    {
        InitializeComponent();
        InitSumatoVisualCore();
    }

    private void InitSumatoVisualCore()
    {
        _queue = new FrameQueue();
    }

    private void fileVideoBtn_Click(object sender, EventArgs e)
    {
        startBtn.Enabled = true;

        var dialog = new OpenFileDialog();

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            if (_reader != null) return;
            _reader = new FrameReader(_queue, new FileFrameSource(dialog.FileName));
        }
    }
    private void cameraBtn_Click(object sender, EventArgs e)
    {
        startBtn.Enabled = true;

        if (_reader != null) return;
        _reader = new FrameReader(_queue, new CameraFrameSoruce(0));
    }

    private void stopBtn_Click(object sender, EventArgs e)
    {
        _running = false;

        stopBtn.Visible = false;
        startBtn.Visible = true;
        cameraBtn.Visible = true;
        fileVideoBtn.Visible = true;
    }


    private void startBtn_Click(object sender, EventArgs e)
    {
        _reader.Start();
        StartStreaming();

        startBtn.Enabled = false;
        stopBtn.Visible = true;
        startBtn.Visible = false;
        cameraBtn.Visible = false;
        fileVideoBtn.Visible = false;
    }

    private void StartStreaming()
    {
        _running = true;
        if (_displayThread != null && _displayThread.IsAlive) return;

        _displayThread = new Thread(DisplayFrames);
        _displayThread.Start();
    }

    private void DisplayFrames()
    {
        while (_running)
        {
            var frame = _queue.PullQueue();
            var resized = frame.Resize(SetUpConfig.ResizeWidth, SetUpConfig.ResizeHeight);

            if (resized is MatFrame matFrame)
            {
                var bmp = BitmapConverter.ToBitmap(matFrame.RawMat);
                pictureBox.Invoke(() =>
                {
                    pictureBox.Image?.Dispose();
                    pictureBox.Image = bmp;
                });
            }
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _running = false;
        base.OnFormClosing(e);
    }

}
