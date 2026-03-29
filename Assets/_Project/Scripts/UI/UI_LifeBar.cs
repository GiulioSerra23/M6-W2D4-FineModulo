
using UnityEngine;
using UnityEngine.UI;

public class UI_LifeBar : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private Image[] _hearts;
    [SerializeField] private Sprite _fullHeartSprite;
    [SerializeField] private Sprite _emptyHeartSprite;
    [SerializeField] private LifeController _lifeController;

    private void Start()
    {
        _lifeController.OnHpChanged += UpdateLifeBar;
    }

    public void UpdateLifeBar(int currentHp)
    {
        for (int i = 0; i < _hearts.Length; i++)
        {
            _hearts[i].sprite = (i < currentHp) ? _fullHeartSprite : _emptyHeartSprite;
        }
    }

    private void OnDestroy()
    {
        if (_lifeController != null) _lifeController.OnHpChanged -= UpdateLifeBar;
    }
}
