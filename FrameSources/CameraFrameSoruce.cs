using OpenCvSharp;

namespace SumatoId
{
    public class CameraFrameSoruce : VideoCaptureFrameSource
    {
        public CameraFrameSoruce(int cameraIndex) : base(new VideoCapture(cameraIndex))
        {
            if (!_capture.IsOpened())
            {
                throw new Exception("Failed to open Camera.");
            }
        }
    }
}
