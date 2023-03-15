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

    int powerUpToSpawn = 10;
    int itemCont = 0;


    private void Awake()
    {
        //InvokeRepeating("SpawnSomething", 0.75f, 1.38f - (GameManager.GetInstance().GetLevel() * 0.38f));
        InvokeRepeating("SpawnSomething", .1f, .1f);

        powerUp_cohete.SpawnThis();
    }

    private void Update()
    {
    }

    void SpawnSomething()
    {
        bool spawnPowerUp = itemCont >= powerUpToSpawn;
        if (spawnPowerUp)
        {
            if (!shield.active)
                powerUp_escudo.SpawnThis();
            else
                powerUp_cohete.SpawnThis();
            itemCont = 0;
        }
        else
        {
            int randomInt = Random.Range(0, 5 + 1);
            bool spawnPincho = randomInt == 0;
            if (spawnPincho)
            {
                pinchos.SpawnThis();
                if (Random.Range(0, 4 + 1) == 0)
                    pinchos.SpawnThis();
            }
            else
            {
                punos.SpawnThis();
                if (Random.Range(0, 3 + 1) == 0)
                    pinchos.SpawnThis();
            }

            itemCont++;
        }

    }
}
