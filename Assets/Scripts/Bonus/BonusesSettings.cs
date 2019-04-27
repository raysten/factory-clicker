using System;

[Serializable]
public class BonusesSettings
{
	public BonusSettings[] Bonuses;

	[Serializable]
    public class BonusSettings
	{
		public float Percentage;
		public float Duration;
		public string Title;
		public float Cost;
		public BONUS_TYPE Type;
	}

	public enum BONUS_TYPE { ACTIVE_INCOME, PASSIVE_INCOME, PASSIVE_INTERVAL }
}
