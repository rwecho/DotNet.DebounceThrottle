using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetDebounceThrottle
{
    internal class Throtter
    {
        private Action _callback;
        private int _delay;
        private bool _noTrailing;
        private DateTime _lastExecutionTime;
        private Timer _tailingTimer;
        private TaskCompletionSource<bool> _timerTcs;

        public Throtter(Action callback, int delay = 200, bool noTrailing = true)
        {
            this._callback = callback;
            this._delay = delay;
            this._noTrailing = noTrailing;
        }

        private void Execute(bool tailing = false)
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

            if (DateTime.Now > _lastExecutionTime.AddMilliseconds(_delay))
            {
                this.Execute();
            }
            else if (!_noTrailing)
            {
                _tailingTimer = new
                    Timer((state) => { this.Execute(true); }, null, _delay, Timeout.Infinite);
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