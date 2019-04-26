using UnityEngine;
using Zenject;

public class HighscoreController : IInitializable
{
	private IHighscoreStore highscoreStore;
	private float gameStartTime;

	public HighscoreController(IHighscoreStore highscoreStore)
	{
		this.highscoreStore = highscoreStore;
	}

	public void OnGameWon()
	{
		float wholeGameTime = Time.time - gameStartTime;

		if (highscoreStore.LoadHighscore() < wholeGameTime)
		{
			highscoreStore.SaveHighscore(wholeGameTime);
		}
	}

	public void Initialize()
	{
		gameStartTime = Time.time;
	}
}
