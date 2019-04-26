using UnityEngine;
using Zenject;
using TMPro;
using System.Collections.Generic;

public class SpecialEventPanel : MonoBehaviour, ISpecialEventHandler
{
	[SerializeField] private TextMeshProUGUI title;
	[SerializeField] private TextMeshProUGUI description;
	[SerializeField] private Transform buttonsContainer;
	[SerializeField] private GameObject closeButton;
	private SpecialEventOptionButton.Pool optionButtonPool;
	private IMoneyStorage moneyStorage;
	private List<SpecialEventOptionButton> optionButtons = new List<SpecialEventOptionButton>();

	[Inject]
	public void Construct(SpecialEventOptionButton.Pool optionButtonPool, IMoneyStorage moneyStorage)
	{
		this.optionButtonPool = optionButtonPool;
		this.moneyStorage = moneyStorage;
	}

	public void HandleEvent(EventData eventData)
	{
		closeButton.SetActive(false);
		gameObject.SetActive(true);
		title.text = eventData.Title;
		description.text = eventData.Description;
		optionButtons.Clear();

		foreach (EventData.EventOption optionData in eventData.Options)
		{
			SpecialEventOptionButton option = optionButtonPool.Spawn(optionData);
			option.transform.SetParent(buttonsContainer);
			optionButtons.Add(option);
		}

		Time.timeScale = 0f;
	}

	public void ProcessOutcome(EventData.EventOutcome outcome)
	{
		description.text = outcome.Description;
		moneyStorage.ChangeBalance(outcome.AbsoluteMoneyChange);
		float percentageBalanceChange = moneyStorage.GetBalance() * outcome.PercentageMoneyChange / 100f;
		moneyStorage.ChangeBalance(percentageBalanceChange);
		
		foreach (SpecialEventOptionButton option in optionButtons)
		{
			option.gameObject.SetActive(false);
			optionButtonPool.Despawn(option);
		}

		closeButton.SetActive(true);
	}

	public void EndEvent()
	{
		Time.timeScale = 1f;
		gameObject.SetActive(false);
	}
}
