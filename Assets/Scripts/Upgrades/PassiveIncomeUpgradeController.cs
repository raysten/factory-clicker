using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PassiveIncomeUpgradeController : IncomeUpgradeController
{
	[SerializeField] private Button upgradeButton;
	[SerializeField] private Text costTxt;
	private PassiveIncomeSettings passiveIncSettings;
	private float upgradeCost;
	private int upgradeCount;

	[Inject]
	public void Construct(
		PassiveIncomeSettings passiveIncSettings,
		[Inject (Id = "passive")]
		IIncomeMaker incomeMaker
	)
	{
		this.passiveIncSettings = passiveIncSettings;
		this.boundIncomeMaker = incomeMaker;
		this.upgradeCost = passiveIncSettings.InitialIncomeUpgradeCost;
	}

	protected override void UpdateUpgraderAvailability(float balance)
	{
		upgradeButton.interactable =
			balance >= upgradeCost &&
			upgradeCount < passiveIncSettings.MaximumIncomeUpgradeCount;
		costTxt.text = upgradeCount < passiveIncSettings.MaximumIncomeUpgradeCount ?
			upgradeCost.ToString() :
			"MAXED";
	}

	public override void Upgrade()
	{
		moneyStorage.ChangeBalance(-upgradeCost);
		boundIncomeMaker.SetIncomeRate(boundIncomeMaker.GetIncomeRate() + passiveIncSettings.IncomeUpgradeIncrease);
		upgradeCost += passiveIncSettings.IncomeUpgradeCostChange;
		upgradeCount++;
		UpdateUpgraderAvailability(moneyStorage.GetBalance());
	}
}
