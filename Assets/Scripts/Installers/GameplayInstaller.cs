using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
	public SpecialEventOptionButton EventOptionButtonPrefab;

	public override void InstallBindings()
	{
		InstallBankManager();
		InstallEventOptionsPool();
		InstallSignals();
		InstallWinCondtion();
		InstallHighscore();
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
		SignalBusInstaller.Install(Container);
		Container.DeclareSignal<BalanceChangedSignal>();
	}

	private void InstallWinCondtion()
	{
		Container.BindInterfacesTo<WinCondition>().AsSingle();
	}

	private void InstallHighscore()
	{
		//Container.BindInterfacesTo<HighscoreIO>().AsSingle();
		Container.Bind<HighscoreController>().AsSingle();
	}
}
