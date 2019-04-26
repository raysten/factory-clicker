using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour
{
    public void OnClick()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(1);
	}
}
