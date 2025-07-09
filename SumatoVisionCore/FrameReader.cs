using SumatoVisionCore;

public class FrameReader : ThreadHandler
{
    public FrameReader(FrameQueue queue, IFrameSource frameSource)
    {
        _threadTask = new ReadFrameTask(queue, frameSource);
    }

    public void SetFrameSource(IFrameSource frameSource)
    {
        ((ReadFrameTask)_threadTask).SetFrameSource(frameSource);
    }
}
