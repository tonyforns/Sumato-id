using SumatoVisionCore;

class Program
{
    static void Main(string[] args)
    {
        ProcessingService processingService = new ProcessingService(SetUpConfig.ProcessingServiceUri);

        processingService.Start();
    }
}
