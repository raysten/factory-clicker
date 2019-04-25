using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SpecialEventOptionButton : MonoBehaviour
{
	[SerializeField] private Text title;
	private EventData.EventOption optionData;
	private IMoneyStorage moneyStorage;

	[Inject]
    public void Construct(IMoneyStorage moneyStorage)
	{
		this.moneyStorage = moneyStorage;
	}

	public void Execute()
	{

	}

	public class Pool : MemoryPool<EventData.EventOption, SpecialEventOptionButton>
	{
		protected override void Reinitialize(EventData.EventOption data, SpecialEventOptionButton option)
		{
			option.optionData = data;
			option.title.text = data.OptionName;
		}
	}
}
