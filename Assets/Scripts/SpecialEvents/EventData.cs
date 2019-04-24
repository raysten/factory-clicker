using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Factory/SpecialEvents")]
public class EventData : ScriptableObject
{
	public string Title;
	public string Description;
	public float Probability; // In percents, % chance of an event happening during 1 second period.
	public List<EventOption> Options;

	[Serializable]
	public class EventOption
	{
		public string OptionName;
		public List<EventOutcome> Outcomes;
	}

	[Serializable]
	public class EventOutcome
	{
		public float Probability;
		public float PercentageMoneyChange;
		public float AbsoluteMoneyChange;
		public string Description;
	}
}
