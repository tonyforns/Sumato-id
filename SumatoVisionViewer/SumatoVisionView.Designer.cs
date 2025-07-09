using SumatoVisionCore;

namespace SumatoVisionViewer;
partial class SumatoVisionView
{
    private System.ComponentModel.IContainer components = null;
    private PictureBox pictureBox;
    private Button startBtn;
    private Button fileVideoBtn;
    private Button cameraBtn;
    private Button stopBtn;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        pictureBox = new PictureBox();
        startBtn = new Button();
        fileVideoBtn = new Button();
        cameraBtn = new Button();
        stopBtn = new Button();
        ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
        SuspendLayout();
        // 
        // pictureBox
        // 
        pictureBox.Dock = DockStyle.Fill;
        pictureBox.Location = new Point(0, 0);
        pictureBox.Name = "pictureBox";
        pictureBox.Size = new Size(640, 455);
        pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        pictureBox.TabIndex = 0;
        pictureBox.TabStop = false;
        // 
        // startBtn
        // 
        startBtn.Dock = DockStyle.Bottom;
        startBtn.Location = new Point(0, 455);
        startBtn.Name = "startBtn";
        startBtn.Size = new Size(640, 25);
        startBtn.TabIndex = 1;
        startBtn.Text = "Start";
        startBtn.UseVisualStyleBackColor = true;
        startBtn.Click += startBtn_Click;
        startBtn.Visible = false;
        // 
        // fileVideoBtn
        // 
        fileVideoBtn.Dock = DockStyle.Bottom;
        fileVideoBtn.Location = new Point(0, 430);
        fileVideoBtn.Name = "fileVideoBtn";
        fileVideoBtn.Size = new Size(640, 25);
        fileVideoBtn.TabIndex = 2;
        fileVideoBtn.Text = "Video File";
        fileVideoBtn.UseVisualStyleBackColor = true;
        fileVideoBtn.Click += fileVideoBtn_Click;
        // 
        // cameraBtn
        // 
        cameraBtn.Dock = DockStyle.Bottom;
        cameraBtn.Location = new Point(0, 405);
        cameraBtn.Name = "cameraBtn";
        cameraBtn.Size = new Size(640, 25);
        cameraBtn.TabIndex = 3;
        cameraBtn.Text = "Camera";
        cameraBtn.UseVisualStyleBackColor = true;
        cameraBtn.Click += cameraBtn_Click;
        // 
        // stopBtn
        // 
        stopBtn.Dock = DockStyle.Bottom;
        stopBtn.Location = new Point(0, 380);
        stopBtn.Name = "button1";
        stopBtn.Size = new Size(640, 25);
        stopBtn.TabIndex = 4;
        stopBtn.Text = "Stop";
        stopBtn.UseVisualStyleBackColor = true;
        stopBtn.Click += stopBtn_Click;
        stopBtn.Visible = false;
        // 
        // Form1
        // 
        ClientSize = new Size(640, 480);
        Controls.Add(stopBtn);
        Controls.Add(cameraBtn);
        Controls.Add(fileVideoBtn);
        Controls.Add(pictureBox);
        Controls.Add(startBtn);
        Name = "SumatoVisionView";
        Text = "Video Viewer";
        ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
        ResumeLayout(false);
    }
}
