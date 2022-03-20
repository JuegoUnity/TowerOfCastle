using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class TurretCard : MonoBehaviour
{
    public static Action<TurretSettings> OnPlaceTurret;
    [SerializeField] private Image turretImage;
    [SerializeField] private TextMeshProUGUI turretCost;

    public TurretSettings TurretLoaded { get; set; }
/// <summary>
/// Funcion que nos muestra el panel para adquirir nuevas torretas mostrandonos su foto y coste
/// </summary>

    public void SetupTurretButton(TurretSettings turretSettings)
    {
        TurretLoaded = turretSettings;
        turretImage.sprite = turretSettings.TurretShopSprite;
        turretCost.text = turretSettings.TurretShopCost.ToString();
    }
/// <summary>
/// Funcion que nos permite poner una nueva torreta en un node, restandonos monedas y cerrando el menu del panel de compra de torretas
/// </summary>
    public void PlaceTurret(){
        if (CurrencySystem.Instance.TotalCoins >= TurretLoaded.TurretShopCost)
        {
            CurrencySystem.Instance.RemoveCoins(TurretLoaded.TurretShopCost);
            UIManager.Instance.CloseTurretShopPanel();
            OnPlaceTurret?.Invoke(TurretLoaded);
        }
    }



}
