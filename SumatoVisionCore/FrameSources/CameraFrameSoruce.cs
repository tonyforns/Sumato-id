using OpenCvSharp;

namespace SumatoVisionCore;
public class CameraFrameSoruce : VideoCaptureFrameSource
{
    private int _cameraIndex;
    public CameraFrameSoruce(int cameraIndex) : base(new VideoCapture(cameraIndex))
    {
        _cameraIndex = cameraIndex;
        if (!_capture.IsOpened())
        {
           ErrorHandler.LogError($"Failed to open camera with index {_cameraIndex}.");
        }
    }

    internal override bool Reconnect()
    {
        try
        {
            _capture.Release();
            bool success = _capture.Open(_cameraIndex);
            _cameraIndex = (_cameraIndex + 1) % 10;
            return success;
        }
        catch (Exception ex)
        {
            ErrorHandler.LogError($"Failed to reconnect to camera with index {_cameraIndex}. Exception: {ex.Message}");
            return false;
        }
    }
}
