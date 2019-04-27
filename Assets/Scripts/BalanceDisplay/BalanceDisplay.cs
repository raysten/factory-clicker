using UnityEngine;
using TMPro;
using Zenject;

public class BalanceDisplay : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI balanceTxt;
	private SignalBus signalBus;

	[Inject]
	public void Construct(SignalBus signalBus)
	{
		this.signalBus = signalBus;
	}

	private void Start()
	{
		signalBus.Subscribe<BalanceChangedSignal>(OnBalanceChanged);
	}

	private void OnDestroy()
	{
		signalBus.Unsubscribe<BalanceChangedSignal>(OnBalanceChanged);
	}

	private void OnBalanceChanged(BalanceChangedSignal balanceChangedInfo)
	{
		balanceTxt.text = balanceChangedInfo.Amount.ToString("F2");
	}
}
