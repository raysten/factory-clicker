using UnityEngine;
using Zenject;

public class ActiveIncomeController : MonoBehaviour, IIncomeMaker, IIncomeBonusReceiver
{
	private IMoneyStorage moneyStorage;
	private float incomeRate;
	private float bonusFactor;

	[Inject]
	public void Construct(IMoneyStorage moneyStorage, ActiveIncomeSettings activeIncSettings)
	{
		this.moneyStorage = moneyStorage;
		this.incomeRate = activeIncSettings.InitialIncomeRate;
	}

	public void MakeMoney()
	{
		moneyStorage.ChangeBalance(incomeRate + incomeRate * bonusFactor);
	}

	#region IIncomeMaker
	public void SetIncomeRate(float incomeRate)
	{
		this.incomeRate = incomeRate;
	}

	public float GetIncomeRate()
	{
		return incomeRate;
	}
	#endregion

	#region IIncomeBonusReceiver
	public void AddIncomeBonus(float percentage)
	{
		bonusFactor = percentage / 100f;
	}

	public void RemoveIncomeBonus()
	{
		bonusFactor = 0f;
	}
	#endregion
}
