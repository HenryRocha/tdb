using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour, IShooter
{
    // Start is called before the first frame update

    public float attackRadius = 2.0f;
    public Transform target = null;

    public GameObject shot;
    public Transform gun;

    
    private float shootDelay = 0.5f;
    private float _lastShoot = 0.0f;
    
    public AudioClip shootSFX;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        Shoot();
    }

    void UpdateTarget() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies) {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance) {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance < attackRadius) {
            target = nearestEnemy.transform;
        } else {
            target = null;
        }
    }

    public void Shoot() {
        if (Time.time - _lastShoot > shootDelay) {
            AudioManager.PlaySFX(shootSFX);
            _lastShoot = Time.time;
            Instantiate(shot, gun.position, Quaternion.identity);
        }
    }
}
