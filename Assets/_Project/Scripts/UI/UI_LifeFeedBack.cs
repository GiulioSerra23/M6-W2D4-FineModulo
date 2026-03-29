
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_LifeFeedBack : GenericSingleton<UI_LifeFeedBack>
{
    [Header ("References")]
    [SerializeField] private Image _overlay;
    [SerializeField] private LifeController _lifeController;

    [Header("Settings")]
    [SerializeField] private Color _hitColor = new Color(1f, 0f, 0f, 0.3f);
    [SerializeField] private Color _healColor = new Color(0f, 1f, 0f, 0.3f);
    [SerializeField] private float _overlayDuration = 1;

    private Coroutine _overlayRoutine;
    private bool _ignoreNextHpChange;
    private int _lastHp;

    private void Start()
    {
        StartHp(_lifeController.CurrentHp);
        _lifeController.OnHpChanged += OnHpChanged;
    }

    private void StartHp(int startingHp) => _lastHp = startingHp;

    public void OnHpChanged(int currentHp)
    {
        if (_ignoreNextHpChange)
        {
            _ignoreNextHpChange = false;
            _lastHp = currentHp;
            return;
        }

        int delta = currentHp - _lastHp;
        _lastHp = currentHp;
        
        if (delta == 0) return;

        Color targetColor = delta < 0 ? _hitColor : _healColor;
        _overlay.color = targetColor;

        if (_overlayRoutine != null) StopCoroutine(_overlayRoutine);

        _overlayRoutine = StartCoroutine(OverlayRoutine());
    }

    public void ResetFeedBack(int newHp)
    {
        _lastHp = newHp;
        _ignoreNextHpChange = true;
    }

    private IEnumerator OverlayRoutine()
    {
        _overlay.gameObject.SetActive(true);

        yield return new WaitForSeconds(_overlayDuration);

        _overlay.gameObject.SetActive(false);
        _overlayRoutine = null;
    }

    protected override void OnDestroy()
    {
        if (_lifeController != null) _lifeController.OnHpChanged -= OnHpChanged;
    }
}
