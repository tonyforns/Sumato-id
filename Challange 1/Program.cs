using SumatoVisionCore;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Iniciando procesamiento de frames...");

        var queue = new FrameQueue();

        var reader = new FrameReader(queue, args.Length > 0 ? new FileFrameSource(args[0]) : new CameraFrameSoruce(0));

        var processor = new FrameProcessor(queue, (frame) =>
        {
            frame.Resize(SetUpConfig.ResizeWidth,SetUpConfig.ResizeHeight);
            Console.WriteLine($"Frame recibido: {frame.Frame} : {frame.Size.X}x{frame.Size.Y}");
        });

        reader.Start();
        processor.Start();

        Console.WriteLine("Presioná Enter para salir...");
        Console.ReadLine();
    }
}
