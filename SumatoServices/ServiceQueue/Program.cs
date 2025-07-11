using Microsoft.AspNetCore.Builder;
using System.Net.WebSockets;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

ConcurrentQueue<byte[]> frameQueue = new();
List<WebSocket> processingClients = new();

app.UseWebSockets();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/ws")
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            Console.WriteLine("WebSocket connected.");

            if (context.Request.Query["role"] == "capture")
            {
                var buffer = new byte[1024 * 1024];
                while (true)
                {
                    var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Close)
                        break;

                    var frameData = buffer[..result.Count];
                    //frameQueue.Enqueue(frameData);

                    foreach (var client in processingClients.ToList())
                    {
                        if (client.State == WebSocketState.Open)
                        {
                            await client.SendAsync(new ArraySegment<byte>(frameData), WebSocketMessageType.Binary, true, CancellationToken.None);
                        }
                    }
                }
            }
            else if (context.Request.Query["role"] == "processing")
            {
                processingClients.Add(webSocket);
                while (webSocket.State == WebSocketState.Open)
                {
                    await Task.Delay(100);
                }
                processingClients.Remove(webSocket);
            }
        }
        else
        {
            context.Response.StatusCode = 400;
        }
    }
    else
    {
        await next();
    }
});

app.Run("http://localhost:5001");
