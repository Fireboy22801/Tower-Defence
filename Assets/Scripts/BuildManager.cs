using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public TurretBluePrint _turretToBuild { get; private set; }
    public GameObject buildEffect;
    public GameObject sellEffect;

    public bool CanBuild { get { return _turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= _turretToBuild.cost; } }

    public NodeUI nodeUI;

    private Node _selectedNode;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
        }
        instance = this;
    }

    public void SelectNode(Node node)
    {
        if (_selectedNode == node)
        {
            DeselectNode();
            return;
        }

        _selectedNode = node;
        _turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        _selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        _turretToBuild = turret;
        DeselectNode();
    }
}
