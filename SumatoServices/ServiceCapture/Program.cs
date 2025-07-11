using SumatoVisionCore;

class Program
{
    static void Main(string[] args)
    {
        CaptureService captureService = new CaptureService(SetUpConfig.CaptureServiceUri, new CameraFrameSoruce(0));

        captureService.Start();
    }
}
