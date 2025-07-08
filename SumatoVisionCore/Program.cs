namespace SumatoVisionCore;

class Program
{
    static void Main(string[] args)
    {
        FrameQueue queue = new FrameQueue();

        FrameReader reader = args.Length > 0 ? 
            new FrameReader(queue, new FileFrameSource(args[0])) :
            new FrameReader(queue, new CameraFrameSoruce(0));

       // string path = $@"C:\Programing\Sumato-id\Video.mp4";

        FrameProcessor processor = new FrameProcessor(queue);

        reader.Start();
        processor.Start();

        Console.WriteLine("Presione cualquier tecla para salir...");
        Console.ReadKey();
    }
}