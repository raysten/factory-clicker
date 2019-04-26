using UnityEngine;
using System;
using Zenject;

public class WinCondition : IInitializable, IDisposable
{
	private SignalBus signalBus;
	private float winAmount;
	private readonly HighscoreController highscoreController;
	private readonly WinPanel winPanel;

	public WinCondition(
		SignalBus signalBus,
		WinSettings settings,
		HighscoreController highscoreController,
		WinPanel winPanel
	)
	{
		this.signalBus = signalBus;
		this.winAmount = settings.WinAmount;
		this.highscoreController = highscoreController;
		this.winPanel = winPanel;
	}

	public void Initialize()
	{
		signalBus.Subscribe<BalanceChangedSignal>(OnBalanceChanged);
	}

	public void Dispose()
	{
		signalBus.Unsubscribe<BalanceChangedSignal>(OnBalanceChanged);
	}

	private void OnBalanceChanged(BalanceChangedSignal balanceChangedInfo)
	{
		if (balanceChangedInfo.Amount >= winAmount)
		{
			Time.timeScale = 0f; // TODO:
			highscoreController.OnGameWon();
			winPanel.Show();
		}
	}
}
