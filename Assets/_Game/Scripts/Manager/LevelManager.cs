using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Level[] mapPrefab;
    [SerializeField] private Player player;
    [SerializeField] private float minTimespawn, maxTimespawn;
    
    private int maxEnemySpawn;
    private int maxTarget;
    private List<Enemy> enemies = new List<Enemy>();

    private int level = 1;

    private int characterHitCount = 1;
    private int enemySpawnCount = 1;
    
    public Level currentLevel;

    private void Start()
    {
        SpawnMap(level);
        StartSpawnEnemy(maxEnemySpawn);
    }

    private void StartSpawnEnemy(int maxEnemy)
    {
        for (int i = 0; i < maxEnemy; i++)
        {

            //Enemy spawnEnemy = LeanPool.Spawn(enemyPrefab, new Vector3(randomX, 0f, randomZ), Quaternion.identity);
            Enemy spawnEnemy = LeanPool.Spawn(enemyPrefab, RandomNavSphere(Vector3.zero, 50f, -1), Quaternion.identity);
            spawnEnemy.OnInit();
            enemies.Add(spawnEnemy);
            enemySpawnCount++;
        }
    }

    public void EnemyDeath(Enemy enemy)
    {
        enemies.Remove(enemy);
        characterHitCount++;
        if (enemies.Count < maxEnemySpawn && enemySpawnCount < maxTarget)
        {
            StartCoroutine(AddEnemy(Random.Range(minTimespawn, maxTimespawn)));
            enemySpawnCount++;
        }

        //If play win
        if (characterHitCount == maxTarget)
        {
            //TO DO: Pop up win

            player.OnWin();
        }
    }

    public IEnumerator AddEnemy(float time)
    {
        yield return new WaitForSeconds(time);

        //Enemy spawnEnemy = LeanPool.Spawn(enemyPrefab, new Vector3(randomX, 0f, randomZ), Quaternion.identity);
        Enemy spawnEnemy = LeanPool.Spawn(enemyPrefab, RandomNavSphere(Vector3.zero, 20f, -1), Quaternion.identity);
        spawnEnemy.OnInit();
        enemies.Add(spawnEnemy);
    }

    public void SpawnMap(int level)
    {
        if (currentLevel == null)
        {
            currentLevel = Instantiate(mapPrefab[level - 1]);
            GetInfoMap(currentLevel.maxEnemySpawn, currentLevel.maxTarget);
        }
        else
        {
            Destroy(currentLevel.gameObject);
            currentLevel = Instantiate(mapPrefab[level - 1]);
            GetInfoMap(currentLevel.maxEnemySpawn, currentLevel.maxTarget);
        }
    }
    public void NextLevel()
    {
        if(level < mapPrefab.Length)
        {
            level++;
        }
        SpawnMap(level);
        ResetGame();
    }

    public void GetInfoMap(int maxtEnemySpawn,int maxTarget)
    {
        this.maxTarget = maxTarget;
        this.maxEnemySpawn = maxtEnemySpawn;
    }

    public void ResetGame()
    {
        player.OnInit();
        player.transform.position = currentLevel.revivePos.position;
        enemies.Clear();
        LeanPool.DespawnAll();
        enemySpawnCount = 1;
        characterHitCount = 1;
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
