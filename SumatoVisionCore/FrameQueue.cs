using System.Collections.Concurrent;

namespace SumatoVisionCore;

public class FrameQueue
{
    private BlockingCollection<IFrame> _queue = new();
    public int Count => _queue.Count;
    public void PushQueue(IFrame frame) => _queue.Add(frame);
    public IFrame PullQueue() => _queue.Take();
}
