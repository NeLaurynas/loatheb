using System.Diagnostics;
using Loatheb.win32;

Console.WriteLine("Press any key for movement test");
var rnd = new Random();

var handle = GetWindowHandle();

var blaX = Win32Api.GetSystemMetrics(Structures.SystemMetric.SM_CXSCREEN);
var blaY = Win32Api.GetSystemMetrics(Structures.SystemMetric.SM_CYSCREEN);

int MOUSE_INPUT_BATCH = 5;
int MOUSE_SLEEP_CHANCE = 25;

var inputs = new Structures.INPUT[MOUSE_INPUT_BATCH];
for (int i = 0; i < MOUSE_INPUT_BATCH; i++)
{
	inputs[i] = new Structures.INPUT();
}

var mouseInput = new Structures.MOUSEINPUT();

while (Console.ReadKey().Key != ConsoleKey.X)
{
	for (var i = 0; i < blaY - 100; i++)
	{
		for (int j = 0; j < inputs.Length; j++)
		{
			inputs[j].type = Structures.INPUT_MOUSE;
			mouseInput.dy = ConvertAbsoluteCoordinate(i, blaY);
			i += rnd.Next(1, 5);
			mouseInput.dx = 100;
			mouseInput.dwFlags = Structures.MOUSEEVENTF.MOVE | Structures.MOUSEEVENTF.ABSOLUTE;
			inputs[j].U = new Structures.InputUnion {mi = mouseInput};
		}

		if (rnd.Next(100) < MOUSE_SLEEP_CHANCE)
			Thread.Sleep(1);

		// Console.WriteLine(i);
		Win32Api.SendInput((uint) inputs.Length, inputs, Structures.INPUT.Size);
	}

	Console.ReadKey();

	for (var i = 0; i < blaX - 100; i++)
	{
		for (int j = 0; j < inputs.Length; j++)
		{
			inputs[j].type = Structures.INPUT_MOUSE;
			mouseInput.dx = ConvertAbsoluteCoordinate(i, blaX);
			i += rnd.Next(1, 5);
			mouseInput.dy = 100;
			mouseInput.dwFlags = Structures.MOUSEEVENTF.MOVE | Structures.MOUSEEVENTF.ABSOLUTE;
			inputs[j].U = new Structures.InputUnion {mi = mouseInput};
		}

		if (rnd.Next(100) < MOUSE_SLEEP_CHANCE)
			Thread.Sleep(1);

		// Console.WriteLine(i);
		Win32Api.SendInput((uint) inputs.Length, inputs, Structures.INPUT.Size);
	}
}

IntPtr GetWindowHandle()
{
	return Process.GetCurrentProcess().Handle;
	// return Process.GetProcesses().FirstOrDefault(x => x.ProcessName.ToLower().StartsWith("notepad"))?.Handle ?? IntPtr.Zero;
}

int ConvertAbsoluteCoordinate(int coordinate, int resolution)
{
	return ((65536 * coordinate) / resolution) + 1;
}
