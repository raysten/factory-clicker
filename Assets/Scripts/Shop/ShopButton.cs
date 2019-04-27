using UnityEngine;

public class ShopButton : MonoBehaviour
{
	[SerializeField] private GameObject shopPanel;

    public void Execute()
	{
		shopPanel.SetActive(true);
	}
}
