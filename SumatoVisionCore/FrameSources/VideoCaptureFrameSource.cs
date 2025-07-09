using OpenCvSharp;

namespace SumatoVisionCore;
public abstract class VideoCaptureFrameSource : IFrameSource
{
    internal VideoCapture _capture;
    public VideoCaptureFrameSource(VideoCapture capture)
    {
        _capture = capture;
    }

    public bool Read(out IFrame frame)
    {
        Mat mat = new Mat();
        bool success = _capture.Read(mat);
        if (!success || mat.Empty())
        {
            Reconnect();
            frame = null;
            return false;
        }
        frame = new MatFrame(mat);
        return success;
    }

    internal abstract bool Reconnect();

}
