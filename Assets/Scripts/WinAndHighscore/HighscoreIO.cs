using UnityEngine;
using Zenject;
using System;

public class HighscoreIO : IHighscoreStore
{
	private string prefsKey = "Highscore";

	public HighscoreIO()
	{
		
	}

	public float LoadHighscore()
	{
		return PlayerPrefs.GetFloat(prefsKey, 0f);
	}

	public void SaveHighscore(float score)
	{
		PlayerPrefs.SetFloat(prefsKey, score);
	}
}
