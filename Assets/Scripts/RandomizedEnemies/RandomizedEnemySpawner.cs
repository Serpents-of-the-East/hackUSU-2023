using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomizedEnemySpawner : MonoBehaviour
{
    public GameObject[] spawners;
    public float distanceBeforeEnemySpawn;
    public Movement movement;

    private float accumulatedDistance;
    private Vector3 previousPosition;

    private GameObject spawner;
    private Enemies enemies;


    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            if (other.name == spawners[i].name)
            {
                spawner = spawners[i];
                enemies = spawner.GetComponent<Enemies>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        accumulatedDistance += Vector3.Distance(transform.position, previousPosition);
        previousPosition = transform.position;
        if (accumulatedDistance > distanceBeforeEnemySpawn)
        {
            float randomValue = Random.Range(0f, 1f);
            List<GameObject> enemiesToSpawn = enemies.GetRandomNumEnemies(); // TODO: Send these prefabs to the battle scene so we can use them
            if (movement.isRunning)
            {
                if (randomValue > 0.3)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
                    Debug.Log("Random Enemy spawn Running"); 
                    GameObject travelObj = GameObject.FindGameObjectWithTag("TravelHandler");
                    TravelHandler travelHandler = travelObj.GetComponent<TravelHandler>();
                    travelHandler.loadBackCameraPosition = mainCamera.transform.position;
                    travelHandler.loadBackPlayerPosition = player.transform.position;
                    SceneManager.LoadScene("ForestPathCombat_Day 1");
                }
            }
            else
            {
                if (randomValue > 0.6)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
                    Debug.Log("Random Enemy Spawn Walking");
                    GameObject travelObj = GameObject.FindGameObjectWithTag("TravelHandler");
                    TravelHandler travelHandler = travelObj.GetComponent<TravelHandler>();
                    travelHandler.loadBackCameraPosition = mainCamera.transform.position;
                    travelHandler.loadBackPlayerPosition = player.transform.position;
                    SceneManager.LoadScene("ForestPathCombat_Day 1");
                }
            }
            accumulatedDistance -= distanceBeforeEnemySpawn;
        }

    }
}
