using SumatoVisionCore;
using SumatoVisionViewer.Properties;

namespace SumatoVisionViewer;
partial class SumatoVisionView
{
    const int BTN_HEIGHT = 25;

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

        pictureBox.Dock = DockStyle.Top; 
        pictureBox.Location = new Point(0, 0);
        pictureBox.Name = "pictureBox";
        pictureBox.Size = new Size(SetUpConfig.ResizeWidth, SetUpConfig.ResizeHeight);
        pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        pictureBox.TabIndex = 0;
        pictureBox.TabStop = false;
        pictureBox.Image = Resources.logo;

        startBtn.Dock = DockStyle.Bottom;
        startBtn.Location = new Point(0, SetUpConfig.ResizeHeight);
        startBtn.Name = "startBtn";
        startBtn.Size = new Size(SetUpConfig.ResizeWidth, BTN_HEIGHT);
        startBtn.TabIndex = 1;
        startBtn.Text = "Start";
        startBtn.UseVisualStyleBackColor = true;
        startBtn.Click += startBtn_Click;
        startBtn.Visible = false;
  
        fileVideoBtn.Dock = DockStyle.Bottom;
        fileVideoBtn.Location = new Point(0, SetUpConfig.ResizeHeight);
        fileVideoBtn.Name = "fileVideoBtn";
        fileVideoBtn.Size = new Size(SetUpConfig.ResizeWidth, BTN_HEIGHT);
        fileVideoBtn.TabIndex = 2;
        fileVideoBtn.Text = "Video File";
        fileVideoBtn.UseVisualStyleBackColor = true;
        fileVideoBtn.Click += fileVideoBtn_Click;
     
        cameraBtn.Dock = DockStyle.Bottom;
        cameraBtn.Location = new Point(0, SetUpConfig.ResizeHeight + BTN_HEIGHT);
        cameraBtn.Name = "cameraBtn";
        cameraBtn.Size = new Size(SetUpConfig.ResizeWidth, BTN_HEIGHT);
        cameraBtn.TabIndex = 3;
        cameraBtn.Text = "Camera";
        cameraBtn.UseVisualStyleBackColor = true;
        cameraBtn.Click += cameraBtn_Click;
   
        stopBtn.Dock = DockStyle.Bottom;
        stopBtn.Location = new Point(0, SetUpConfig.ResizeHeight);
        stopBtn.Name = "button1";
        stopBtn.Size = new Size(SetUpConfig.ResizeWidth, BTN_HEIGHT);
        stopBtn.TabIndex = 4;
        stopBtn.Text = "Stop";
        stopBtn.UseVisualStyleBackColor = true;
        stopBtn.Click += stopBtn_Click;
        stopBtn.Visible = false;
 
        ClientSize = new Size(SetUpConfig.ResizeWidth, SetUpConfig.ResizeHeight + + (BTN_HEIGHT * 2));
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
