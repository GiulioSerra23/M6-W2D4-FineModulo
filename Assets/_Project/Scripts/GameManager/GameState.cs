
using System.Collections.Generic;
using UnityEditor.Tilemaps;

public class GameState : GenericSingleton<GameState>
{
    protected override bool ShouldBeDestroyedOnLoad { get; set ; } = false;

    protected override void Awake()
    {
        base.Awake();
        LoadGame();
    }

    private void OnEnable()
    {
        LevelProgression.OnProgressChanged += SaveState;
    }

    private void OnDisable()
    {
        LevelProgression.OnProgressChanged -= SaveState;
    }

    #region SAVING
    private void SaveState()
    {
        SavingSystem.Save();
    }

    public SaveData GetSaveData()
    {
        SaveData data = new SaveData();

        data.Levels = LevelProgression.GetLevels();

        return data;
    }

    private void LoadGame()
    {
        SaveData data = SavingSystem.Load();

        if (data == null)
        {
            LevelProgression.SetLevels(new List<LevelData> { new LevelData { Unlocked = true, Completed = false } });
            SaveState();
            return;
        }

        LevelProgression.SetLevels(data.Levels);
    }
    #endregion
}