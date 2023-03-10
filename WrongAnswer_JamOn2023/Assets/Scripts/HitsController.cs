using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitsController : MonoBehaviour
{
    [SerializeField]
    float invinTime = 0.1f;

    float cont = 0f;

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
            Debug.Log("MALO");
            cont = 0;
        }
    }
}
