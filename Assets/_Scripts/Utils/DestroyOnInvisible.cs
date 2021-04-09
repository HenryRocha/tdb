using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnInvisible : MonoBehaviour
{
    [SerializeField] private GameObject destroyTarget = null;

    /// <summary>
    /// OnBecameInvisible is called when the renderer is no longer visible by any camera.
    /// </summary>
    void OnBecameInvisible()
    {
        if (destroyTarget == null)
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(destroyTarget);
        }
    }
}
