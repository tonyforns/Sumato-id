
namespace SumatoVisionCore;

public static class ErrorHandler
{
    public static Action<string>? OnError;
       public static void LogError(string message)
       {
           OnError?.Invoke(message);
    }
}
