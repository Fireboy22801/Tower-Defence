using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive;

    [SerializeField] private Waves[] _waves;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject speedButton;
    [SerializeField] private Sprite standartSpeedButton;
    [SerializeField] private Sprite pressedSpeedButton;

    private int _currentEnemyIndex;
    private int _currentWaveIndex;
    private int _enemiesLeftToSpawn;

    private void Start()
    {
        startButton.SetActive(true);
        speedButton.SetActive(false);
        EnemiesAlive = 0;
        _enemiesLeftToSpawn = _waves[0].WaveSettings.Length;
    }

    private void Update()
    {
        if (EnemiesAlive <= 0)
        {
            startButton.SetActive(true);
            speedButton.SetActive(false);
        }
    }

    private IEnumerator SpawnEnemyInWave()
    {
        if (_enemiesLeftToSpawn > 0)
        {
            yield return new WaitForSeconds(_waves[_currentWaveIndex]
                .WaveSettings[_currentEnemyIndex]
                .SpawnDelay);
            Instantiate(_waves[_currentWaveIndex]
                .WaveSettings[_currentEnemyIndex].Enemy,
                transform.position, Quaternion.identity);
            _enemiesLeftToSpawn--;
            _currentEnemyIndex++;
            StartCoroutine(SpawnEnemyInWave());
        }
        else
        {
            if (_currentWaveIndex < _waves.Length - 1)
            {
                _currentWaveIndex++;
                _enemiesLeftToSpawn = _waves[_currentWaveIndex].WaveSettings.Length;
                _currentEnemyIndex = 0;
            }
        }
    }

    public void LaunchWave()
    {
        startButton.SetActive(false);
        speedButton.SetActive(true);
        PlayerStats.Rounds++;
        EnemiesAlive = _waves[_currentWaveIndex].WaveSettings.Length;
        StartCoroutine(SpawnEnemyInWave());
    }

    public void ChangeTimeSpeed()
    {
        ChangeImage();
        if (Time.timeScale == 1)
        {
            Time.timeScale = 2;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void ChangeImage()
    {
        Image image = speedButton.GetComponent<Image>();
        if (image.sprite == standartSpeedButton)
        {
            image.sprite = pressedSpeedButton;
        }
        else
        {
            image.sprite = standartSpeedButton;
        }
    }
}

[System.Serializable]
public class Waves
{
    [SerializeField] private WaveSettings[] _waveSettings;
    public WaveSettings[] WaveSettings { get => _waveSettings; }
}

[System.Serializable]
public class WaveSettings
{
    [SerializeField] private GameObject _enemy;
    public GameObject Enemy { get => _enemy; }
    [SerializeField] private float _spawnDelay;
    public float SpawnDelay { get => _spawnDelay; }
}
