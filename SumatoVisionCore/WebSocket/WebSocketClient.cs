using System.Net.WebSockets;

namespace SumatoVisionCore;
public class WebSocketClient
{
    private ClientWebSocket _webSocket;
    private readonly Uri _serverUri;

    public WebSocketClient(string uri)
    {
        _webSocket = new ClientWebSocket();
        _serverUri = new Uri(uri);
    }

    public async Task ConnectAsync()
    {
        if (_webSocket.State != WebSocketState.Open)
        {
            await _webSocket.ConnectAsync(_serverUri, CancellationToken.None);
            Console.WriteLine($"Connected to {_serverUri}");
        }
    }

    public async Task SendMessageAsync(MessageModel message)
    {
        if (_webSocket.State == WebSocketState.Open)
        {
            var buffer = SerializeMessage(message);
            await _webSocket.SendAsync(
                new ArraySegment<byte>(buffer),
                WebSocketMessageType.Binary,
                endOfMessage: true,
                cancellationToken: CancellationToken.None
            );
        }
    }

    public async Task ReceiveMessagesAsync(Action<MessageModel> onMessageReceived)
    {
        var buffer = new byte[1024 * 64];
        while (_webSocket.State == WebSocketState.Open)
        {
            var result = await _webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer),
                CancellationToken.None
            );

            if (result.MessageType == WebSocketMessageType.Close)
            {
                Console.WriteLine("Service closed");
                await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            }
            else if (result.MessageType == WebSocketMessageType.Binary)
            {
                var messageBytes = buffer.Take(result.Count).ToArray();
                MessageModel message = DeserializeMessage(messageBytes);
                onMessageReceived?.Invoke(message);
            }
        }
    }

    public async Task CloseAsync()
    {
        if (_webSocket.State == WebSocketState.Open || _webSocket.State == WebSocketState.CloseReceived)
        {
            await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client closed conextion", CancellationToken.None);
            Console.WriteLine("WebSocket conection closed");
        }
    }
    public byte[] SerializeMessage(MessageModel message)
    {
        using var ms = new MemoryStream();
        using var bw = new BinaryWriter(ms);
        bw.Write((int)message.DataType);
        bw.Write(message.Data.Length);
        bw.Write(message.Data);
        return ms.ToArray();
    }

    public MessageModel DeserializeMessage(byte[] data)
    {
        using var ms = new MemoryStream(data);
        using var br = new BinaryReader(ms);
        var type = (MessageModel.FrameType)br.ReadInt32();
        int length = br.ReadInt32();
        var buffer = br.ReadBytes(length);
        return new MessageModel(buffer, type);
    }
}
