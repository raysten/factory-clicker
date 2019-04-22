using UnityEngine;
using Zenject;

public class PassiveIncomeController : MonoBehaviour, IIncomeMaker
{
	private IMoneyStorage moneyStorage;
	private float incomeRate;
	private float interval; // TODO:

	[Inject]
	public void Construct(IMoneyStorage moneyStorage)
	{
		this.moneyStorage = moneyStorage;
	}

	public float GetIncomeRate()
	{
		return incomeRate;
	}

	public void SetIncomeRate(float incomeRate)
	{
		this.incomeRate = incomeRate;
	}
}
