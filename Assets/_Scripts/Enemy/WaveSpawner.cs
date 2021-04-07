using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenWaves = 5.0f;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private Wave[] waves;

    public static int EnemiesAlive = 0;

    private float countdown = 2f;

    private int currentWave = 0;

    GameManager gm;

    void Start() {
        gm = GameManager.GetInstance();
    }

    void Update()
    {
        // If there are no enemies alive, run the countdown and spawn a new wave.
        if (EnemiesAlive <= 0)
        {
            // At the end of the countdown, spawn a new wave of enemies and reset the countdown.
            if (countdown <= 0f)
            {
                StartCoroutine(SpawnWave());
                gm.RoundUp();
                countdown = timeBetweenWaves;
            }
            else
            {
                // Subtract from the countdown, and make sure it stays between 0 and N.
                countdown -= Time.deltaTime;
                countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
            }

        }
    }

    IEnumerator SpawnWave()
    {
        // Get the current wave.
        Wave wave = waves[currentWave];

        // Update the number of enemies alive.
        EnemiesAlive = wave.count;

        // Spawn all the enemies, async.
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        // Update the current wave.
        currentWave++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}