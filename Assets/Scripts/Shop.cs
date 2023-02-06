using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBluePrint standartTurret;
    public TurretBluePrint missileLauncher;
    public TurretBluePrint laserBeamer;

    private BuildManager _buildManager;

    private void Start()
    {
        _buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret()
    {
        _buildManager.SelectTurretToBuild(standartTurret);
    }
    public void SelectMissileLauncher()
    {
        _buildManager.SelectTurretToBuild(missileLauncher);
    }
    public void SelectLaserBeamer()
    {
        _buildManager.SelectTurretToBuild(laserBeamer);
    }
}
