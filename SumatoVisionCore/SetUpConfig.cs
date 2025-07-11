namespace SumatoVisionCore;

public static class SetUpConfig
{
    public static int frameDelayMs = 30;
    public static int ResizeWidth = 640;
    public static int ResizeHeight = 480;
    public static string ProcessingServiceUri = "ws://localhost:5001/ws?role=processing";
    public static string CaptureServiceUri = "ws://localhost:5001/ws?role=capture";
}
