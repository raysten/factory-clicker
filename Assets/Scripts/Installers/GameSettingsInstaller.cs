using UnityEngine;
using Zenject;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Factory/Game Settings")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
	public ActiveIncomeSettings ActiveIncome;
	public PassiveIncomeSettings PassiveIncome;
	public SpecialEventsSettings SpecialEvents;
	public WinSettings WinCondition;

	public override void InstallBindings()
	{
		Container.BindInstance(ActiveIncome);
		Container.BindInstance(PassiveIncome);
		Container.BindInstance(SpecialEvents);
		Container.BindInstance(WinCondition);
	}
}
