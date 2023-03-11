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
            Debug.Log(i);
            transform.position = Vector3.MoveTowards(transform.position, GameManager.GetInstance().getEyes()[i].transform.position, vel);
        }
    }
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
