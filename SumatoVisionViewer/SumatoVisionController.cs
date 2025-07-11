using SumatoVisionCore;

namespace SumatoVisionViewer
{
    public class SumatoVisionController : IDisposable   
    {
        private FrameQueue _queue;
        private FrameReader _reader;
        private FrameProcessor _processor;

        public SumatoVisionController(Action<IFrame> processorHandler)
        {
            _queue = new FrameQueue();
            _processor = new FrameProcessor(_queue, processorHandler);
        }

        public void FileVideoBtn_Click(string path)
        {
            if(_reader != null)
            {
                _reader.SetFrameSource(new FileFrameSource(path), SetUpConfig.frameDelayMs);
            } else
            {
                _reader = new FrameReader(_queue, new FileFrameSource(path), SetUpConfig.frameDelayMs);
            }
        }

        public void CameraBtn_Click()
        {
            if (_reader != null)
            {
                _reader.SetFrameSource(new CameraFrameSoruce(0));
            }
            else
            {
                _reader = new FrameReader(_queue, new CameraFrameSoruce(0));
            }
        }

        public void StartBtn_Click()
        {
            if(_reader.IsRunning && _processor.IsRunning)
            {
                _reader.Resume();
                _processor.Resume();
            } else
            {
                _reader.Start();
                _processor.Start();
            }
        }

        public void StopBtn_Click()
        {
            _reader.Pause();
            _processor.Pause();
        }

        public void Dispose()
        {
            _processor.Dispose();
            _reader.Dispose();
            _queue.Dispose();
        }
    }
}
