using System.Collections;
using UnityEngine;
using Zenject;

public class BonusManager : MonoBehaviour
{
	private IIncomeBonusReceiver activeIncome;
	private IIncomeBonusReceiver passiveIncome;
	private IIntervalBonusReceiver passiveInterval;
	private BonusesSettings settings;

	[Inject]
    public void Construct(
		[Inject (Id = "active")] IIncomeBonusReceiver activeIncome,
		[Inject(Id = "passive")] IIncomeBonusReceiver passiveIncome,
		[Inject(Id = "passive")] IIntervalBonusReceiver passiveInterval,
		BonusesSettings settings
	)
	{
		this.activeIncome = activeIncome;
		this.passiveIncome = passiveIncome;
		this.passiveInterval = passiveInterval;
		this.settings = settings;
	}

	public void AddActiveIncomeBonus()
	{
		StartCoroutine(IncomeBonusTimeout(activeIncome, settings.ActiveIncome.Percentage, settings.ActiveIncome.Duration));
	}

	public void AddPassiveIncomeBonus()
	{
		StartCoroutine(IncomeBonusTimeout(passiveIncome, settings.PassiveIncome.Percentage, settings.PassiveIncome.Duration));
	}

	public void AddPassiveIntervalBonus()
	{
		StartCoroutine(
			IntervalBonusTimeout(
				passiveInterval,
				settings.PassiveInterval.Percentage,
				settings.PassiveInterval.Duration
			)
		);
	}

	private IEnumerator IncomeBonusTimeout(IIncomeBonusReceiver receiver, float percentage, float duration)
	{
		receiver.AddIncomeBonus(percentage);
		yield return new WaitForSeconds(duration);
		receiver.RemoveIncomeBonus();
	}

	private IEnumerator IntervalBonusTimeout(IIntervalBonusReceiver receiver, float percentage, float duration)
	{
		receiver.AddIntervalBonus(percentage);
		yield return new WaitForSeconds(duration);
		receiver.RemoveIntervalBonus();
	}
}
