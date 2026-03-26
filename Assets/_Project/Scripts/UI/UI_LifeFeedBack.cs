
using UnityEngine;
using UnityEngine.UI;

public class UI_LifeFeedBack : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private Image _overlay;
    [SerializeField] private LifeController _lifeController;

    [Header("Settings")]
    [SerializeField] private Color _hitColor = new Color(1f, 0f, 0f, 0.3f);
    [SerializeField] private Color _healColor = new Color(0f, 1f, 0f, 0.3f);
    [SerializeField] private float _overlayDuration = 1;

    private float _overlayStartTime;
    private int _lastHp;

    private void Start()
    {
        StartHp(_lifeController.CurrentHp);
    }

    private bool CanActivate() => Time.time - _overlayStartTime >= _overlayDuration;

    private void StartHp(int startingHp) => _lastHp = startingHp;

    public void OnHpChanged(int currentHp)
    {
        int delta = currentHp - _lastHp;
        _lastHp = currentHp;
        
        if (delta == 0 || !CanActivate()) return;

        Color targetColor = delta < 0 ? _hitColor : _healColor;
        _overlay.color = targetColor;

        _overlay.gameObject.SetActive(true);
        _overlayStartTime = Time.time;
    }

    public void Update()
    {
        if (CanActivate())
        {
            _overlay.gameObject.SetActive(false);
        }
    }
}
