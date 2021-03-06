using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCore.DebounceThrottle
{
    internal class Debouncer
    {
        private readonly Action _callback;
        private readonly int _delay;
        private readonly bool _atBegin;
        private DateTime _lastExecutionTime;
        private Timer _tailingTimer;
        private TaskCompletionSource<bool> _timerTcs;

        public Debouncer(Action callback, int delay = 200, bool atBegin = false)
        {
            this._callback = callback;
            this._delay = delay;
            this._atBegin = atBegin;
        }

        private void Execute()
        {
            try
            {
                _lastExecutionTime = DateTime.Now;
                _callback();
                _timerTcs.SetResult(true);
            }
            catch (Exception exception)
            {
                _timerTcs.SetException(exception);
            }
        }
        
        public void Run()
        {
            lock (this)
            {
                this.InternalRun();
            }
        }
        
        private void InternalRun()
        {
            _timerTcs = new TaskCompletionSource<bool>();
            if (_tailingTimer != null)
            {
                _tailingTimer.Dispose();
            }

            var now = DateTime.Now;
            if (now > _lastExecutionTime.AddMilliseconds(_delay))
            {
                if (_atBegin)
                {
                    this.Execute();
                }
                else
                {
                    _tailingTimer = new
                       Timer((state) => { this.Execute(); }, null, _delay, Timeout.Infinite);
                }
            }
            else
            {
                // extend the next execute time.
                _lastExecutionTime = DateTime.Now;
            }
        }

        public Task Task
        {
            get
            {
                return _timerTcs.Task;
            }
        }
    }
}