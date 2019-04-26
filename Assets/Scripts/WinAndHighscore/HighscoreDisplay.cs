using UnityEngine;
using Zenject;
using TMPro;

public class HighscoreDisplay : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI scoreTxt;
	private IHighscoreStore highscoreStore;

	[Inject]
	public void Construct(IHighscoreStore highscoreStore)
	{
		this.highscoreStore = highscoreStore;
	}

	private void Start()
	{
		if (highscoreStore.LoadHighscore() == 0f)
		{
			scoreTxt.text = "N / A";
		} else
		{
			scoreTxt.text = (highscoreStore.LoadHighscore() / 60f).ToString("F2");
		}
	}
}
