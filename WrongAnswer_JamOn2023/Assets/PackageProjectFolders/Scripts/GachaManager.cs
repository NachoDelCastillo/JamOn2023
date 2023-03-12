using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaManager : MonoBehaviour
{
    AllMenuManager_PK allMenuManager;
    void Awake()
    { allMenuManager = GetComponentInParent<AllMenuManager_PK>(); }

    [SerializeField]
    GameObject scroll;

    // UPDATE USAIS ESTE
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            allMenuManager.BackButtonHorizontal();
            scroll.GetComponent<CaseScroll>().limpiar();
        }
    }
}
