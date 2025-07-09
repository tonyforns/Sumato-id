using OpenCvSharp;

namespace SumatoVisionCore;
public class FileFrameSource : VideoCaptureFrameSource
{
    private readonly string _filePath;
    public FileFrameSource(string filePath) : base(new VideoCapture(filePath))
    {
        _filePath = filePath;
        if (!_capture.IsOpened())
        {
            ErrorHandler.LogError($"Failed to open video file: {_filePath}.");
        }
    }

    internal override bool Reconnect()
    {
        try
        {
            _capture.Release();
            return _capture.Open(_filePath);
        }
        catch (Exception ex)
        {
            ErrorHandler.LogError($"Failed to reconnect to video file {_filePath}. Exception: {ex.Message}");
            return false;
        }
    }
}
