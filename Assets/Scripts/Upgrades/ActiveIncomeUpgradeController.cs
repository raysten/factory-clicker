using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ActiveIncomeUpgradeController : MonoBehaviour
{
	[SerializeField] private Button upgradeButton;
	private SignalBus signalBus;
	private IIncomeMaker boundIncomeMaker;
	private IMoneyStorage moneyStorage;
	private float upgradeFactor = 2f; // TODO: Use global settings
	private float upgradeCostFactor = -8f;

	// TODO: Display upgrade cost. Handle maximum possible upgrade.
	// TODO: Use it also for passive income and add another interface for activating upgrade.

	[Inject]
    public void Construct(SignalBus signalBus, IIncomeMaker incomeMaker, IMoneyStorage moneyStorage)
	{
		this.signalBus = signalBus;
		this.boundIncomeMaker = incomeMaker;
		this.moneyStorage = moneyStorage;
	}

	private void Start()
	{
		signalBus.Subscribe<BalanceChangedSignal>(DisableUpgrading);
		DisableUpgrader(moneyStorage.GetBalance());
	}

	private void OnDestroy()
	{
		signalBus.Unsubscribe<BalanceChangedSignal>(DisableUpgrading);
	}

	private void DisableUpgrading(BalanceChangedSignal balanceChangedInfo)
	{
		DisableUpgrader(balanceChangedInfo.amount);
	}

	private void DisableUpgrader(float balance)
	{
		upgradeButton.interactable =
			balance >= Mathf.Abs(boundIncomeMaker.GetIncomeRate() * upgradeCostFactor);
	}

	public void Upgrade()
	{
		float currentIncomeRate = boundIncomeMaker.GetIncomeRate();
		moneyStorage.ChangeBalance(currentIncomeRate * upgradeCostFactor);
		boundIncomeMaker.SetIncomeRate(currentIncomeRate * upgradeFactor);
		DisableUpgrader(moneyStorage.GetBalance());
	}
}
