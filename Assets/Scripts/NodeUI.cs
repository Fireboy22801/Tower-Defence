using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject UI;

    public TMP_Text upgradeCost;
    public TMP_Text sellAmount;
    public Button upgradeButton;
    public Color upgradedColor;

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

        if (!target.isUpgraded)
        {
            upgradeButton.image.color = standartColor;
            upgradeCost.text = "$" + target.turretBluePrint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeButton.image.color = upgradedColor;
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
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