using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
   GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.GetInstance();
    }
    // Update is called once per frame
    void Update()
    {
        if(gameManager.GetEyeIndex() >= gameManager.getEyes().Count)
            this.gameObject.SetActive(false);
    }
}
