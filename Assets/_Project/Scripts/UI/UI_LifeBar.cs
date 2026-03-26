
using UnityEngine;
using UnityEngine.UI;

public class UI_LifeBar : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private Image[] _hearts;
    [SerializeField] private Sprite _fullHeartSprite;
    [SerializeField] private Sprite _emptyHeartSprite;

    public void UpdateLifeBar(int currentHp)
    {
        for (int i = 0; i < _hearts.Length; i++)
        {
            _hearts[i].sprite = (i < currentHp) ? _fullHeartSprite : _emptyHeartSprite;
        }
    }
}
