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
        if (TryToReadFrame(out IFrame frame))
            _queue.PushQueue(frame);

        int fps = 1000 / SetUpConfig.FrameRate;
        Thread.Sleep(fps);
    }

    private bool TryToReadFrame(out IFrame frame)
    {
        if (!_capture.Read(out frame))
        {
            return false;
        }
        return true;
    }
}