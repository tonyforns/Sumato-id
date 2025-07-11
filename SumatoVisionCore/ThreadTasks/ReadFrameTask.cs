using SumatoVisionCore;

public class ReadFrameTask : IThreadTask
{
    private readonly FrameQueue _queue;
    private int _frameDelay;
    private IFrameSource _capture;
    private Thread _thread;
    public ReadFrameTask(FrameQueue queue, IFrameSource frameSource, int frameDelay = 0)
    {
        _queue = queue;
        _capture = frameSource;
        _frameDelay = frameDelay;
    }
    public void Task()
    {
        if (TryToReadFrame(out IFrame frame))
            _queue.PushQueue(frame);
        Thread.Sleep(_frameDelay);
    }
    private bool TryToReadFrame(out IFrame frame)
    {
        if (!_capture.Read(out frame))
        {
            return false;
        }
        return true;
    }
    public void OnStopped()
    {
        if(_capture is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    public void SetFrameSource(IFrameSource frameSource, int frameDelay = 0)
    {
        _capture = frameSource;
        _frameDelay = frameDelay;
    }
}