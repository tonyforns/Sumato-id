using OpenCvSharp;

namespace SumatoId;
public class FrameProcessor
{
    private readonly FrameQueue _queue;

    public FrameProcessor(FrameQueue queue)
    {
        _queue = queue;
    }

    public void Start()
    {
        new Thread(() =>
        {
            while (true)
            {
                IFrame frame = _queue.PullQueue().Resize(SetUpConfig.ResizeWidth, SetUpConfig.ResizeHeight);

                Console.WriteLine("Processed frame: " + frame.Frame);
            }
        }).Start();
    }
}
