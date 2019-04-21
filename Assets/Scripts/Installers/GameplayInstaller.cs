using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		InstallBankManager();
		InstallSignals();
	}

	private void InstallBankManager()
	{
		Container.BindInterfacesAndSelfTo<BankManager>().AsSingle();
	}

	private void InstallSignals()
	{
		SignalBusInstaller.Install(Container); // TODO: Do it on project context level

		Container.DeclareSignal<BalanceChangedSignal>();
	}
}
