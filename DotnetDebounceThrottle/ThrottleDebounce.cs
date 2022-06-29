using System;

namespace DotnetDebounceThrottle
{
    public static class ThrottleDebounce
    {
        public static Action Throttle(Action callback, int delay, bool noTrailing)
        {
            var throtter = new Throtter(callback, delay, noTrailing);

            return throtter.Run;
        }

        public static Action Debounce(Action callback, int delay, bool atBegin)
        {
            var debouncer = new Debouncer(callback, delay, atBegin);

            return debouncer.Run;
        }
    }
}