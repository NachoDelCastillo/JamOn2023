using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    int id;
    [SerializeField]
    GameObject explosion;
    [SerializeField]
    GameObject explosion_2;
    [SerializeField]
    GameObject boss;

    [SerializeField]
    Material functionalEye;
    [SerializeField]
    Material brokenEye;

    [SerializeField]
    MeshRenderer[] breakTheseParts;

    ShipController ship;

    bool golpeado = false;

    private void Awake()
    {
        id = GameManager.GetInstance().getEyes().Count;
        GameManager.GetInstance().getEyes().Add(this);

        ship = FindObjectOfType<ShipController>();

        // Fix 
        //foreach (MeshRenderer item in breakTheseParts)
        //    item.material = functionalEye;
    }

    private void Update()
    {
        if (!golpeado)
            transform.LookAt(ship.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Missile misil = collision.gameObject.GetComponent<Missile>();
        if (misil != null && id == GameManager.GetInstance().GetEyeIndex())
        {
            GameManager.GetInstance().IncreaseEyeIndex();
            Instantiate(explosion, transform.position, Quaternion.identity, boss.transform);
            Instantiate(explosion_2, transform.position, Quaternion.identity, boss.transform);
            misil.Explode();
            //this.gameObject.SetActive(false);

            golpeado = true;
            foreach (MeshRenderer item in breakTheseParts)
                item.material = brokenEye;
        }

    }
}
