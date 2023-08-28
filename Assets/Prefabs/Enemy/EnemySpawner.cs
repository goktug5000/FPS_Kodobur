using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject[] wayPoints;

    [SerializeField] private float spawnCD;
    // Start is called before the first frame update
    void Start()
    {
        Enemies = new GameObject[5];
        foreach(GameObject enemy in Enemies)
        {
            if (enemy == null)
            {
                SpawnNewEnemy();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnNewEnemy()
    {
        StartCoroutine(spawnAfterTime());
    }
    IEnumerator spawnAfterTime()
    {


        yield return new WaitForSeconds(spawnCD);
        
        Debug.Log("EnemySpawned");

        var newEnemy = Instantiate(EnemyPrefab, chooseSpawnPoint().transform.position, Quaternion.identity);
        newEnemy.transform.parent = gameObject.transform;
        newEnemy.GetComponent<EnemyDuty>().setMyWayPoints(wayPoints);

        for(int q = 0; q < Enemies.Length; q++)
        {
            if (Enemies[q] == null)
            {
                Enemies[q] = newEnemy;
                yield break;
            }
        }

        yield break;
    }
    public GameObject chooseSpawnPoint()
    {

        var SpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        return SpawnPoint;
    }
}
