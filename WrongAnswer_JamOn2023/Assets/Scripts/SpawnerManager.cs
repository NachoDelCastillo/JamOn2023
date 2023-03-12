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

    [SerializeField]
    Shield shield;

    float powerUpTimer;
    float powerUpMAXTime = 10;


    private void Awake()
    {
        InvokeRepeating("SpawnSomething", 1, 2.5f - (GameManager.GetInstance().GetLevel() * 0.75f));

        powerUpTimer = powerUpMAXTime;
    }

    private void Update()
    {
        if (powerUpTimer > 0)
            powerUpTimer -= Time.deltaTime;
    }

    void SpawnSomething()
    {
        powerUp_cohete.SpawnThis();
        return;

        int randomInt = Random.Range(0, 5 + 1);
        bool spawnPincho = randomInt == 0;

        if (spawnPincho)
        {
            pinchos.SpawnThis();
        }
        else
        {
            randomInt = Random.Range(0, 5 + 1);
            bool spawnPowerUp = randomInt == 0;

            if (spawnPowerUp)
            {
                if (powerUpTimer < 0)
                {
                    powerUpTimer = powerUpMAXTime;

                    if (!shield.active)
                        powerUp_escudo.SpawnThis();
                    else
                        powerUp_cohete.SpawnThis();
                }
            }
            else punos.SpawnThis();
        }

    }
}
