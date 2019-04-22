using UnityEngine;
using Zenject;

public class ActiveIncomeController : MonoBehaviour, IIncomeMaker
{
	private IMoneyStorage moneyStorage;
	private float incomeRate;

	[Inject]
	public void Construct(IMoneyStorage moneyStorage, ActiveIncomeSettings activeIncSettings)
	{
		this.moneyStorage = moneyStorage;
		this.incomeRate = activeIncSettings.InitialIncomeRate;
	}

	public void MakeMoney()
	{
		moneyStorage.ChangeBalance(incomeRate);
	}

	public void SetIncomeRate(float incomeRate)
	{
		this.incomeRate = incomeRate;
	}

	public float GetIncomeRate()
	{
		return incomeRate;
	}
}
