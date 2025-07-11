using SumatoVisionCore;

public class FrameReader : ThreadHandler
{

    public FrameReader(FrameQueue queue, IFrameSource frameSource, int frameDelay = 0)
    {
        _threadTask = new ReadFrameTask(queue, frameSource, frameDelay);
    }

    public void SetFrameSource(IFrameSource frameSource, int frameDelay = 0)
    {
        ((ReadFrameTask)_threadTask).SetFrameSource(frameSource, frameDelay);
    }
}
