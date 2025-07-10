using System.Drawing;
using System.Net.WebSockets;

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

            using var ms = new MemoryStream(frameData);
            var bmp = new Bitmap(ms);
            var resized = new Bitmap(bmp, new Size(640, 480));

            Console.WriteLine($"Frame recibido: {resized.Width}x{resized.Height}");
        }
    }
}
