
using UnityEngine;

public static class LevelProgression
{
    private static int _totalLevels = 2;

    public static void CompleteLevel(int levelIndex)
    {
        PlayerPrefs.SetInt($"Level_{levelIndex}_Completed", 1);

        int nextLevel = levelIndex + 1;
        if (nextLevel <= _totalLevels)
        {
            PlayerPrefs.SetInt($"Level_{nextLevel}_Unlocked", 1);
        }
       
        PlayerPrefs.Save();
    }

    public static bool IsUnlocked(int levelIndex)
    {
        if (levelIndex == 1) return true;

        return PlayerPrefs.GetInt($"Level_{levelIndex}_Unlocked", 0) == 1;
    }

    public static bool IsCompleted(int levelIndex)
    {
        return PlayerPrefs.GetInt($"Level_{levelIndex}_Completed", 0) == 1;
    }
}