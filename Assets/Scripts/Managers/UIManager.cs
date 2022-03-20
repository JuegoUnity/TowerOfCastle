using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : Singleton<UIManager>
{
    [Header("Panels")]
    [SerializeField] private GameObject turretShopPanel;
    [SerializeField] private GameObject nodeUIPanel;
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private GameObject gameOverPanel;




    [Header("Text")]
    [SerializeField] private TextMeshProUGUI upgradeText;
    [SerializeField] private TextMeshProUGUI sellText;
    [SerializeField] private TextMeshProUGUI turretLevelText;
    [SerializeField] private TextMeshProUGUI totalCoinsText;
    [SerializeField] private TextMeshProUGUI lifesText;
    [SerializeField] private TextMeshProUGUI currentWaveText;
    [SerializeField] private TextMeshProUGUI gameOverTotalCoinsText;


    private Node _currentNodeSelected;

    private void Update() 
    {
        totalCoinsText.text = CurrencySystem.Instance.TotalCoins.ToString();
        lifesText.text = LevelManager.Instance.TotalLives.ToString();
        currentWaveText.text = $"Wave {LevelManager.Instance.CurrentWave}";
    }
/// <summary>
/// Poder relantizar el tiempo de nuestra partiad
/// </summary>
    public void SlowTime()
    {
        Time.timeScale = 0.5f;
    }
/// <summary>
/// Permite reanudar el tiempo con normalidad de nuestra partida
/// </summary>
    public void ResumeTime()
    {
        Time.timeScale = 1f;
    }
/// <summary>
/// Permite avanzar el tiempo de la partida mas deprisa
/// </summary>
    public void FastTime()
    {
        Time.timeScale = 2f;
    }

/// <summary>
/// Se encarga de mostrarnos el panel de GAME OVER cuando perdemos y nos muestra nuesto total de monedas obtenidas
/// </summary>

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        gameOverTotalCoinsText.text = CurrencySystem.Instance.TotalCoins.ToString();
    }

/// <summary>
/// Permite restablecer la partida desde el principio
/// </summary>

    public void RestarGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

/// <summary>
/// Nos permite abrir nuestro panel de logros y poder visualizarlo
/// </summary>

    public void OpenAchievementPanel(bool status)
    {
        achievementPanel.SetActive(status);
    }
/// <summary>
/// Permite cerrar nuestro panel de logros
/// </summary>
    public void CloseTurretShopPanel()
    {
        turretShopPanel.SetActive(false);
    }
/// <summary>
/// Permite cerrar nuestro panel de nodes cuando pinchamos en el. En el cual nos sale el panel de upgrade y sell de nuestras torretas
/// </summary>

    public void CloseNodeUIPanel()
    {
        _currentNodeSelected.CloseAttackRangeSprite();
        nodeUIPanel.SetActive(false);
    }

/// <summary>
/// Permite upgradear nuestras torretas cuando pinchamos en el panel
/// </summary>
    public void UpgradeTurret()
    {
        _currentNodeSelected.Turret.TurretUpgrade.UpgradeTurret();
        UpdateUpgradeText();
        UpdateTurretLevel();
        UpdateSellValue();
    }
/// <summary>
/// Permite vender nuestra torreta cuando pinchamos en el panel
/// </summary>
    public void SellTurret()
    {
        _currentNodeSelected.SellTurret();
        _currentNodeSelected = null;
        nodeUIPanel.SetActive(false);
    }
/// <summary>
/// Nos muestra el panel de los nodes para añadir torretas
/// </summary>
    private void ShowNodeUI()
    {
        nodeUIPanel.SetActive(true);
        UpdateUpgradeText();
        UpdateTurretLevel();
        UpdateSellValue();

    }
/// <summary>
/// Nos muestra el texto del coste del upgrade de las torretas
/// </summary>
    
    private void UpdateUpgradeText()
    {
        upgradeText.text = _currentNodeSelected.Turret.TurretUpgrade.UpgradeCost.ToString();
    }

/// <summary>
/// Nos muestra el nivel que nuestra torreta a adquirido con el upgrade
/// </summary>

    private void UpdateTurretLevel()
    {
        turretLevelText.text = $"Level {_currentNodeSelected.Turret.TurretUpgrade.Level}";
    }
/// <summary>
/// Nos muestra el precio de venta cuando upgradeamos una torreta
/// </summary>

    private void UpdateSellValue()
    {
        int sellAmount = _currentNodeSelected.Turret.TurretUpgrade.GetSellValue();
     
        sellText.text = sellAmount.ToString();
    }
/// <summary>
/// Nos permite poder clickar encima de nuestros nodes para añadir posteriormente las torretas
/// </summary>
    private void NodeSelected(Node nodeSelected)
    {
        _currentNodeSelected = nodeSelected;
        if (_currentNodeSelected.IsEmpty())
        {
            turretShopPanel.SetActive(true);
        }
        else
        {
            ShowNodeUI();
        }
    }
    private void OnEnable() 
    {
        Node.OnNodeSelected += NodeSelected;
    }

    private void OnDisable() 
    {
        Node.OnNodeSelected -= NodeSelected;
    }

}
