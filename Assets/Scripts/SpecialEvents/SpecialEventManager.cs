using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpecialEventManager : MonoBehaviour
{
	private SpecialEventsSettings settings;
	private ISpecialEventHandler handler;
	private float eventCheckInterval = 1f;

	[Inject]
	public void Construct(SpecialEventsSettings settings, ISpecialEventHandler handler)
	{
		this.settings = settings;
		this.handler = handler;
	}

	private void Start()
	{
		StartCoroutine(SpecialEventActivator());
	}

	private IEnumerator SpecialEventActivator()
	{
		while (true) // TODO:
		{
			yield return new WaitForSeconds(eventCheckInterval);

			for (int i = 0; i < settings.Events.Count; i++)
			{
				int randomChance = Random.Range(0, 100);

				if (randomChance <= settings.Events[i].Probability)
				{
					handler.HandleEvent(settings.Events[i]);
					break;
				}
			}
		}
	}
}
