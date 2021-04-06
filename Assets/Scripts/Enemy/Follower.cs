using UnityEngine;
using PathCreation;

public class Follower : MonoBehaviour
{
    [SerializeField]
    private PathCreator pathCreator;

    [SerializeField]
    private float speed = 5.0f;

    private float distanceTravelled = 0.0f;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
    }
}
