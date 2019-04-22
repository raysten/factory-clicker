using UnityEngine;
using Zenject;

public class ActiveIncomeController : MonoBehaviour, IIncomeMaker
{
	private IMoneyStorage moneyStorage;
	private float incomeRate = 1f; // TODO: use global settings for it

	[Inject]
	public void Construct(IMoneyStorage moneyStorage)
	{
		this.moneyStorage = moneyStorage;
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
