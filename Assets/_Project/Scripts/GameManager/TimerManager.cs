
using System;
using UnityEngine;
using UnityEngine.Events;

public class TimerManager : GenericSingleton<TimerManager>
{
    [Header ("Event")]    
    [SerializeField] private UnityEvent _onTimeEnded;

    [Header ("Timer Settings")]
    [SerializeField] private float _timerDuration = 60f;

    public event Action<float> OnTimeChanged;
    public float TimeLeft { get; private set; }

    private void Start()
    {
        StartTimer(_timerDuration);
    }

    private void SetTime(float time)
    {
        TimeLeft = Mathf.Max(time, 0);
        TimeLeft = time;
    }

    public void AddTime(float amount)
    {
        SetTime(TimeLeft + amount);
    }

    public void StartTimer(float duration)
    {
        TimeLeft = duration;
    }

    private void UpdateTimer()
    {
        SetTime(TimeLeft -= Time.deltaTime);
        OnTimeChanged?.Invoke(TimeLeft);

        if (TimeLeft <= 0f)
        {
            _onTimeEnded.Invoke();
        }
    }

    private void Update()
    {
        UpdateTimer();
    }
}
