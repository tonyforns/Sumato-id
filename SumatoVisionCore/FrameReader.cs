using SumatoVisionCore;

public class FrameReader : ThreadHandler
{
    private readonly FrameQueue _queue;
    private IFrameSource _capture;
 
    public FrameReader(FrameQueue queue, IFrameSource frameSource)
    {
        _queue = queue;
        _capture = frameSource;
    }

    public void SetFrameSource(IFrameSource frameSource)
    {
        _capture = frameSource;
    }

    internal override void ThreadFunction()
    {
        if (!TryToReadFrame()) return;

        int fps = 1000 / SetUpConfig.FrameRate;
        Thread.Sleep(fps);
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