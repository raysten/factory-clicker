using System.Collections;
using UnityEngine;
using Zenject;

public class BonusManager : MonoBehaviour
{
	private IIncomeBonusReceiver activeIncome;
	private IIncomeBonusReceiver passiveIncome;
	private IIntervalBonusReceiver passiveInterval;

	[Inject]
    public void Construct(
		[Inject (Id = "active")] IIncomeBonusReceiver activeIncome,
		[Inject(Id = "passive")] IIncomeBonusReceiver passiveIncome,
		[Inject(Id = "passive")] IIntervalBonusReceiver passiveInterval
	)
	{
		this.activeIncome = activeIncome;
		this.passiveIncome = passiveIncome;
		this.passiveInterval = passiveInterval;
	}

	public void ProcessBonus(BonusesSettings.BonusSettings bonusData, BonusButton button)
	{
		switch (bonusData.Type)
		{
			case BonusesSettings.BONUS_TYPE.ACTIVE_INCOME:
				StartCoroutine(IncomeBonusTimeout(activeIncome, bonusData.Percentage, bonusData.Duration, button));
				break;
			case BonusesSettings.BONUS_TYPE.PASSIVE_INCOME:
				StartCoroutine(IncomeBonusTimeout(passiveIncome, bonusData.Percentage, bonusData.Duration, button));
				break;
			case BonusesSettings.BONUS_TYPE.PASSIVE_INTERVAL:
				StartCoroutine(IntervalBonusTimeout(passiveInterval, bonusData.Percentage, bonusData.Duration, button));
				break;
		}
	}

	private IEnumerator IncomeBonusTimeout(IIncomeBonusReceiver receiver, float percentage, float duration, BonusButton button)
	{
		receiver.AddIncomeBonus(percentage);
		button.Disable();
		yield return new WaitForSeconds(duration);
		receiver.RemoveIncomeBonus();
		button.Enable();
	}

	private IEnumerator IntervalBonusTimeout(IIntervalBonusReceiver receiver, float percentage, float duration, BonusButton button)
	{
		receiver.AddIntervalBonus(percentage);
		button.Disable();
		yield return new WaitForSeconds(duration);
		button.Enable();
		receiver.RemoveIntervalBonus();
	}
}
