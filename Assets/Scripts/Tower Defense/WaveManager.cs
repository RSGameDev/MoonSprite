using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<WaveData> Waves;
    public float WaveCoolDown = 5f;

    private int waveIndex = 0;
    private int enemyIndex = 0;

    private float waveTime = 0;
    private bool waveFinished = false;
    private float enemyTime = 0;

    public GameObject enemyPrefab;

    public List<Routes> routes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waveIndex >= Waves.Count)
        {
            return;
        }
        if (waveFinished)
        {
            HandleWave();
        }
        else
        {
            HandleEnemy();
        }
    }

    private void HandleWave()
    {
        if(waveTime <= Time.time)
        {
            waveIndex++;
            enemyIndex = 0;
            waveFinished = false;
        }
    }

    private void HandleEnemy()
    {
        if (enemyTime < Time.time)
        {
            EnemyData enemyToSpawn = Waves[waveIndex].enemies[enemyIndex];
            GameObject SpawnedObject = Instantiate(enemyPrefab, routes[0].routePath[0].transform.position, Quaternion.identity);

            SpawnedObject.GetComponent<Enemy>().SetupEnemy(enemyToSpawn, convertTo2DVectorList(routes[0].routePath));
            enemyTime = Time.time + Waves[waveIndex].spawnCooldown;
            enemyIndex++;
         

            if (enemyIndex >= Waves[waveIndex].enemies.Count)
            {
                waveFinished = true;
            }
        
        }

    }

    private List<Vector2> convertTo2DVectorList(List<GameObject> objectList)
    {
        var newList = new List<Vector2>();

        foreach(var obj in objectList)
        {
            newList.Add(new Vector2(obj.transform.position.x, obj.transform.position.y));
        }

        return newList;
    }



}
