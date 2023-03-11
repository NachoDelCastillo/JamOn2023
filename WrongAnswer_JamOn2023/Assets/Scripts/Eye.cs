using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Missile misil = collision.gameObject.GetComponent<Missile>();
        if (misil != null)
        {
            GameManager.GetInstance().IncreaseEyeIndex();
            misil.Explode();
            this.gameObject.SetActive(false);
        }
              
    }
}
