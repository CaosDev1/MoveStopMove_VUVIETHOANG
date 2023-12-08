using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float minTimespawn, maxTimespawn;
    [SerializeField] private int maxEnemySpawn;
    [SerializeField] private Transform revivePos;
    [SerializeField] private Player player;
    private List<Enemy> enemies = new List<Enemy>();

    private void Start()
    {
        StartSpawnEnemy(maxEnemySpawn);
    }

    private void StartSpawnEnemy(int maxEnemy)
    {
        for (int i = 0; i < maxEnemy; i++)
        {

            //Enemy spawnEnemy = LeanPool.Spawn(enemyPrefab, new Vector3(randomX, 0f, randomZ), Quaternion.identity);
            Enemy spawnEnemy = LeanPool.Spawn(enemyPrefab, RandomNavSphere(Vector3.zero, 50f, -1), Quaternion.identity);
            
            enemies.Add(spawnEnemy);
        }
    }

    public void EnemyDeath(Enemy enemy)
    {
        enemies.Remove(enemy);
        
        if(enemies.Count < maxEnemySpawn)
        {
            StartCoroutine(AddEnemy(Random.Range(minTimespawn,maxTimespawn)));
        }
    }

    public IEnumerator AddEnemy(float time)
    {
        yield return new WaitForSeconds(time);
        
        //Enemy spawnEnemy = LeanPool.Spawn(enemyPrefab, new Vector3(randomX, 0f, randomZ), Quaternion.identity);
        Enemy spawnEnemy = LeanPool.Spawn(enemyPrefab, RandomNavSphere(Vector3.zero,20f,-1), Quaternion.identity);
        spawnEnemy.OnInit();
        enemies.Add(spawnEnemy);
    }

    public void ResetGame()
    {
        player.OnInit();
        player.transform.position = revivePos.position;
        enemies.Clear();
        LeanPool.DespawnAll();
        StartSpawnEnemy(maxEnemySpawn);
    }

    public Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }


}
