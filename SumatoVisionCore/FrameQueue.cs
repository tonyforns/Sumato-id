using System.Collections.Concurrent;

namespace SumatoVisionCore;

public class FrameQueue : IDisposable
{
    private BlockingCollection<IFrame> _queue = new();
    public int Count => _queue.Count;
    public void PushQueue(IFrame frame) => _queue.Add(frame);
    public IFrame PullQueue() => _queue.Take();

    public void Dispose()
    {
        if (_queue != null)
        {
            _queue.CompleteAdding();

            foreach (IFrame frame in _queue)
            {
                if(frame.Frame is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }

            _queue.Dispose();
            _queue = null;
        }
    }
}
