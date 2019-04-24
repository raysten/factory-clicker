using UnityEngine;
using Zenject;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Factory/Game Settings")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
	public ActiveIncomeSettings ActiveIncomeSettings;
	public PassiveIncomeSettings PassiveIncomeSettings;
	public List<EventData> SpecialEvents;

	public override void InstallBindings()
	{
		Container.BindInstance(ActiveIncomeSettings);
		Container.BindInstance(PassiveIncomeSettings);
		Container.BindInstance(SpecialEvents); // TODO: Bind list?
	}
}
