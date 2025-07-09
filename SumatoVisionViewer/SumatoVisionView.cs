using OpenCvSharp.Extensions;
using SumatoVisionCore;

namespace SumatoVisionViewer;
public partial class SumatoVisionView : Form
{

    private SumatoVisionController _controller;

    public SumatoVisionView()
    {
        _controller = new SumatoVisionController(DisplayFrames);
        InitializeComponent();
    }

    private void fileVideoBtn_Click(object sender, EventArgs e)
    {
        var dialog = new OpenFileDialog();
        if (dialog.ShowDialog() == DialogResult.OK)
            _controller.FileVideoBtn_Click(dialog.FileName);

        startBtn.Visible = true;
        SetVisbleCameraAndFileVideoBtns(false);
    }

    private void cameraBtn_Click(object sender, EventArgs e)
    {
        _controller.CameraBtn_Click();

        startBtn.Visible = true;
        SetVisbleCameraAndFileVideoBtns(false);
    }

    private void stopBtn_Click(object sender, EventArgs e)
    {
        _controller.StopBtn_Click();

        stopBtn.Visible = false;
        SetVisbleCameraAndFileVideoBtns(true);
    }


    private void startBtn_Click(object sender, EventArgs e)
    {
        _controller.StartBtn_Click();
        
        startBtn.Visible = false;
        stopBtn.Visible = true;
    }

    private void DisplayFrames(IFrame frame)
    {
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

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);
    }

    private void SetVisbleCameraAndFileVideoBtns(bool isVisible)
    {
        cameraBtn.Visible = isVisible;
        fileVideoBtn.Visible = isVisible;
    }


}
