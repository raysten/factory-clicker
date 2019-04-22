using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class IncomeUpgradeController : MonoBehaviour
{
	[SerializeField] private Button upgradeButton;
	[SerializeField] private Text costTxt;
	private SignalBus signalBus;
	private IIncomeMaker boundIncomeMaker;
	private IMoneyStorage moneyStorage;
	private float upgradeFactor = 2f; // TODO: Use global settings
	private float upgradeCostFactor = -8f;
	private int maximumUpgradeCount = 10;
	private int upgradeCount;

	[Inject]
    public void Construct(SignalBus signalBus, IIncomeMaker incomeMaker, IMoneyStorage moneyStorage)
	{
		this.signalBus = signalBus;
		this.boundIncomeMaker = incomeMaker;
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
		UpdateUpgraderAvailability(balanceChangedInfo.amount);
	}

	private void UpdateUpgraderAvailability(float balance)
	{
		float currentUpgradeCost = Mathf.Abs(boundIncomeMaker.GetIncomeRate() * upgradeCostFactor);
		upgradeButton.interactable = balance >= currentUpgradeCost && upgradeCount < maximumUpgradeCount;
		costTxt.text = currentUpgradeCost.ToString();
	}

	public void Upgrade()
	{
		float currentIncomeRate = boundIncomeMaker.GetIncomeRate();
		moneyStorage.ChangeBalance(currentIncomeRate * upgradeCostFactor);
		boundIncomeMaker.SetIncomeRate(currentIncomeRate * upgradeFactor);
		upgradeCount++;
		UpdateUpgraderAvailability(moneyStorage.GetBalance());
	}
}
