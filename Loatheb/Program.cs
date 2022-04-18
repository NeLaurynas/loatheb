using System.Diagnostics;
using Loatheb;

Console.WriteLine("Press any key for movement test");
var rnd = new Random();

Win32.POINT p = new Win32.POINT(100, 100);

var handle = GetWindowHandle();

while (Console.ReadKey().Key != ConsoleKey.X)
{
    for (var i = 0; i < 50000; i++)
    {
        p.x = i;
        Console.WriteLine(i);
        Win32.mouse_event((int)(Win32.MouseEventFlags.MOVE | Win32.MouseEventFlags.ABSOLUTE), p.x, p.y, 0, 0);
        // Win32.ClientToScreen(handle, ref p);
        // Win32.SetCursorPos(p.x, p.y);
        if (i % rnd.Next(500, 1000) == 0)
            Thread.Sleep(1);
        i += rnd.Next(5, 10);
    }

    p.x = 100;
    p.y = 100;
}

IntPtr GetWindowHandle()
{
    return Process.GetCurrentProcess().Handle;
    // return Process.GetProcesses().FirstOrDefault(x => x.ProcessName.ToLower().StartsWith("notepad"))?.Handle ?? IntPtr.Zero;
}
