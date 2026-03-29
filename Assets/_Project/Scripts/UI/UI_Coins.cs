
using TMPro;
using UnityEngine;

public class UI_Coins : MonoBehaviour
{
    [Header ("Text")]
    [SerializeField] private TextMeshProUGUI _coinText;

    private void Start()
    {
        CoinsManager.Instance.OnCoinsChanged += UpdateCoinsUI;
    }

    public void UpdateCoinsUI(int coins)
    {
        _coinText.SetText(coins.ToString());
    }

    private void OnDestroy()
    {
        if (CoinsManager.Instance != null) CoinsManager.Instance.OnCoinsChanged -= UpdateCoinsUI;
    }
}
