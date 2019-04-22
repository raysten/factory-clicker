using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Factory/Game Settings")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
	public ActiveIncomeSettings ActiveIncomeSettings;

	public override void InstallBindings()
	{
		Container.BindInstance(ActiveIncomeSettings);
	}
}
