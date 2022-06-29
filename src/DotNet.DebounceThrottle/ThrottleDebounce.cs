using System;

namespace DotNet.DebounceThrottle
{
    /// <summary>
    /// Providers throttle and debounce function.
    /// </summary>
    public static class ThrottleDebounce
    {
        /// <summary>
        /// Rate limit your actions within a delay time.
        /// </summary>
        /// <param name="callback">The limited action</param>
        /// <param name="delay">Between the delay time, only once trigger.</param>
        /// <param name="noTrailing">Represent whether if trigger at the end.</param>
        /// <returns>Return action to execute repeatedly</returns>
        public static Action Throttle(Action callback, int delay, bool noTrailing)
        {
            var throtter = new Throtter(callback, delay, noTrailing);

            return throtter.Run;
        }

        /// <summary>
        /// Debounce your actions within a delay time. The trigger will be postponed once invoked in the delay time
        /// </summary>
        /// <param name="callback">The debounced action</param>
        /// <param name="delay">Delay time</param>
        /// <param name="atBegin">Represent whether if trigger at begin or end.</param>
        /// <returns>Return action to execute repeatedly.</returns>
        public static Action Debounce(Action callback, int delay, bool atBegin)
        {
            var debouncer = new Debouncer(callback, delay, atBegin);

            return debouncer.Run;
        }
    }
}