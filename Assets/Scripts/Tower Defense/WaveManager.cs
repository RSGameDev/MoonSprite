using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    public TowerDefenseManager tdManager;

    public float WaveCoolDown = 5f;

    private int waveIndex = 0;
    private int enemyIndex = 0;

    private float waveTime = 0;
    private bool waveFinished = false;
    private float enemyTime = 0;

    public GameObject enemyPrefab;
    public List<WaveData> Waves;
    public List<Routes> routes;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waveIndex >= Waves.Count) // Are there any waves left
        {
            return; // GAME FINISHED
        }

        if (waveFinished)
        {
            HandleWave(); // Move onto the next wave
        }
        else
        {
            HandleEnemy(); // Spawn enemy if time is up
        }
    }

    private void HandleWave()
    {
        if(waveTime <= Time.time) // Move onto next wave once timer is up
        {
            waveIndex++;
            enemyIndex = 0;
            waveFinished = false;
        }
    }

    private void HandleEnemy()
    {
        if (enemyTime < Time.time) // When cooldown is up spawn enemy
        {
            EnemyData enemyToSpawn = Waves[waveIndex].enemies[enemyIndex]; // Get Enemy data
            GameObject SpawnedObject = Instantiate(enemyPrefab, routes[0].routePath[0].transform.position, Quaternion.identity); //Spawn enemy

            SpawnedObject.GetComponent<Enemy>().SetupEnemy(enemyToSpawn, convertTo2DVectorList(routes[0].routePath), tdManager); //Update enemy with data
            enemyTime = Time.time + Waves[waveIndex].spawnCooldown; // Update timer
            enemyIndex++;
         

            if (enemyIndex >= Waves[waveIndex].enemies.Count) //Are all enemys spawned
            {
                waveFinished = true;
                waveTime = Time.time + WaveCoolDown;
            }
        
        }

    }

    private List<Vector2> convertTo2DVectorList(List<GameObject> objectList) // Convert Vector 3 game object transforms into Vector 2 list
    {
        var newList = new List<Vector2>();

        foreach(var obj in objectList)
        {
            newList.Add(new Vector2(obj.transform.position.x, obj.transform.position.y));
        }

        return newList;
    }



}
