using System.Collections;
using UnityEngine;
using Zenject;

public class PassiveIncomeController : MonoBehaviour, IIncomeMaker, IPeriodicalIncomeMaker, IIncomeBonusReceiver, IIntervalBonusReceiver
{
	private IMoneyStorage moneyStorage;
	private bool isActive;
	private float incomeRate;
	private float incomeBonusFactor;
	private float interval;
	private float intervalBonusFactor;

	[Inject]
	public void Construct(IMoneyStorage moneyStorage, PassiveIncomeSettings passiveIncSettings)
	{
		this.moneyStorage = moneyStorage;
		this.interval = passiveIncSettings.InitialInterval;
	}

	#region IIncomeMaker
	public float GetIncomeRate()
	{
		return incomeRate;
	}

	public void SetIncomeRate(float incomeRate)
	{
		this.incomeRate = incomeRate;

		if (!isActive)
		{
			isActive = true;
			StartCoroutine(MakeMoneyPeriodically());
		}
	}
	#endregion

	#region IPeriodicalIncomeMaker
	public void DecreaseInterval(float delta)
	{
		interval -= delta;
	}

	public bool IsActive()
	{
		return isActive;
	}
	#endregion

	#region IIncomeBonusReceiver
	public void AddIncomeBonus(float percentage)
	{
		incomeBonusFactor = percentage / 100f;
	}

	public void RemoveIncomeBonus()
	{
		incomeBonusFactor = 0f;
	}
	#endregion

	#region IIntervalBonusReceiver
	public void AddIntervalBonus(float percentage)
	{
		intervalBonusFactor = percentage / 100f;
	}

	public void RemoveIntervalBonus()
	{
		intervalBonusFactor = 0f;
	}
	#endregion

	private IEnumerator MakeMoneyPeriodically()
	{
		while (isActive)
		{
			moneyStorage.ChangeBalance(incomeRate + incomeRate * incomeBonusFactor);
			yield return new WaitForSeconds(interval - interval * intervalBonusFactor);
		}
	}
}
