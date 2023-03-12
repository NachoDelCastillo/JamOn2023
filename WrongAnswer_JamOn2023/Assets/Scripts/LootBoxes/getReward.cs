using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getReward : MonoBehaviour{

    private void Start()
    {
    }

    private void OnTriggerStay2D(Collider2D collision){
        var cmp = collision.GetComponent<CaseCell>();
        
        if (cmp != null){            
           
            GameManager.GetInstance().setReward(cmp.idList, cmp.id);
            gameObject.SetActive(false);
            cmp.transform.parent.GetChild(1).gameObject.SetActive(true);
                       
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var cmp = collision.GetComponent<AudioSource>();

        if (cmp != null)
        {
            cmp.pitch = Random.Range(2.9f, 3f);
            cmp.Play();
        }
    }
}
