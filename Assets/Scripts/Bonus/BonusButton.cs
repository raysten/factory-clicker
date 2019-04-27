using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BonusButton : MonoBehaviour
{
	[SerializeField] private Text title;
	[SerializeField] private Text cost;
	private BonusManager manager;

	//[Inject]
	//public void Construct(BonusManager manager)
	//{
	//	this.manager = manager;
	//}

	public class Pool : MemoryPool<BonusesSettings.BonusSettings, BonusButton>
	{
		protected override void Reinitialize(BonusesSettings.BonusSettings settings, BonusButton item)
		{
			item.title.text = settings.Title;
			item.cost.text = settings.Cost != 0f ? settings.Cost.ToString() : "FREE";
		}
	}
}
