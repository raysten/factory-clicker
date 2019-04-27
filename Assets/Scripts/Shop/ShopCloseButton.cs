using UnityEngine;

public class ShopCloseButton : MonoBehaviour
{
	[SerializeField] private GameObject shopPanel;

    public void Execute()
	{
		shopPanel.SetActive(false);
	}
}
