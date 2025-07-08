using OpenCvSharp;

namespace SumatoId
{
    public class FileFrameSource : VideoCaptureFrameSource
    {
        public FileFrameSource(string filePath) : base(new VideoCapture(filePath))
        {
            if (!_capture.IsOpened())
            {
                throw new Exception("Failed to open File.");
            }
        }
    }
}
