using System.Collections;
using UnityEngine;
using Zenject;

public class PassiveIncomeController : MonoBehaviour, IIncomeMaker
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

	private IEnumerator MakeMoneyPeriodically()
	{
		while (isActive)
		{
			moneyStorage.ChangeBalance(incomeRate);
			yield return new WaitForSeconds(interval);
		}
	}
}
