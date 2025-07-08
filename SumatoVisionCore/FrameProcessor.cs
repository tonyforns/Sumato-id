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
        IFrame frame = _queue.PullQueue().Resize(SetUpConfig.ResizeWidth, SetUpConfig.ResizeHeight);
        _processFrameAction?.Invoke(frame);
    }
}
