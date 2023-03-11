using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour{

    float timer;
    float myBestTime;
    public Text TextoTimer;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        timer += Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other){
        if (other.gameObject.GetComponent<ShipController>() != null){ 
            timer = 0.0f;
            myBestTime = timer < myBestTime ? timer : myBestTime;
        }
    }
}
