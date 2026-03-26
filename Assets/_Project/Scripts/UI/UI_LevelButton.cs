
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_LevelButton : MonoBehaviour
{
    [Header ("Level Info")]
    [SerializeField] private int _levelIndex;
    [SerializeField] private string _sceneName;

    [Header ("UI")]
    [SerializeField] private GameObject _lockOverlay;
    [SerializeField] private Image _levelNumberImage;
    [SerializeField] private Image _stars;

    [Header("Start Sprites")]
    [SerializeField] private Sprite _emptyStars;
    [SerializeField] private Sprite _fullStars;

    private Button _button;
    private bool _isUnlocked;
    private bool _isCompleted;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        if (_levelIndex == 1) PlayerPrefs.SetInt("LevelUnlocked_1", 1);

        SetupButton();
        RefreshVisualState();
    }

    private void SetupButton()
    {        
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OnButtonPressed);
    }

    private void RefreshVisualState()
    {
        _isUnlocked = LevelProgression.IsUnlocked(_levelIndex);
        _isCompleted = LevelProgression.IsCompleted(_levelIndex);

        _lockOverlay.SetActive(!_isUnlocked);

        _button.interactable = _isUnlocked;

        _stars.sprite = _isCompleted ? _fullStars : _emptyStars;

        _levelNumberImage.enabled = _isUnlocked;
    }

    private void OnButtonPressed()
    {
        if (!_isUnlocked) return;

        SceneManager.LoadScene(_sceneName);
    }
}
