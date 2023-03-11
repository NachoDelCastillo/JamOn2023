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

    private void Start(){
        shield = GetComponentInChildren<Shield>();
    }
    //Shield
    Shield shield;
    private void Update()
    {
        if (cont < invinTime)
        {
            cont += Time.deltaTime;
        }
    }

    public void OnHit()
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
            }

            cont = 0;
        }
    }
}
