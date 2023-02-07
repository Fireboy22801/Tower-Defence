using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive;

    [SerializeField] private Waves[] _waves;

    private int _currentEnemyIndex;
    private int _currentWaveIndex;
    private int _enemiesLeftToSpawn;

    private bool _isAutoStart = false;

    private void Start()
    {
        EnemiesAlive = 0;
        _enemiesLeftToSpawn = _waves[0].WaveSettings.Length;
    }

    private void Update()
    {
        if (_isAutoStart)
        {
            if (EnemiesAlive <= 0)
            {
                LaunchWave();
                EnemiesAlive = _enemiesLeftToSpawn;
            }
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
        EnemiesAlive = _waves[_currentWaveIndex].WaveSettings.Length;
        StartCoroutine(SpawnEnemyInWave());
    }

    public void AutoWaveSpawn()
    {
        _isAutoStart = !_isAutoStart;
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
