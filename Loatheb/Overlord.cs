using System.ComponentModel;
using System.Runtime.CompilerServices;
using Loatheb.Annotations;
using Loatheb.steps;
namespace Loatheb;

public class Overlord : INotifyPropertyChanged
{
	private readonly Logger _logger;
	private readonly LoathebForm _form;

	private StepBase _grindStep;
	private StepBase _fishingStep;

	public Overlord(Logger logger, LoathebForm form)
	{
		_logger = logger;
		_form = form;
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

		_grindStep = UtilSteps.CreateTryResettingUIStep(RepairEquipmentSteps.RepairEquipmentBegin);

		DI.Sys.RefreshLAWindowLocation();
		Utils.ActivateLAWindow();

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

	private async Task<StepBase?> ExecuteStep(StepBase step)
	{
		try
		{
			StepBase? nextOne;

			step.State.Iter++;

			if (step.State.Iter > step.State.MaxIter)
			{
				_logger.Log($"Step {step.GetType().Name} reached max iteration count, halting");
				Running = false;
				return null;
			}
			_logger.Log($"Executing {step.GetType().Name} - {step.State.Iter} / {step.State.MaxIter}");

			if (step.State.SleepDurationBeforeExecuting.HasValue)
			{
				_logger.Log($"Step {step.GetType().Name} waiting for {step.State.SleepDurationBeforeExecuting.Value}");
				Thread.Sleep(step.State.SleepDurationBeforeExecuting.Value);
			}
			
			nextOne = await step.Execute();
			step.AfterExec();

			if (step.State.SleepDurationAfterExecution.HasValue)
			{
				_logger.Log($"Step {step.GetType().Name} waiting for {step.State.SleepDurationAfterExecution.Value}");
				Thread.Sleep(step.State.SleepDurationAfterExecution.Value);
			}
			
			if (nextOne == null)
			{
				_logger.Log($"Next step returned by {step.GetType().Name} was null");
				return null;
			}

			if (!Running)
			{
				DI.Logger.Log("Stopping?");
				return null;
			}

			return await ExecuteStep(nextOne);
		}
		catch (Exception ex)
		{
			DI.Logger.Log($"Exception: {ex.Message}");
			Running = false;
			return null;
		}
	}

	public event PropertyChangedEventHandler? PropertyChanged;

	[NotifyPropertyChangedInvocator]
	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		if (_form.InvokeRequired)
			_form.Invoke(() =>
			{
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			});
		else
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
