using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{
    public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Close()
	{
		SceneManager.LoadScene(0);
	}
}
