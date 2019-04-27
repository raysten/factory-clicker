using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ActiveIncomeController : MonoBehaviour, IIncomeMaker, IIncomeBonusReceiver
{
	[SerializeField] private Text buttonText;
	private IMoneyStorage moneyStorage;
	private float incomeRate;
	private float bonusFactor;

	[Inject]
	public void Construct(IMoneyStorage moneyStorage, ActiveIncomeSettings activeIncSettings)
	{
		this.moneyStorage = moneyStorage;
		this.incomeRate = activeIncSettings.InitialIncomeRate;
		SetButtonText();
	}

	public void MakeMoney()
	{
		moneyStorage.ChangeBalance(incomeRate + incomeRate * bonusFactor);
	}

	#region IIncomeMaker
	public void SetIncomeRate(float incomeRate)
	{
		this.incomeRate = incomeRate;
		SetButtonText();
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
		SetButtonText();
	}

	public void RemoveIncomeBonus()
	{
		bonusFactor = 0f;
		SetButtonText();
	}
	#endregion

	private void SetButtonText()
	{
		buttonText.text = "+" + (incomeRate + incomeRate * bonusFactor).ToString() + "$";
	}
}
