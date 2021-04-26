using UnityEngine;

[System.Serializable]
public class Enemy
{
    public GameObject enemy;
    public int count;
}

[System.Serializable]
public class Wave
{
    public Enemy[] enemies;
    public float rate;
}
