# DotNetCore.DebounceThrottle
This library is inspired by [jquery-throttle-debounce](https://github.com/cowboy/jquery-throttle-debounce). Most of the DotNet libraries about debounce/throttle is used into WPF.
In my Blazor project, I hope to use this library to improve the performance of my application. In this library, that can be used to throttle and debounce.

## Throttle
Demonstrate how to use the throttle in code.
``` csharp
var throttle = ThrottleDebounce.Throttle(action, 200);

while(true)
{
    throttle();
    await Task.Delay(100);
}

```
## Debounce
Demonstrate how to use the debounce in code.
```charp
var debounce = ThrottleDebounce.Debounce(action, 200);

while(true)
{
    debounce();
    await Task.Delay(100);
}
```