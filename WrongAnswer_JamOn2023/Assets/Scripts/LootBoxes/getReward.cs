using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getReward : MonoBehaviour{
    private void OnTriggerStay2D(Collider2D collision){
        var cmp = collision.GetComponent<CaseCell>();
        if (cmp != null){
            GameManager.GetInstance().setReward(cmp.idList, cmp.id);
            gameObject.SetActive(false);            
        }
    }
}
