using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool active = true;
    MeshRenderer meshRenderer;
    AudioManager_PK audioManager;
    Animator myAnimator;
    private void Start(){
        meshRenderer = GetComponent<MeshRenderer>();
        audioManager = AudioManager_PK.GetInstance();
        myAnimator = GetComponent<Animator>();
        backIdle();
    }
    public void setActive(bool act){
        if (act == active) return;

        active = act;
        if (active) { //si es tru se activa
            cambio();
            shieldUp();
        }
        else { //si es false se desactiva
            Invoke("cambio", 1.0f);
            shieldDown();
        }
    }
    public bool getActive() { return active; }

    private void shieldUp(){
        myAnimator.Play("ShieldUp");
    }
    private void shieldDown(){
        myAnimator.Play("ShieldDown");
    }
    void cambio(){
        meshRenderer.enabled = active;
    }
    void backIdle()
    {
        myAnimator.Play("Idle");
    }
}
