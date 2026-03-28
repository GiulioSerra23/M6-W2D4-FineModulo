
using TMPro;
using UnityEngine;

public class UI_Timer : MonoBehaviour
{
    [Header ("Text")]
    [SerializeField] private TextMeshProUGUI _timerText;

    private void Start()
    {
        TimerManager.Instance.OnTimeChanged += UpdateTimerUI;
    }

    public void UpdateTimerUI(float time)
    {
        time = Mathf.Max(time, 0f);

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        _timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void OnDestroy()
    {
        if (TimerManager.Instance != null) TimerManager.Instance.OnTimeChanged -= UpdateTimerUI;
    }
}
