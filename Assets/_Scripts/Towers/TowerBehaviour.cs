using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour, IShooter
{
    // Start is called before the first frame update

    public string towerName;

    public float attackRadius = 2.0f;
    private Transform target = null;

    public GameObject shot;
    public Transform gun;


    private float shootDelay = 2f;
    private float _lastShoot = 0.0f;
    public int level = 0;

    public AudioClip shootSFX;

    public Sprite[] renderers = new Sprite[3];
    private SpriteRenderer spriteR;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        spriteR.sprite = renderers[level];
    }

    void ConfigTower(float fireRate, float attackRadius) {
        shootDelay = 1f/fireRate;
        this.attackRadius = attackRadius;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        // transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        Shoot();
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
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
        }

        if (nearestEnemy != null && shortestDistance < attackRadius)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    public void Upgrade() {
        if (level < 2) {
            level++;
            spriteR.sprite = renderers[level];
        }
    }

    public void Shoot()
    {
        if (Time.time - _lastShoot > shootDelay)
        {
            AudioManager.PlaySFX(shootSFX);
            _lastShoot = Time.time;
            GameObject shotObject = Instantiate(shot, gun.position, Quaternion.identity) as GameObject;
            ShotBehaviour shotBehaviour = shotObject.GetComponent<ShotBehaviour>();
            shotBehaviour.direction = (target.position - gun.position).normalized;
        }
    }
}
