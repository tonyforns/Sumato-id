namespace SumatoVisionCore
{
    public abstract class ThreadHandler : IDisposable
    {
        private Thread _thread;
        protected IThreadTask _threadTask;
        private ManualResetEventSlim _pauseEvent = new ManualResetEventSlim(true);
        private bool _shouldStop = false;
        public bool IsRunning { get; private set; } = false;

        public void Start()
        {
            if (IsRunning) return;
            IsRunning = true;

            _thread = new Thread(() =>
            {
                while (!_shouldStop)
                {
                    _pauseEvent.Wait();

                    if (_shouldStop) break;

                    _threadTask.Task();
                }
                _threadTask.OnStopped();
            });

            _thread.Start();
        }

        public void Pause()
        {
            _pauseEvent.Reset();
        }

        public void Resume()
        {
            _pauseEvent.Set();
        }

        private void Stop()
        {
            if (!IsRunning) return;

            _shouldStop = true;
            _pauseEvent.Set();
            _thread?.Join(1000);
            IsRunning = false;
        }
        public void Dispose()
        {
            Stop();
            _pauseEvent.Dispose();
        }
    }
}
