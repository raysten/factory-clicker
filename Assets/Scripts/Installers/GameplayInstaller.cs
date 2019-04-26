using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
	public SpecialEventOptionButton EventOptionButtonPrefab; // TODO:

	public override void InstallBindings()
	{
		InstallBankManager();
		InstallEventOptionsPool();
		InstallSignals();
	}

	private void InstallBankManager()
	{
		Container.BindInterfacesAndSelfTo<BankManager>().AsSingle();
	}

	private void InstallEventOptionsPool()
	{
		Container.BindMemoryPool<SpecialEventOptionButton, SpecialEventOptionButton.Pool>()
			.WithInitialSize(3)
			.FromComponentInNewPrefab(EventOptionButtonPrefab)
			.UnderTransformGroup("EventOptionButtonsPool");
	}

	private void InstallSignals()
	{
		SignalBusInstaller.Install(Container); // TODO: Do it on project context level

		Container.DeclareSignal<BalanceChangedSignal>();
	}
}
