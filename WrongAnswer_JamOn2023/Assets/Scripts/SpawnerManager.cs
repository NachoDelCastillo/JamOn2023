using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    [SerializeField]
    Spawner punos;
    [SerializeField]
    Spawner pinchos;
    [SerializeField]
    Spawner powerUp_cohete;
    [SerializeField]
    Spawner powerUp_escudo;


    float powerUpTimer;
    float powerUpMAXTime = 2;


    private void Awake()
    {
        InvokeRepeating("GH", 1, 1);

        powerUpTimer = powerUpMAXTime;
    }

    private void Update()
    {
        if (powerUpTimer > 0)
            powerUpTimer -= Time.deltaTime;
    }

    void SpawnSomething()
    {
        int randomInt = Random.Range(0, 3 + 1);
        bool spawnPincho = randomInt == 0;

        if (spawnPincho)
        {
            pinchos.SpawnThis();
        }
        else
        {
            randomInt = Random.Range(0, 2 + 1);
            bool spawnPowerUp = randomInt == 0;

            if (spawnPowerUp)
            {
                if (powerUpTimer < 0)
                {
                    powerUpTimer = powerUpMAXTime;
                }

            }
        }

        punos.SpawnThis();
    }
}
