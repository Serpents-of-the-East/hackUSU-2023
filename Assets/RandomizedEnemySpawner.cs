using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizedEnemySpawner : MonoBehaviour
{
    public GameObject[] spawners;
    public float distanceBeforeEnemySpawn;
    public Movement movement;

    private float accumulatedDistance;
    private Vector3 previousPosition;

    private Collider spawnArea;


    // Start is called before the first frame update
    void Start()
    {
        previousPosition= transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        spawnArea = other;
    }

    // Update is called once per frame
    void Update()
    {
        accumulatedDistance += Vector3.Distance(transform.position, previousPosition);
        previousPosition = transform.position;
        if (accumulatedDistance > distanceBeforeEnemySpawn)
        {
            float randomValue = Random.Range(0f, 1f);
            if (movement.isRunning)
            {
                if (randomValue > 0.3)
                {
                    Debug.Log("Random Enemy spawn Running");
                }
            }
            else
            {
                if (randomValue > 0.6)
                {
                    Debug.Log("Random Enemy Spawn Walking");
                }
            }
            accumulatedDistance -= distanceBeforeEnemySpawn;
        }

    }
}
