using OpenCvSharp;

namespace SumatoVisionCore;
public class ServerFrameSource : IFrameSource
{
    private readonly FrameQueue queue;

    public ServerFrameSource(string url)
    {
        queue = new FrameQueue();
        WebSocketClient webSocketClient = new WebSocketClient(url);

        webSocketClient.ConnectAsync().Wait();
        webSocketClient.ReceiveMessagesAsync(OnMessageReceived);
    }

    private void OnMessageReceived(MessageModel model)
    {
        queue.PushQueue(ReconstructFrame(model));
    }

    public static IFrame ReconstructFrame(MessageModel message)
    {
        return message.DataType switch
        {
            MessageModel.FrameType.Mat => new MatFrame(Cv2.ImDecode(message.Data, ImreadModes.Color)),
            _ => throw new NotSupportedException()
        };
    }

    public bool Read(out IFrame frame)
    {
        frame = null;   
        if (queue.Count > 0)
        {
            frame = queue.PullQueue();
            return true;
        }
        return false;
    }
}
