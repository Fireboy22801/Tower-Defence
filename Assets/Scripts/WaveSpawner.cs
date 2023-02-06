using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive;

    [Header("Main Settings")]
    public List<Transform> enemiesPrefabs;
    public List<float> spawnChances;

    public float delay = 0.2f;

    [Header("Q - the difference between chances (Geometric progression)")]
    public int q = 3;

    private int _waveIndex = 0;

    private int qStart;
    private bool isAutoStart = false;

    private void Start()
    {
        qStart = q;
        EnemiesAlive = 0;
        GetStartChances();
    }

    private void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (_waveIndex == 100)
        {
            Time.timeScale = 0;
        }

        if (isAutoStart)
            StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++;
        _waveIndex++;

        UpdateChances();

        for (int i = 0; i < _waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(delay);
        }
    }
    public void StartNextWave()
    {
        if (EnemiesAlive == 0)
            StartCoroutine(SpawnWave());
    }

    public void AutoStart()
    {
        isAutoStart = !isAutoStart;
    }

    private void SpawnEnemy()
    {
        Instantiate(enemiesPrefabs[GetRandomEnemyIndex(spawnChances)], transform.position, Quaternion.identity);
        EnemiesAlive++;
    }

    private int GetRandomEnemyIndex(List<float> chances)
    {
        float total = chances.Sum();
        float rand = Random.Range(0f, total);
        for (int i = 0; i < chances.Count; i++)
        {
            if (rand < chances[i])
                return i;
            total -= chances[i];
            rand = Random.Range(0f, total);
        }
        return -1;
    }

    private void UpdateChances()
    {
        NormalizeChances();

        for (int i = 0; i < spawnChances.Count; i++)
        {
            if (i >= spawnChances.Count / 2)
            {
                if (_waveIndex >= 5)
                    spawnChances[i] = (spawnChances[i] * 1.8f) + spawnChances[i - 1] / 100 * (spawnChances.Count - i);
            }
            else
            {
                if (spawnChances[i] <= 1)
                {
                    enemiesPrefabs.RemoveAt(i);
                    spawnChances.RemoveAt(i);
                }
            }
        }

        NormalizeChances();
    }

    private void GetStartChances()
    {
        float sumOfGeometricProgr = (Mathf.Pow(q, spawnChances.Count) - 1) / (q - 1);

        q = 1;

        for (int i = spawnChances.Count - 1; i >= 0; i--)
        {
            if (i >= spawnChances.Count / 2)
                spawnChances[i] = 0;
            else
            {
                spawnChances[i] = 100f / sumOfGeometricProgr * q;
                q *= qStart;
                Mathf.Clamp(spawnChances[i], 0, Mathf.Infinity);
            }
        }

        NormalizeChances();
    }

    private void NormalizeChances()
    {
        float sum = spawnChances.Sum();
        float x = sum / 100;
        for (int i = 0; i < spawnChances.Count; i++)
        {
            spawnChances[i] /= x;
        }
    }
}
