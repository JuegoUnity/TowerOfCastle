using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievement")]
public class Achievement : ScriptableObject
{
/// <summary>
/// Creamos los objetos para poder referenciar despues los achievements
/// </summary>
    public string ID;
    public string Title;
    public int ProgressToUnlock;
    public int GoldReward;
    public Sprite Sprite;

    public bool IsUnlocked { get; set; }

    private int CurrentProgress;

/// <summary>
/// 
/// </summary>
    public void AddProgress(int amount)
    {
        CurrentProgress += amount;
        AchievementManager.OnProgressUpdate?.Invoke(this);
        CheckUnlockStatus();
    }
/// <summary>
/// Funcion que desbloquea un logro
/// </summary>

    private void CheckUnlockStatus()
    {
        if (CurrentProgress >= ProgressToUnlock)
        {
            UnlockAchievement();
        }
    }
/// <summary>
/// Funcion que nos permite desbloquear un logro
/// </summary>
    private void UnlockAchievement()
    {
        IsUnlocked = true;
        AchievementManager.OnAchievementUnLocked?.Invoke(this);
    }
/// <summary>
/// Funcion que nos cuenta el progreso que llevamos de los logros
/// </summary>
    public string GetProgress()
    {
        return $"{CurrentProgress}/{ProgressToUnlock}";
    }
/// <summary>
/// Funcion que nos permite recibir el logro
/// </summary>
    public string GetProgressCompleted()
    {
        return $"{ProgressToUnlock}/{ProgressToUnlock}";
    }
/// <summary>
/// Llamos a los objetos
/// </summary>
    private void OnEnable() 
    {
        IsUnlocked = false;
        CurrentProgress = 0;
    }
}
