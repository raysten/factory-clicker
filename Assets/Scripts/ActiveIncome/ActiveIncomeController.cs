using UnityEngine;
using Zenject;

public class ActiveIncomeController : MonoBehaviour
{
	private IMoneyReceiver receiver;
	private float singleIncome = 10f; // TODO: use global settings for it

	[Inject]
	public void Construct(IMoneyReceiver receiver)
	{
		this.receiver = receiver;
	}

	public void MakeMoney()
	{
		receiver.ReceiveMoney(singleIncome);
	}
}
