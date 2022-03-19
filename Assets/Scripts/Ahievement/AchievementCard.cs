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

    public void SetupAchievement(Achievement achievement)
    {
        achievementImage.sprite = achievement.Sprite;
        title.text = achievement.Title;
        progress.text = achievement.ProgressToUnlock.ToString();
        reward.text = achievement.GoldReward.ToString();
    }
}
