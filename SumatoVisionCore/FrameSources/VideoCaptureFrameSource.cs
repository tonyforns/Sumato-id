using OpenCvSharp;

namespace SumatoVisionCore;
public abstract class VideoCaptureFrameSource : IFrameSource
{
    internal readonly VideoCapture _capture;
    public VideoCaptureFrameSource(VideoCapture capture)
    {
        _capture = capture;
    }

    public bool Read(out IFrame frame)
    {
        Mat mat = new Mat();
        if (!_capture.Read(mat) || mat.Empty())
        {
            frame = new MatFrame(null);
            return false;
        }

        frame = new MatFrame(mat);
        return true;
    }
}
