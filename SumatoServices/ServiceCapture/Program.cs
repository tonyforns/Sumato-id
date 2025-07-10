using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Net.WebSockets;
using System.Drawing.Imaging;
using SumatoVisionCore;

class Program
{
    static async Task Main()
    {
        using var ws = new ClientWebSocket();
        await ws.ConnectAsync(new Uri("ws://localhost:5001/ws?role=capture"), CancellationToken.None);

        var queue = new FrameQueue();
        var reader = new FrameReader(queue, new CameraFrameSoruce(0));

        var processor = new ProcessFrameTask(queue, async (frame) =>
        {
            using Mat mat = (Mat)frame.Frame;
            byte[] data = mat.ImEncode(".jpg");

            if (ws.State == WebSocketState.Open)
            {
                Console.WriteLine($"Sending frame {mat} of size {data.Length} bytes.");
                await ws.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Binary, true, CancellationToken.None);
            }
        });

        reader.Start();

        while(true)
        {
            processor.Task();
        }
    }
}
