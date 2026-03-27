
using UnityEngine;
using UnityEngine.UI;

public class UI_LifeBar : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private Image[] _hearts;
    [SerializeField] private Sprite _fullHeartSprite;
    [SerializeField] private Sprite _emptyHeartSprite;

    private void OnEnable()
    {
        LifeController.Instance.OnHpChanged += UpdateLifeBar;
    }

    public void UpdateLifeBar(int currentHp)
    {
        for (int i = 0; i < _hearts.Length; i++)
        {
            _hearts[i].sprite = (i < currentHp) ? _fullHeartSprite : _emptyHeartSprite;
        }
    }

    private void OnDisable()
    {
        if (LifeController.Instance != null) LifeController.Instance.OnHpChanged -= UpdateLifeBar;
    }
}
