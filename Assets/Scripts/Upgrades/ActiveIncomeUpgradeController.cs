using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ActiveIncomeUpgradeController : IncomeUpgradeController
{
	[SerializeField] private Button upgradeButton;
	[SerializeField] private Text costTxt;
	private ActiveIncomeSettings activeIncSettings;
	private int upgradeCount;

	[Inject]
	public void Construct(
		ActiveIncomeSettings activeIncSettings,
		[Inject(Id = "active")]
		IIncomeMaker incomeMaker
	)
	{
		this.activeIncSettings = activeIncSettings;
		this.boundIncomeMaker = incomeMaker;
	}

	protected override void UpdateUpgraderAvailability(float balance)
	{
		float currentUpgradeCost = Mathf.Abs(boundIncomeMaker.GetIncomeRate() * activeIncSettings.UpgradeCostFactor);
		upgradeButton.interactable = balance >= currentUpgradeCost && upgradeCount < activeIncSettings.MaximumUpgradeCount;
		costTxt.text = upgradeCount < activeIncSettings.MaximumUpgradeCount ?
		currentUpgradeCost.ToString() : "MAXED";
	}

	public override void Upgrade()
	{
		float currentIncomeRate = boundIncomeMaker.GetIncomeRate();
		moneyStorage.ChangeBalance(currentIncomeRate * -activeIncSettings.UpgradeCostFactor);
		boundIncomeMaker.SetIncomeRate(currentIncomeRate * activeIncSettings.UpgradeFactor);
		upgradeCount++;
		UpdateUpgraderAvailability(moneyStorage.GetBalance());
	}
}
