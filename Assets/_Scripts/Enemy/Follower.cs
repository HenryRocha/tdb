using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    private PathCreator pathCreator;
    private float distanceTravelled = 0.0f;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        pathCreator = GameObject.FindGameObjectWithTag("EnemyPath").GetComponent<PathCreator>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
    }
}
