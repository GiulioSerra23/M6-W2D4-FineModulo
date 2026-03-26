
using TMPro;
using UnityEngine;

public class UI_Timer : MonoBehaviour
{
    [Header ("Text")]
    [SerializeField] private TextMeshProUGUI _timerText;

    public void UpdateTimerUI(float time)
    {
        time = Mathf.Max(time, 0f);

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        _timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
