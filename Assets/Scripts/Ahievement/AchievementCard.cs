using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementCard : MonoBehaviour
{
    [SerializeField] private Image achievementImage;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI progress;
    [SerializeField] private TextMeshProUGUI reward;    
    [SerializeField] private Button rewardButton;


    public Achievement AchievementLoaded { get; set; }

/// <summary>
/// Metodo que nos mostrara todos los apartados de nuestro panel de logros
/// </summary>
    public void SetupAchievement(Achievement achievement)
    {
        AchievementLoaded = achievement;
        achievementImage.sprite = achievement.Sprite;
        title.text = achievement.Title;
        progress.text = achievement.GetProgress();
        reward.text = achievement.GoldReward.ToString();
    }
/// <summary>
/// Funcion que nos permite obtener la recompensa de nuestros logros y añadir las coins.
/// </summary>
    public void GetReward()
    {
        if (AchievementLoaded.IsUnlocked)
        {
            CurrencySystem.Instance.AddCoins(AchievementLoaded.GoldReward);
            rewardButton.gameObject.SetActive(false);
        }
    }
/// <summary>
/// Funcion que nos permite registrar y cargar cuantos enemigos muertos o oleadas tenemos y verlos en la pantalla de logros
/// </summary>
    private void LoadAchievementsProgress()
    {
        if (AchievementLoaded.IsUnlocked)
        {
            progress.text = AchievementLoaded.GetProgressCompleted();
        }
        else
        {
            progress.text = AchievementLoaded.GetProgress();
        }
    }
/// <summary>
/// Funcion que nos permite poder ver si el boton esta listo para poder activarse
/// </summary>
    private void CheckRewardButtonStatus()
    {
        if (AchievementLoaded.IsUnlocked)
        {
            rewardButton.interactable = true;
        }
        else
        {
            rewardButton.interactable = false;
        }
    }
/// <summary>
/// Actulaiza el progreso de los logros
/// </summary>
    private void UpdateProgress(Achievement achievementWithProgress)
    {
        if (AchievementLoaded == achievementWithProgress)
        {
            LoadAchievementsProgress();
        }
    } 
/// <summary>
/// Funcion que nos permite recoger las monedas
/// </summary>
    private void AchievementUnlocked(Achievement achievement)
    {
        if (AchievementLoaded == achievement)
        {
            CheckRewardButtonStatus();
        }
    }
    private void OnEnable() 
    {
        CheckRewardButtonStatus();
        LoadAchievementsProgress();
        AchievementManager.OnProgressUpdate += UpdateProgress;
        AchievementManager.OnAchievementUnLocked += AchievementUnlocked;

    }

    private void OnDisable() 
    {
        AchievementManager.OnProgressUpdate -= UpdateProgress;
        AchievementManager.OnAchievementUnLocked -= AchievementUnlocked;
    }
    
        
    
}
