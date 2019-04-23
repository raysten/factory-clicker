using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class IncomeUpgradeController : MonoBehaviour
{
	private SignalBus signalBus;
	protected IIncomeMaker boundIncomeMaker;
	protected IMoneyStorage moneyStorage;

	[Inject]
    public void Construct(
		SignalBus signalBus,
		//IIncomeMaker incomeMaker,
		IMoneyStorage moneyStorage
	)
	{
		this.signalBus = signalBus;
		//this.boundIncomeMaker = incomeMaker;
		this.moneyStorage = moneyStorage;
	}

	private void Start()
	{
		signalBus.Subscribe<BalanceChangedSignal>(UpdateAvailability);
		UpdateUpgraderAvailability(moneyStorage.GetBalance());
	}

	private void OnDestroy()
	{
		signalBus.Unsubscribe<BalanceChangedSignal>(UpdateAvailability);
	}

	private void UpdateAvailability(BalanceChangedSignal balanceChangedInfo)
	{
		UpdateUpgraderAvailability(balanceChangedInfo.Amount);
	}

	protected abstract void UpdateUpgraderAvailability(float balance);

	public abstract void Upgrade();
}
