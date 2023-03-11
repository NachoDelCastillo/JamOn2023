using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    int id;
    [SerializeField]
    GameObject explosion;
    [SerializeField]
    GameObject boss;

    private void Awake()
    {
        id = GameManager.GetInstance().getEyes().Count;
        GameManager.GetInstance().getEyes().Add(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Missile misil = collision.gameObject.GetComponent<Missile>();
        if (misil != null && id==GameManager.GetInstance().GetEyeIndex())
        {
            GameManager.GetInstance().IncreaseEyeIndex();
            Instantiate(explosion, boss.transform);
            misil.Explode();
            this.gameObject.SetActive(false);
        }
              
    }
}
