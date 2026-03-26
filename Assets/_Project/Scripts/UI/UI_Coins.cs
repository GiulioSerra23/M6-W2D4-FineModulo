
using TMPro;
using UnityEngine;

public class UI_Coins : MonoBehaviour
{
    [Header ("Text")]
    [SerializeField] private TextMeshProUGUI _coinText;

    public void UpdateCoinsUI(int coins)
    {
        _coinText.text = $"{coins}";
    }
}
