using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform _target;
    private Enemy targetEnemy;

    [Header("General")]

    public float range = 15f;

    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    public float _fireCountDown = 1f;

    [Header("Use Laser")]
    public bool useLaser = false;

    public int damageOverTime = 30;

    public float slowAmount = 0.5f;
    public float slowDuration = 0.5f;


    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;


    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    [Header("Bullet")]

    public Transform firePoint;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0, 0.5f);
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

            if (nearestEnemy != null && shortestDistance <= range)
            {
                _target = nearestEnemy.transform;
                targetEnemy = nearestEnemy.GetComponent<Enemy>();
            }
            else
            {
                _target = null;
            }

        }
    }

    private void Update()
    {
        if (_target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }
            return;
        }


        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (_fireCountDown <= 0)
            {
                Shoot();
                _target = null;
                _fireCountDown = 1f / fireRate;
            }
            _fireCountDown -= Time.deltaTime;
        }

    }

    private void LockOnTarget()
    {
        Vector3 direction = _target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount, slowDuration);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, _target.position);

        Vector3 directionToLaser = firePoint.position - _target.position;

        impactEffect.transform.position = _target.position + directionToLaser.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(directionToLaser);

    }

    private void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(_target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
