using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievement")]
public class Achievement : ScriptableObject
{
    public string ID;
    public string Title;
    public int ProgressToUnlock;
    public int GoldReward;
    public Sprite Sprite;

    private int CurrentProgress;

    public void AddProgress(int amount)
    {
        CurrentProgress += amount;
        AchievementManager.OnProgressUpdate?.Invoke(this);
        CheckUnlockStatus();
    }

    private void CheckUnlockStatus()
    {
        if (CurrentProgress >= ProgressToUnlock)
        {
            UnlockAchievement();
        }
    }

    private void UnlockAchievement()
    {
        AchievementManager.OnAchievementUnLocked?.Invoke(this);
    }

    public string GetProgress()
    {
        return $"{CurrentProgress}/{ProgressToUnlock}";
    }

    private void OnEnable() 
    {
        CurrentProgress = 0;
    }
}
