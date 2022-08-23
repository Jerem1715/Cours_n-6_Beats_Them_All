using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject playerRef;
    [SerializeField] Transform enemyParent;

    [SerializeField] float xValue;
    [SerializeField] float yValue;

    [SerializeField] float timerBetweenSpawn = 2f;
    [SerializeField] int spawnCount = 10;
    [SerializeField] int startSpawnCount = 4;
    Vector2 enemyRandomToSpawn;
    float timer = 0f;


    // de placer une variable static me permet d'y accéder depuis tous les objet de ma scène
    public static int enemyEnVie;

    void Start()
    {
 
        //Execute mon code
        for (int i = 0; i < startSpawnCount; i++)
        {
            CreateEnemy();
        }
    }
    void CreateEnemy()
    {
        //Je crée une position aléatoire
         enemyRandomToSpawn= new Vector2(Random.Range(-xValue, xValue), Random.Range(-yValue, yValue));

        //Je crée mon enemi
        Instantiate(enemyPrefab, enemyRandomToSpawn, Quaternion.identity,enemyParent);
     
    }

    void Update()
    {
        if (spawnCount <= 0 || playerRef == null) return;

        //counter
        timer += Time.deltaTime;
        //debug.Log(timer);

        //Sapwn toutes les c secondes
        if (timer >= timerBetweenSpawn)
        {
            CreateEnemy();
            spawnCount--;

            timer = 0;
        }
    }
}
