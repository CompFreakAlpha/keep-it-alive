using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public float timeUntilNextWave = 0;
    public float timeBetweenWaves = 4;

    public int waveIndex = 0;

    public EnemySpawn[] spawnInfo = new EnemySpawn[] {
        new EnemySpawn("Skeleton", 0.1f),
        new EnemySpawn("Slime", 1),
        new EnemySpawn("Goblin", 2),
};



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timeUntilNextWave > 0)
        {
            timeUntilNextWave -= Time.deltaTime;
        }
        else
        {
            if (GameObject.FindObjectOfType<EntitySkeleton>() == null)
            {

                if (((float)waveIndex / 5f) % 1 == 0)
                {
                    //Every fifth wave
                    GameManager.instance.RefreshUpgrades(1);

                }
                else if (((float)waveIndex / 10f) % 1f == 0)
                {
                    //Every tenth wave
                    GameManager.instance.RefreshUpgrades(2);
                }
                else if (((float)waveIndex / 25f) % 1f == 0)
                {
                    //Every 25th wave
                    GameManager.instance.RefreshUpgrades(3);
                }

                Wave currentWave = GenerateWaveData(waveIndex);

                float run = 0;
                for (int i = 0; i < currentWave.enemies.Count; i++)
                {
                    run += currentWave.enemies[i].delay;
                }

                StartCoroutine(SpawnWave(currentWave, waveIndex));
                waveIndex++;
                timeUntilNextWave = timeBetweenWaves + run;
            }



        }
    }

    public Wave GenerateWaveData(int index)
    {
        Wave wave = new Wave();

        int spawnCountBase = 0;

        spawnCountBase = 5 + (int)Mathf.Ceil((float)index / 3f);

        int spawnCount = spawnCountBase + Random.Range(-2, 2);
        for (int i = 0; i < spawnCount; i++)
        {
            int rand = Random.Range(0, spawnInfo.Length);
            wave.enemies.Add(spawnInfo[rand]);
        }

        return wave;
    }

    public IEnumerator SpawnWave(Wave wave, int index)
    {

        AudioManager.instance.Play("wave_new");

        float rand = Random.value;
        if (rand > 0.8f)
        {
            float random = Random.Range(0, 5);
            for (int i = 0; i < random; i++)
            {
                Vector2 spawnPos = new Vector2(transform.position.x + UnityEngine.Random.Range(-3, 3), transform.position.y);
                GameObject enemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/Runner"), spawnPos, Quaternion.identity);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(5);
        }


        for (int i = 0; i < wave.enemies.Count; i++)
        {
            Vector2 spawnPos = new Vector2(transform.position.x + UnityEngine.Random.Range(-3, 3), transform.position.y);
            GameObject enemy = Instantiate(Resources.Load<GameObject>("Prefabs/Enemies/" + wave.enemies[i].id), spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(wave.enemies[i].delay);
        }

    }
}
