using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField]
    Transform pivot;

    float vel = 2/*=100*/;
    bool shot;
    // Start is called before the first frame update

    Transform player;

    void Start()
    {
        shot = false;
        Invoke("Shoot", 2.3f);

        pivot.DOLocalRotate(new Vector3(0, 0, 360 * 10), 10, RotateMode.FastBeyond360);

        player = FindObjectOfType<ShipController>().transform;
    }

    int eyeIndex;
    private void Update()
    {
        if (shot)
        {
            transform.LookAt(GameManager.GetInstance().getEyes()[eyeIndex].transform.position);

            //transform.Translate(transform.forward * vel * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, GameManager.GetInstance().getEyes()[eyeIndex].transform.position, vel);

            vel += Time.deltaTime * .7f;
        }
        else
        {
            transform.forward = player.transform.forward;
            transform.position = player.position;
        }
    }

    public void Shoot()
    {
        shot = true;
        eyeIndex = GameManager.GetInstance().GetEyeIndex();
    }
    public void Explode()
    {
        shot=false;
        //this.gameObject.GetComponentInParent<ShipController>().IncreaseVel();
       // this.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
