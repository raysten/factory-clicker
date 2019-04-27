using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System.Collections.Generic;

public class SpecialEventOptionButton : MonoBehaviour
{
	[SerializeField] private Text title;
	private EventData.EventOption optionData;
	private ISpecialEventHandler handler;

	[Inject]
    public void Construct(ISpecialEventHandler handler)
	{
		this.handler = handler;
	}

	public void Execute()
	{
		float randomChance = Random.Range(0f, 100f);
		List<PercentageRange> ranges = CreatePercentageRangesOfPossibleOutcomes();

		for (int i = 0; i < optionData.Outcomes.Count; i++)
		{
			if (randomChance >= ranges[i].Low && randomChance <= ranges[i].High)
			{
				handler.ProcessOutcome(optionData.Outcomes[i]);
				break;
			}
		}
	}

	private List<PercentageRange> CreatePercentageRangesOfPossibleOutcomes()
	{
		List<PercentageRange> ranges = new List<PercentageRange>();

		for (int i = 0; i < optionData.Outcomes.Count; i++)
		{
			if (i == 0)
			{
				ranges.Add(new PercentageRange(0, optionData.Outcomes[i].Probability));
			}
			else
			{
				ranges.Add(new PercentageRange(ranges[i - 1].High, ranges[i - 1].High + optionData.Outcomes[i].Probability));
			}
		}

		return ranges;
	}

	public class Pool : MemoryPool<EventData.EventOption, SpecialEventOptionButton>
	{
		protected override void Reinitialize(EventData.EventOption data, SpecialEventOptionButton option)
		{
			option.gameObject.SetActive(true);
			option.optionData = data;
			option.title.text = data.OptionName;
		}
	}

	public class PercentageRange
	{
		public float Low;
		public float High;

		public PercentageRange(float low, float high)
		{
			this.Low = low;
			this.High = high;
		}
	}
}
