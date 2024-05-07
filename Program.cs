using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseWebSockets();

var SOCKETS = new List<WebSocket>();
app.MapGet("/ws", async (HttpContext context) =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        SOCKETS.Add(webSocket);
        await HandleWebSocketRequest(webSocket);
    }
    else
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("This endpoint only accepts WebSocket");
    }
});
app.Run();


// Temporary functions
async Task HandleWebSocketRequest(WebSocket socket)
{
    try
    {
        while (socket.State == WebSocketState.Open)
        {
            var buffer = new byte[1024 * 4];
            var receivedBuffer = new ArraySegment<byte>(buffer);
            var message = await socket.ReceiveAsync(receivedBuffer, default);

            if (message.MessageType == WebSocketMessageType.Close)
            {
                var closeStatus = message.CloseStatus.HasValue ? message.CloseStatus.Value
                                                                : WebSocketCloseStatus.Empty;
                await socket.CloseAsync(closeStatus, message.CloseStatusDescription, default);
                break;
            }

            foreach (var s in SOCKETS)
            {
                string textMessage = Encoding.UTF8.GetString(receivedBuffer);
                textMessage = $"Received at {DateTime.Now} --> {textMessage}";
                await s.SendAsync(Encoding.UTF8.GetBytes(textMessage), WebSocketMessageType.Text, true, default);
            }
        }
    }
    catch (WebSocketException ex)
    {
        Console.WriteLine($"--> An exception occured: {ex.Message}");
    }
    finally
    {
        SOCKETS.Remove(socket);
    }
}