using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitsController : MonoBehaviour
{
    [SerializeField]
    float invinTime = 0.1f;

    [SerializeField]
    CameraShake cam;

    float cont = 0f;
    //Shield
    Shield shield;
    GameManager gameManager;

    private void Start(){
        shield = GetComponentInChildren<Shield>();
        gameManager=GameManager.GetInstance();
    }   
    private void Update()
    {
        if (cont < invinTime)
        {
            cont += Time.deltaTime;
        }
    }

    public bool OnHit()
    {
        if (cont > invinTime)
        {
            // Comportamiento de recibir un golpe
            cam.Shake(1, 1f);
            if (shield.getActive()) {
                shield.setActive(false);
                Debug.Log("SHIELD DOWN");
            }
            else{
                Debug.Log("MUERTO");
                gameManager.setGameOver(true);
            }

            cont = 0;

            return true;
        }
        return false;
    }
}
