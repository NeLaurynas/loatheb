using System.ComponentModel;
using System.Runtime.CompilerServices;
using Loatheb.Annotations;
using Loatheb.steps;
namespace Loatheb;

public class Overlord : INotifyPropertyChanged
{
	private readonly Logger _logger;

	private Step _grindStep;
	private Step _fishingStep;

	public Overlord(Logger logger)
	{
		_logger = logger;
		_grindStep = new RepairNormalGearStep();
	}

	private bool _running;
	public bool Running
	{
		get
		{
			return _running;
		}
		set
		{
			_running = value;
			OnPropertyChanged();
		}
	}

	public async Task Run()
	{
		if (Running) return;

		Running = true;
		
		DI.Utils.ActivateLAWindow();

		_logger.Log("Started executing grind steps");

		await Task.Run(async () =>
		{
			await ExecuteStep(_grindStep);
		});
	}

	public void Stop()
	{
		if (!Running) return;

		Running = false;
	}

	public void Initialize()
	{
		_logger.Log("Initializing Overlord - constructing steps");
	}

	private async Task<Step?> ExecuteStep(Step step)
	{
		Step? nextOne;

		step.State.IterCount++;

		if (step.State.IterCount > step.State.MaxIter)
		{
			_logger.Log($"Step {step.GetType().Name} reached max iteration count, halting");
			Running = false;
			return null;
		}
		_logger.Log($"Executing {step.GetType().Name} - {step.State.IterCount} / {step.State.MaxIter}");

		nextOne = await step.Execute();

		if (nextOne == null)
		{
			_logger.Log($"Next step returned by {step.GetType().Name} was null");
			return null;
		}

		// await ExecuteStep(nextOne);
		// if (result == true)
		// 	await ExecuteStep(step.OkStep);
		// else if (result == false)
		// {
		// 	var sleepTime = step.State.SleepDurationOnFail ?? 5000;
		// 	Thread.Sleep(sleepTime);
		// 	if (step.FailStep != null)
		// 		await ExecuteStep(step.FailStep);
		// 	else
		// 	{
		// 		_logger.Log($"Step {step.GetType().Name} failed, but had no fail step, retrying");
		// 		return null;
		// 	}
		// }
		
		return await ExecuteStep(nextOne);
	}

	public event PropertyChangedEventHandler? PropertyChanged;

	[NotifyPropertyChangedInvocator]
	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
