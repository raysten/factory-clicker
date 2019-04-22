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
	private ActiveIncomeSettings activeIncSettings;
	private int upgradeCount;

	[Inject]
    public void Construct(
		SignalBus signalBus,
		IIncomeMaker incomeMaker,
		IMoneyStorage moneyStorage,
		ActiveIncomeSettings activeIncSettings
	)
	{
		this.signalBus = signalBus;
		this.boundIncomeMaker = incomeMaker;
		this.moneyStorage = moneyStorage;
		this.activeIncSettings = activeIncSettings;
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

	private void UpdateUpgraderAvailability(float balance)
	{
		float currentUpgradeCost = Mathf.Abs(boundIncomeMaker.GetIncomeRate() * activeIncSettings.UpgradeCostFactor);
		upgradeButton.interactable = balance >= currentUpgradeCost && upgradeCount < activeIncSettings.MaximumUpgradeCount;
		costTxt.text = currentUpgradeCost.ToString();
	}

	public void Upgrade()
	{
		float currentIncomeRate = boundIncomeMaker.GetIncomeRate();
		moneyStorage.ChangeBalance(currentIncomeRate * -activeIncSettings.UpgradeCostFactor);
		boundIncomeMaker.SetIncomeRate(currentIncomeRate * activeIncSettings.UpgradeFactor);
		upgradeCount++;
		UpdateUpgraderAvailability(moneyStorage.GetBalance());
	}
}
