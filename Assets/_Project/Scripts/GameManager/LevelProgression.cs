
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public bool Unlocked { get; set; } = false;
    public bool Completed { get; set; } = false;
}

public static class LevelProgression
{
    public static event Action OnProgressChanged;

    private static List<LevelData> _levels = new List<LevelData>();

    public static List<LevelData> GetLevels() => _levels;

    public static void SetLevels(List<LevelData> levels)
    {
        _levels = levels;
        if (_levels.Count == 0)
        {
            _levels.Add(new LevelData { Unlocked = true, Completed = false });
        }
    }

    public static void CompleteLevel(int levelIndex)
    {
        EnsureLevelExists(levelIndex);
        _levels[levelIndex].Completed = true;

        int nextLevelIndex = levelIndex + 1;
        if (nextLevelIndex <= _levels.Count)
        {
            _levels[nextLevelIndex].Unlocked = true;
        }
        else
        {
            _levels.Add(new LevelData { Unlocked = true, Completed = false });
        }
       
        OnProgressChanged?.Invoke();
    }

    public static bool IsUnlocked(int levelIndex)
    {
        EnsureLevelExists(levelIndex);
        return _levels[levelIndex].Unlocked;
    }

    public static bool IsCompleted(int levelIndex)
    {
        EnsureLevelExists(levelIndex);
        return _levels[levelIndex].Completed;
    }

    private static void EnsureLevelExists(int levelIndex)
    {
        while (_levels.Count <= levelIndex)
        {
            _levels.Add(new LevelData());
        }

        if (_levels.Count > 0)
        {
            _levels[0].Unlocked = true;
        }
    }
}