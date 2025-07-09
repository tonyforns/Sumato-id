using SumatoVisionCore;

public class ReadFrameTask : IThreadTask
{
    private readonly FrameQueue _queue;
    private IFrameSource _capture;
    private Thread _thread;
    public ReadFrameTask(FrameQueue queue, IFrameSource frameSource)
    {
        _queue = queue;
        _capture = frameSource;
    }
    public void Task()
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
    public void OnStopped()
    {
        if(_capture is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }

    public void SetFrameSource(IFrameSource frameSource)
    {
        _capture = frameSource;
    }
}