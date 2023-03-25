using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public GameObject[] enemies = new GameObject[5];


    public List<GameObject> GetRandomNumEnemies()
    {
        int numberOfEnemies = Random.Range(1, 4);

        List<GameObject> enemyList = new();
        for (int i = 0; i < numberOfEnemies; i++)
        {
            int randomNumber = Random.Range(1, enemies.Length);
            enemyList.Add(enemies[randomNumber]);
        }
        return enemyList;
    }
}
