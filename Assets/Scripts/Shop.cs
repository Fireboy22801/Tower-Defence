using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Button standartTurretItem;
    [SerializeField] private Button missileTurretItem;
    [SerializeField] private Button laserBeamerItem;
    [SerializeField] private Color disabledColor;

    public TurretBluePrint standartTurretBP;
    public TurretBluePrint missileLauncherBP;
    public TurretBluePrint laserBeamerBP;

    private BuildManager _buildManager;

    private Color _standartTurretColor;
    private Color _missileTurretColor;
    private Color _laserBeamerColor;

    private void Start()
    {
        _buildManager = BuildManager.instance;
        _standartTurretColor = standartTurretItem.image.color;
        _missileTurretColor = missileTurretItem.image.color;
        _laserBeamerColor = laserBeamerItem.image.color;
    }

    private void Update()
    {
        standartTurretItem.image.color = ChangeColors(standartTurretBP, _standartTurretColor, standartTurretItem);
        missileTurretItem.image.color = ChangeColors(missileLauncherBP, _missileTurretColor, missileTurretItem);
        laserBeamerItem.image.color = ChangeColors(laserBeamerBP, _laserBeamerColor, laserBeamerItem);
    }

    public void SelectStandardTurret()
    {
        _buildManager.SelectTurretToBuild(standartTurretBP);
    }
    public void SelectMissileLauncher()
    {
        _buildManager.SelectTurretToBuild(missileLauncherBP);
    }
    public void SelectLaserBeamer()
    {
        _buildManager.SelectTurretToBuild(laserBeamerBP);
    }

    private Color ChangeColors(TurretBluePrint bluePrint, Color standartColor, Button button)
    {
        if (bluePrint.cost > PlayerStats.Money)
        {
            button.interactable = false;
            return disabledColor;
        }
        else
        {
            button.interactable = true;
            return standartColor;
        }
    }
}
