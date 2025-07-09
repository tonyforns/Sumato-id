namespace SumatoVisionCore;

public class ProcessFrameTask : IThreadTask
{
    private readonly FrameQueue _queue;
    private Action<IFrame> _processFrameAction;

    public ProcessFrameTask(FrameQueue queue, Action<IFrame> processFrameAction)
    {
        _queue = queue;
        _processFrameAction = processFrameAction;
    }
    public void Task()
    {
        if (_queue.Count == 0)
        {
            return;
        }
        IFrame frame = _queue.PullQueue();
        _processFrameAction?.Invoke(frame);
    }
    public void OnStopped()
    {
        _processFrameAction = null;
    }
}
