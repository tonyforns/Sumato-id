using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Net.WebSockets;
using System.Drawing.Imaging;

class Program
{
    static async Task Main()
    {
        using var ws = new ClientWebSocket();
        await ws.ConnectAsync(new Uri("ws://localhost:5001/ws?role=capture"), CancellationToken.None);

        var capture = new VideoCapture(0);
        using var mat = new Mat();

        while (capture.Read(mat))
        {
            using var bmp = BitmapConverter.ToBitmap(mat);
            using var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Jpeg);

            var data = ms.ToArray();
            await ws.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Binary, true, CancellationToken.None);
        }
    }
}
