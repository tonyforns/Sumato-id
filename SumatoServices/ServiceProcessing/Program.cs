using System.Net.WebSockets;
using OpenCvSharp;
using SumatoVisionCore;
class Program
{
    static async Task Main()
    {
        using var ws = new ClientWebSocket();
        await ws.ConnectAsync(new Uri("ws://localhost:5001/ws?role=processing"), CancellationToken.None);

        var buffer = new byte[1024 * 1024];

        while (true)
        {
            var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            var frameData = buffer[..result.Count];

            using Mat mat = Cv2.ImDecode(frameData, ImreadModes.Color);
            if (mat.Empty())
            {
                Console.WriteLine("Error: Empty or invalid Image.");
                continue;
            }
            MatFrame matFrame = new MatFrame(mat);
            matFrame.Resize(640, 480);

            Console.WriteLine($"Received frame: {matFrame.Frame}  {matFrame.Size.X}x{matFrame.Size.Y}");
        }
    }
}
