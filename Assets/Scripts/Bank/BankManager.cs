using Zenject;

public class BankManager : IMoneyStorage
{
	private float balance;
	private SignalBus signalBus;

	public BankManager(SignalBus signalBus)
	{
		this.signalBus = signalBus;
	}

	public void ChangeBalance(float amount)
	{
		balance += amount;
		signalBus.Fire(new BalanceChangedSignal(balance));
	}

	public float GetBalance()
	{
		return balance;
	}
}
