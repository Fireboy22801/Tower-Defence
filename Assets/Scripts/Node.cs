using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color erorColor;
    public Vector3 positionOffSet;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBluePrint turretBluePrint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer _rend;
    private Color _startColor;

    private BuildManager _buildManager;

    private void Start()
    {
        _rend = GetComponent<Renderer>();
        _startColor = _rend.material.color;

        _buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffSet;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret != null)
        {
            _buildManager.SelectNode(this);
            return;
        }

        if (!_buildManager.CanBuild)
            return;

        BuildTurret(_buildManager._turretToBuild);
    }

    private void BuildTurret(TurretBluePrint bluePrint)
    {
        if (PlayerStats.Money < bluePrint.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }
        PlayerStats.Money -= bluePrint.cost;

        GameObject tempTurret = Instantiate(bluePrint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = tempTurret;

        turretBluePrint = bluePrint;

        GameObject effect = Instantiate(_buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBluePrint.GetSellAmount();

        GameObject effect = Instantiate(_buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);

        Destroy(turret);
        turretBluePrint = null;

        isUpgraded = false;
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBluePrint.upgradeCost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }
        PlayerStats.Money -= turretBluePrint.upgradeCost;

        Destroy(turret);

        GameObject tempTurret = Instantiate(turretBluePrint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = tempTurret;

        GameObject effect = Instantiate(_buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);

        isUpgraded = true;
    }


    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!_buildManager.CanBuild || !_buildManager.HasMoney)
        {
            return;
        }
        else
            _rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        _rend.material.color = _startColor;
    }
}
