namespace SumatoVisionCore;
internal class ProcessingService
{
    private FrameReader _frameReader;
    private FrameProcessor _frameProcessor;

    public ProcessingService(string serverUri)
    {
        var queue = new FrameQueue();

        IFrameSource frameSource = new ServerFrameSource(serverUri);
        _frameReader = new FrameReader(queue, frameSource);

        _frameProcessor = new FrameProcessor(queue, (frame) =>
        {
            if(!frame.IsEmpty)
            {
                frame.Resize(SetUpConfig.ResizeWidth, SetUpConfig.ResizeHeight);
                Console.WriteLine($"Processing frame: {frame.Frame} Size {frame.Size.X} x {frame.Size.Y}");
            }

            if(frame.Frame is IDisposable disposable)
            {
                disposable.Dispose();    
            }
        });
    }

    public void Start()
    {
        _frameProcessor.Start();

        _frameReader.Start();
    }
}
