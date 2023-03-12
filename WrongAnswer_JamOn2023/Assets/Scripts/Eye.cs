using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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

    Vector3 lookTarget;

    [SerializeField]
    bool lookPlayer;

    private void Awake()
    {
        id = GameManager.GetInstance().getEyes().Count;
        GameManager.GetInstance().getEyes().Add(this);

        ship = FindObjectOfType<ShipController>();

        // Fix 
        //foreach (MeshRenderer item in breakTheseParts)
        //    item.material = functionalEye;

        lookTarget = ship.transform.position;

        InvokeRepeating("ChangeTarget", 1, 1);
    }

    void ChangeTarget()
    {
        int num = 50;
        float x = Random.Range(-num, num);
        float y = Random.Range(-num, num);
        float z = Random.Range(-num, num);

        lookTarget = ship.transform.position + new Vector3(x, y, z);
    }

    private void Update()
    {
        if (!golpeado)
        {
            if (lookPlayer)
                transform.LookAt(ship.transform.position);
            else
                transform.LookAt(lookTarget);
        }
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

            Camera.main.GetComponent<CameraShake>().Shake(1f, 1);
        }

    }
}
