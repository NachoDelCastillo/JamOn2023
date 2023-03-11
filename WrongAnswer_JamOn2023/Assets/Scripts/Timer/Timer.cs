using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour{

    float timer;
    float myBestTime;
    public Text textTimer;
    public Text textRecord;
    public Guardar guardar;
    // Start is called before the first frame update
    void Start(){
        guardar.Load();
        myBestTime = guardar.record;
        timer = 0.0f;
        actualizarRecord();
    }

    // Update is called once per frame
    void Update(){
        timer += Time.deltaTime;
        actualizarTimer();
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
            actualizarTimer();
        }
    }
    public void actualizarRecord(){
        if (myBestTime != -1)
            textRecord.text = myBestTime.ToString();
        else 
            textRecord.text = "No Time";
    }
    public void actualizarTimer() {
        textTimer.text = timer.ToString();
    }
}
