using System;

[Serializable]
public class PassiveIncomeSettings
{
	public float InitialIncomeRate;
	public float InitialInterval;
	public float IncomeUpgradeIncrease;
	public float InitialIncomeUpgradeCost;
	public float IncomeUpgradeCostChange;
	public float InitialIntervalUpgradeCost;
	public float FurtherIntervalUpgradeCost;
	public float[] IntervalUpgradeChanges;
	public int MaximumIncomeUpgradeCount;
}
