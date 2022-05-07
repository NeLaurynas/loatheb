using System.Reflection;
using System.Text;
namespace Loatheb;

public class Cfg
{
	private readonly Logger _logger;
	
	public Cfg(Logger logger)
	{
		_logger = logger;
	}
	
	public int MouseInputBatch { get; set; }
	public int MouseSleepChance { get; set; }
	public int MouseRndMovePxUpperBound { get; set; }
	
	public string LAProcessName { get; set; }

	public int DelayAfterUpAndDown { get; set; }
	public int DelayBetweenSkills { get; set; }
	public int ChanceToMove { get; set; }

	public bool ClearFloor2 { get; set; }

	public void Initialize()
	{
		if (File.Exists(@".\cfg.ini") && File.ReadAllText(@".\cfg.ini").Length > 10)
		{
			_logger.Log("Initializing configuration from cfg.ini");
			LoadSettings();
			return;
		}
		_logger.Log("Initializing default settings");

		// TODO: read config
		MouseInputBatch = 3;
		MouseSleepChance = 15;
		MouseRndMovePxUpperBound = 5;
		
		LAProcessName = "lostark";

		DelayAfterUpAndDown = 2300;
		DelayBetweenSkills = 500;
		ChanceToMove = 20;
		ClearFloor2 = true;
		
		SaveSettings();
	}

	public void SaveSettings()
	{
		_logger.Log("Saving settings to cfg.ini");
		var props = new List<(string Property, object? Value)>();
		props.AddRange(GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(x => (x.Name, x.GetValue(this))));

		var sb = new StringBuilder();

		foreach (var (propName, propValue) in props)
			sb.Append($"{propName}={propValue?.ToString()}{Environment.NewLine}");
		
		File.WriteAllText(@".\cfg.ini", sb.ToString());
	}

	public void LoadSettings()
	{
		var lines = File.ReadAllText(@".\cfg.ini").Split(Environment.NewLine);

		foreach (var line in lines.Select(x => x.Split('=')))
		{
			var prop = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(x => x.Name == line[0]);
			prop?.SetValue(this, Convert.ChangeType(line[1], prop.PropertyType));
		}
	}
}
