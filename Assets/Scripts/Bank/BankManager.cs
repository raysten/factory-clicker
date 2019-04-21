using Zenject;

public class BankManager : IMoneyReceiver
{
	private float balance;
	private SignalBus signalBus;

	public BankManager(SignalBus signalBus)
	{
		this.signalBus = signalBus;
	}

	public void ReceiveMoney(float amount)
	{
		balance += amount;
		signalBus.Fire(new BalanceChangedSignal(balance));
	}
}
