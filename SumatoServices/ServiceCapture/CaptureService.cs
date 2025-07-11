
using OpenCvSharp;

namespace SumatoVisionCore;
internal class CaptureService
{
    private readonly WebSocketClient _websocketClient;
    private FrameReader _frameReader;
    private FrameProcessor _frameProcessor;

    public CaptureService(string serverUri, IFrameSource frameSource)
    {
        _websocketClient = new WebSocketClient(serverUri);
        var queue = new FrameQueue();
        _frameReader = new FrameReader(queue, frameSource);


        _frameProcessor = new FrameProcessor(queue, async (frame) =>
        {
            using Mat mat = (Mat)frame.Frame;
            byte[] data = ((Mat)frame.Frame).ImEncode(".jpg");
            var message = new MessageModel(data, MessageModel.FrameType.Mat);
            Console.WriteLine($"Sending frame of size: {data.Length} bytes");
            await _websocketClient.SendMessageAsync(message);
        });
    }


    public void Start()
    {
        _websocketClient.ConnectAsync().Wait();
        _frameReader.Start();
        _frameProcessor.Start();
    }
}
