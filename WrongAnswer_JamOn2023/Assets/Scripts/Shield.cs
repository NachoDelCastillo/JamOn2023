using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool active = false;
    private void Start(){        
        setActive(active);
    }
    public void setActive(bool act){
        active = act;
        this.gameObject.SetActive(active);
    }
}
