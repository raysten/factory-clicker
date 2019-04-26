using UnityEngine;
using System;
using Zenject;

public class WinCondition : IInitializable, IDisposable
{
	private SignalBus signalBus;
	private float winAmount;
	private readonly HighscoreController highscoreController;

    public WinCondition(SignalBus signalBus, WinSettings settings, HighscoreController highscoreController)
	{
		this.signalBus = signalBus;
		this.winAmount = settings.WinAmount;
		this.highscoreController = highscoreController;
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
			// TODO: End game.
			Time.timeScale = 0f; // TODO:
			highscoreController.OnGameWon();
		}
	}
}
