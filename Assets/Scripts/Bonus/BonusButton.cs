using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BonusButton : MonoBehaviour
{
	[SerializeField] private Text title;
	[SerializeField] private Text cost;
	[SerializeField] private Button btn;
	private BonusManager manager;
	private BonusesSettings.BonusSettings settings;

	[Inject]
	public void Construct(BonusManager manager)
	{
		this.manager = manager;
	}

	public void Execute()
	{
		manager.ProcessBonus(settings, this);
	}

	public void Enable()
	{
		btn.interactable = true;
	}

	public void Disable()
	{
		btn.interactable = false;
	}

	public class Pool : MemoryPool<BonusesSettings.BonusSettings, BonusButton>
	{
		protected override void Reinitialize(BonusesSettings.BonusSettings settings, BonusButton item)
		{
			item.settings = settings;
			item.title.text = settings.Title;
			item.cost.text = settings.Cost != 0f ? settings.Cost.ToString() : "FREE";
		}
	}
}
