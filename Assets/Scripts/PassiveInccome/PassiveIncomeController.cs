using System.Collections;
using UnityEngine;
using Zenject;

public class PassiveIncomeController : MonoBehaviour, IIncomeMaker, IPeriodicalIncomeMaker
{
	private IMoneyStorage moneyStorage;
	private PassiveIncomeSettings passiveIncSettings;
	private bool isActive;
	private float incomeRate;
	private float interval;

	[Inject]
	public void Construct(IMoneyStorage moneyStorage, PassiveIncomeSettings passiveIncSettings)
	{
		this.moneyStorage = moneyStorage;
		this.passiveIncSettings = passiveIncSettings;
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

	private IEnumerator MakeMoneyPeriodically()
	{
		while (isActive)
		{
			moneyStorage.ChangeBalance(incomeRate);
			yield return new WaitForSeconds(interval);
		}
	}
}
