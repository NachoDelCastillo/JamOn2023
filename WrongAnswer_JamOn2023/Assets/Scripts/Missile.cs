using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField]
    Transform pivot;

    float vel = 3/*=100*/;
    bool shot;
    // Start is called before the first frame update

    Transform player;

    [SerializeField]
    Transform leftRocket;
    [SerializeField]
    Transform rightRocket;

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
            Vector3 bossPosition = GameManager.GetInstance().getEyes()[eyeIndex].transform.position;

            transform.LookAt(bossPosition);

            //transform.Translate(transform.forward * vel * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, bossPosition, vel);

            vel += Time.deltaTime * .7f;

            float dist = Vector3.Distance(transform.position, bossPosition);

            Debug.Log("dist = " + dist);

            if (dist < 100)
            {
                if (!rotatingfast)
                {
                    rotatingfast = true;
                    startRotatingFast();
                }
                leftRocket.localPosition = new Vector3(-6 * dist / 100, 0, 0);
                rightRocket.localPosition = new Vector3(6 * dist / 100, 0, 0);
            }
        }
        else
        {
            transform.forward = player.transform.forward;
            transform.position = player.position;
        }
    }

    bool rotatingfast = false;
    void startRotatingFast()
    {
        pivot.DOKill();
        pivot.DOLocalRotate(new Vector3(0, 0, 360 * 10), 5, RotateMode.FastBeyond360);
    }

    public void Shoot()
    {
        shot = true;
        eyeIndex = GameManager.GetInstance().GetEyeIndex();
    }
    public void Explode()
    {
        shot = false;
        //this.gameObject.GetComponentInParent<ShipController>().IncreaseVel();
        // this.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
