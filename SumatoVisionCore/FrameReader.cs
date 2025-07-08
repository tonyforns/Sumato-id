using SumatoVisionCore;

public class FrameReader
{
    private readonly FrameQueue _queue;
    private readonly IFrameSource _capture;
 
    public FrameReader(FrameQueue queue, IFrameSource frameSource)
    {
        _queue = queue;
        _capture = frameSource;
    }

    public void Start()
    {
        new Thread(() =>
        {
            while (true)
            {
                if (!TryToReadFrame()) break;

                int fps = 1000 / SetUpConfig.FrameRate;
                Thread.Sleep(fps);
            }
        }).Start();
    }

    private bool TryToReadFrame()
    {
        if (!_capture.Read(out IFrame frame) || frame.IsEmpty)
        {
            Console.WriteLine("Can't read frame or frame is Empty");
            return false;
        }
        _queue.PushQueue(frame);
        return true;
    }
}