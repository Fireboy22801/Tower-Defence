using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject UI;

    public TMP_Text upgradeCost;
    public TMP_Text sellAmount;
    public Button upgradeButton;
    public Color disabledColor;

    private Color standartColor;
    private Node _target;

    private void Start()
    {
        standartColor = upgradeButton.image.color;
    }

    public void SetTarget(Node target)
    {
        _target = target;

        transform.position = _target.GetBuildPosition();

        if (!target.isUpgraded) // not Updaded
        {
            upgradeCost.text = "$" + target.turretBluePrint.upgradeCost;

            if (PlayerStats.Money < target.turretBluePrint.upgradeCost)
            {
                upgradeButton.interactable = false;
                upgradeButton.image.color = disabledColor;
            }
            else
            {
                upgradeButton.interactable = true;
                upgradeButton.image.color = standartColor;
            }
        }
        else // Updaded
        {
            upgradeButton.interactable = false;
            upgradeButton.image.color = disabledColor;
            upgradeCost.text = "DONE";
        }

        sellAmount.text = "$" + target.turretBluePrint.GetSellAmount();

        UI.SetActive(true);
    }

    public void Hide()
    {
        UI.SetActive(false);
    }

    public void Upgrade()
    {
        _target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        _target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}