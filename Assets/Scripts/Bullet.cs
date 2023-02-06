using System.Runtime.CompilerServices;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _target;

    public float speed = 70f;
    public int damage = 50;
    public float explosionRadius = 0f;

    public GameObject impactEffect;

    public void Seek(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = _target.position - transform.position;
        float DistanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= DistanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * DistanceThisFrame, Space.World);
        transform.LookAt(_target);
    }

    private void HitTarget()
    {
        GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 3f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(_target);
        }

        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    private void Damage(Transform enemyTrans)
    {
        Enemy enemy = enemyTrans.GetComponent<Enemy>();
        if (enemy != null)
            enemy.TakeDamage(damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
