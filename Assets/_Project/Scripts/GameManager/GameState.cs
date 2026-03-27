
public class GameState : GenericSingleton<GameState>
{
    private bool _lifeReady;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        //LoadGame();
    }

    public void SetupRun()
    {

    }

    #region INITIALIZE

    private void OnEnable()
    {
        TryInit();

        //if (!_lifeReady) LifeController.OnSingletonReady += OnLifeReady;
    }

    private void TryInit()
    {
        //if (LifeController.Instance != null) _lifeReady = true;

        if (_lifeReady) Init();
    }

    private void Init()
    {
        //LifeController.Instance.OnDie += SaveState;

        SaveState();
    }

    private void OnLifeReady()
    {
        _lifeReady = true;
        TryInit();
    }

    private void OnPowerUpReady()
    {
        TryInit();
    }

    private void OnDisable()
    {

    }

    #endregion
    
    #region SAVING
    private void SaveState()
    {
        SavingSystem.Save();
    }

    public SaveData GetSaveData()
    {
        SaveData data = new SaveData();

        return data;
    }

    private void LoadGame()
    {
        SaveData data = SavingSystem.Load();

        if (data == null) return;
    }
    #endregion
}