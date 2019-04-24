using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class FasterPassiveIncomeUpgradeController : IncomeUpgradeController
{
	[SerializeField] private Button upgradeButton;
	[SerializeField] private Text costTxt;
	private IPeriodicalIncomeMaker periodicalIncomeMaker;
	private PassiveIncomeSettings passiveIncSettings;
	private float upgradeCost;
	private int upgradeCount;

	[Inject]
	public void Construct(
		PassiveIncomeSettings passiveIncSettings,
		[Inject (Id = "passive")]
		IPeriodicalIncomeMaker periodicalIncomeMaker
	)
	{
		this.passiveIncSettings = passiveIncSettings;
		this.periodicalIncomeMaker = periodicalIncomeMaker;
		this.upgradeCost = passiveIncSettings.InitialIntervalUpgradeCost;
	}

	protected override void UpdateUpgraderAvailability(float balance)
	{
		upgradeButton.interactable =
			balance >= upgradeCost &&
			upgradeCount < passiveIncSettings.IntervalUpgradeChanges.Length &&
			periodicalIncomeMaker.IsActive();
		costTxt.text = upgradeCount < passiveIncSettings.IntervalUpgradeChanges.Length ?
			upgradeCost.ToString() :
			"MAXED";
	}

	public override void Upgrade()
	{
		upgradeCount++;
		periodicalIncomeMaker.DecreaseInterval(passiveIncSettings.IntervalUpgradeChanges[upgradeCount - 1]);
		moneyStorage.ChangeBalance(-upgradeCost);
		upgradeCost = passiveIncSettings.FurtherIntervalUpgradeCost;
		UpdateUpgraderAvailability(moneyStorage.GetBalance());
	}
}
