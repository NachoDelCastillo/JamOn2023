using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour{

    float timer;
    float myBestTime;
    public Text textTimer;
    public Guardar guardar;
    // Start is called before the first frame update
    void Start(){
        guardar.Load();
        myBestTime = guardar.record;
        actualizarRecord();
    }

    // Update is called once per frame
    void Update(){
        timer += Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other){
        if (other.gameObject.GetComponent<ShipController>() != null){            
            if(timer < myBestTime||myBestTime==-1){
                myBestTime= timer;
                guardar.record = myBestTime;
                guardar.Safe();
                actualizarRecord();
            }
            timer = 0.0f;
        }
    }
    public void actualizarRecord(){
        if (myBestTime != -1)
            textTimer.text = myBestTime.ToString();
        else 
            textTimer.text = "No Time";
    }
}
