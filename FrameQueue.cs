
using System.Collections.Concurrent;

namespace SumatoId;

public class FrameQueue
{
    private BlockingCollection<IFrame> _queue = new();

    public void PushQueue(IFrame frame) => _queue.Add(frame);
    public IFrame PullQueue() => _queue.Take();
}
