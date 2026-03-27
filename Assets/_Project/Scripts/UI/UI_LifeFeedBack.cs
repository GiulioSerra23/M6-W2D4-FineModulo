
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_LifeFeedBack : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private Image _overlay;

    [Header("Settings")]
    [SerializeField] private Color _hitColor = new Color(1f, 0f, 0f, 0.3f);
    [SerializeField] private Color _healColor = new Color(0f, 1f, 0f, 0.3f);
    [SerializeField] private float _overlayDuration = 1;

    private Coroutine _overlayRoutine;

    private int _lastHp;

    private void OnEnable()
    {
        LifeController.Instance.OnHpChanged += OnHpChanged;
    }

    private void Start()
    {
        StartHp(LifeController.Instance.CurrentHp);
    }

    private void StartHp(int startingHp) => _lastHp = startingHp;

    public void OnHpChanged(int currentHp)
    {
        int delta = currentHp - _lastHp;
        _lastHp = currentHp;
        
        if (delta == 0) return;

        Color targetColor = delta < 0 ? _hitColor : _healColor;
        _overlay.color = targetColor;

        if (_overlayRoutine != null) StopCoroutine(_overlayRoutine);

        _overlayRoutine = StartCoroutine(OverlayRoutine());
    }

    private IEnumerator OverlayRoutine()
    {
        _overlay.gameObject.SetActive(true);

        yield return new WaitForSeconds(_overlayDuration);

        _overlay.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (LifeController.Instance != null) LifeController.Instance.OnHpChanged -= OnHpChanged;
    }
}
