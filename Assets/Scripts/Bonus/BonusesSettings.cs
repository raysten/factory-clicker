using System;

[Serializable]
public class BonusesSettings
{
	public BonusSettings ActiveIncome;
	public BonusSettings PassiveIncome;
	public BonusSettings PassiveInterval;

	[Serializable]
    public class BonusSettings
	{
		public float Percentage;
		public float Duration;
		public string Title;
		public float Cost;
	}
}
