
using DotNet.DebounceThrottle;

var action = ThrottleDebounce.Debounce(() =>
{
    Console.WriteLine($"{DateTime.Now:HH:mm:ss FFF}");
}, 2000, false);

var i = 30;
while (i>0)
{
    i--;
    Parallel.For(1, 10, (index) =>
    {
        action();
    });

    await Task.Delay(100);
}


var action1 = ThrottleDebounce.Throttle(() =>
{
    Console.WriteLine($"{DateTime.Now:HH:mm:ss FFF}");
}, 2000, false);

i = 30;
while (i > 0)
{
    i--;
    Parallel.For(1, 10, (index) =>
    {
        action();
    });

    await Task.Delay(100);
}

await Task.Delay(3000);

