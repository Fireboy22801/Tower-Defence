using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform _target;
    private int _wavePointIndex = 0;

    private Enemy enemy;
    private PlayerStats _playerStats;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        _playerStats = GameObject.Find("GameManager").GetComponent<PlayerStats>();
        _target = WayPoints.points[0];
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, enemy.speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, _target.position) < 0.3)
        {
            GetNextWayPoint();
        }
    }

    private void GetNextWayPoint()
    {
        if (_wavePointIndex >= WayPoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        _wavePointIndex++;
        _target = WayPoints.points[_wavePointIndex];
    }

    private void EndPath()
    {
        _playerStats.TakeDamage(1);
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
