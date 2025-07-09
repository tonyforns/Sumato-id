namespace SumatoVisionCore;
public class FrameProcessor : ThreadHandler
{
    private readonly FrameQueue _queue;
    private Action<IFrame> _processFrameAction;

    public FrameProcessor(FrameQueue queue, Action<IFrame> processFrameAction)
    {
        _queue = queue;
        _processFrameAction = processFrameAction;
    }

    internal override void ThreadFunction()
    {
        if (_queue.Count == 0)
        {
            return;
        }
        IFrame frame = _queue.PullQueue();
        _processFrameAction?.Invoke(frame);
    }

    internal override void OnStopped()
    {
        _processFrameAction = null;
        base.OnStopped();
    }
}
