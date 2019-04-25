using UnityEngine;
using Zenject;
using TMPro;

public class SpecialEventPanel : MonoBehaviour, ISpecialEventHandler
{
	[SerializeField] private TextMeshProUGUI title;
	[SerializeField] private TextMeshProUGUI description;
	[SerializeField] private Transform buttonsContainer;
	SpecialEventOptionButton.Pool optionButtonPool;

	[Inject]
	public void Construct(SpecialEventOptionButton.Pool optionButtonPool)
	{
		this.optionButtonPool = optionButtonPool;
	}

	public void HandleEvent(EventData eventData)
	{
		gameObject.SetActive(true);
		title.text = eventData.Title;
		description.text = eventData.Description;

		foreach (EventData.EventOption optionData in eventData.Options)
		{
			SpecialEventOptionButton option = optionButtonPool.Spawn(optionData);
			option.transform.SetParent(buttonsContainer);
		}

		Time.timeScale = 0f; // TODO:
	}

	// TODO: Hide the panel, despawn options.
}
