using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject testEnemy;
    public void TestEnemySpawn()
    {
        GameObject enemy = Instantiate(testEnemy);
        enemy.transform.position = new(Random.Range(-2, 3), 4, 0);
    }
}
