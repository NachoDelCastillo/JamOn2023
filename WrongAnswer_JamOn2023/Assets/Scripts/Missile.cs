using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField]
    float vel/*=100*/;
    bool shot;
    // Start is called before the first frame update
    void Start()
    {
        shot = false;
        Invoke("Shoot", 4.0f);
    }

    private void Update()
    {
        if (shot)
        {
            int i = GameManager.GetInstance().GetEyeIndex();
            transform.position = Vector3.MoveTowards(transform.position, GameManager.GetInstance().getEyes()[i].transform.position, vel);
        }
    }

    //private void FixedUpdate()
    //{
    //    if (shot)
    //    {
    //        int i = GameManager.GetInstance().GetEyeIndex();
    //        Vector3 direction = GameManager.GetInstance().getEyes()[i].transform.position - this.gameObject.transform.position;
    //        this.transform.LookAt(direction);
    //        direction.Normalize();
    //        this.gameObject.GetComponent<Rigidbody>().AddForce(direction * vel, ForceMode.VelocityChange);
    //    }
    //}
    public void Shoot()
    {
        shot = true;
    }
    public void Explode()
    {
        shot=false;
        this.gameObject.SetActive(false);
    }
}
