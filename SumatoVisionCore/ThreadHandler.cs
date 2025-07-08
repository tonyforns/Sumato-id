using System.Threading;

namespace SumatoVisionCore
{
    public abstract class ThreadHandler
    {
        private Thread _thread;
        private ManualResetEventSlim _pauseEvent = new ManualResetEventSlim(true);
        public bool IsRunning { get; private set; } = false;

        public void Start()
        {
            if (IsRunning) return;
            IsRunning = true;

            _thread = new Thread(() =>
            {
                while (true)
                {
                    _pauseEvent.Wait();

                    ThreadFunction();
                }
            });

            _thread.Start();
        }

        internal abstract void ThreadFunction();

        public void Pause()
        {
            _pauseEvent.Reset();
        }

        public void Resume()
        {
            _pauseEvent.Set();
        }
    }
}
