namespace SumatoVisionCore;
public class FrameProcessor : ThreadHandler
{
    public FrameProcessor(FrameQueue queue, Action<IFrame> processFrameAction)
    {
        _threadTask = new ProcessFrameTask(queue, processFrameAction);
    }
}
