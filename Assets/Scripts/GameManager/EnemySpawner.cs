using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : Respawner
{
    private const float MIN_SPAWN_DISTANCE_FROM_PLAYER = 3f;
    private const float MAX_SPAWN_DISTANCE_FROM_PLAYER = 5f;
    private const int MAX_ENEMY_NUMBER = 4;
    private const float SPAWN_INTERVAL = 5;

    public GameObject[] enemyPrefabs;

    private float spawnTime;
    private List<GameObject> objectPool;
    private List<GameObject> animatorPool;

    private MovePlayer player;

    protected override void Start()
    {
        base.Start();
        objectPool = new List<GameObject>();
        animatorPool = new List<GameObject>();
        player = FindObjectOfType<MovePlayer>();
        spawnTime = 0f;
    }

    private void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawnTime >= SPAWN_INTERVAL)
        {
            if(objectPool.Count(objectPool => objectPool.activeSelf) < MAX_ENEMY_NUMBER)
            {
                SpawnEnemy();
            }
            spawnTime -= SPAWN_INTERVAL;
        }
    }

    public void SpawnEnemy()
    {
        GameObject newEnemyPrefab = GetNewEnemyPrefab();
        bool foundInactive = false;
        foreach(GameObject enemy in objectPool)
        {
            if (!enemy.name.StartsWith(newEnemyPrefab.name))
            {
                continue;
            }
            if (!enemy.activeSelf)
            {
                foundInactive = true;
                InitObject(enemy, newEnemyPrefab.name.Equals("Firefly"));
                break;
            }
        }
        if (!foundInactive)
        {
            GameObject newObject = Instantiate(newEnemyPrefab, transform);
            InitObject(newObject, false);
            if (newEnemyPrefab.name.Equals("Firefly"))
            {
                animatorPool.Add(newObject.transform.Find("Particles").gameObject);
            }
            objectPool.Add(newObject);
        }
    }

    void InitObject(GameObject newObject, bool needParticleReattachment)
    {
        newObject.SetActive(true);
        newObject.transform.localPosition = new Vector3(GetSpawnPosition(), 0, 0);
        newObject.transform.rotation = Quaternion.identity;
        newObject.GetComponent<MoveGhost>().ReplanTargetPosition(false);

        if (needParticleReattachment)
        {
            foreach(GameObject anim in animatorPool)
            {
                if (anim.transform.parent == null)
                {
                    anim.transform.parent = newObject.transform;
                    anim.transform.localPosition = new Vector3(0, 0, 0);
                    anim.GetComponent<ParticleSystem>().Play();
                    break;
                }
            }
        }
    }

    private float GetSpawnPosition()
    {
        float direction = Random.Range(0, 2) * 2 - 1;
        float distance = Random.Range(MIN_SPAWN_DISTANCE_FROM_PLAYER, MAX_SPAWN_DISTANCE_FROM_PLAYER);
        return player.transform.position.x + direction * distance;
    }

    GameObject GetNewEnemyPrefab()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        return enemyPrefabs[randomIndex];
    }

    public override void Respawn()
    {
        spawnTime = 0f;
    }
}
