using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehaviour : SteerableBehaviour
{

    public Vector3 direction;
    public int pierce;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Thrust(direction.x, direction.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Tower"))
        {
            IDamageable damageable = collision.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
     
            if (!(damageable is null))
            {
                damageable.TakeDamage(1);

                if (pierce == 1) {
                    Destroy(gameObject);
                } else {
                    pierce--;
                }
            }
        }
    }
}
