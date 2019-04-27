using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class BonusGUI : MonoBehaviour
{
	[SerializeField] private Transform container;
	private BonusesSettings settings;
	private BonusButton.Pool buttonsPool;
	private List<BonusButton> buttons = new List<BonusButton>();

	[Inject]
	public void Construct(BonusesSettings settings, BonusButton.Pool buttonsPool)
	{
		this.settings = settings;
		this.buttonsPool = buttonsPool;
	}

	private void OnEnable()
	{
		Time.timeScale = 0f;

		if (buttons.Count == 0)
		{
			foreach (BonusesSettings.BonusSettings bonusData in settings.Bonuses)
			{
				BonusButton button = buttonsPool.Spawn(bonusData);
				button.transform.SetParent(container);
				buttons.Add(button);
			}
		}
	}

	private void OnDisable()
	{
		Time.timeScale = 1f;
	}
}
