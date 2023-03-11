using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField]
    GameObject explosion;

    GameManager gameManager;
    bool killed = false;
    private void Start()
    {
        gameManager = GameManager.GetInstance();
    }
    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetEyeIndex() >= gameManager.getEyes().Count && !killed)
            Explosion();
    }

    private void Explosion()
    {
        killed = true;
        this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        Instantiate(explosion, this.gameObject.GetComponentInChildren<MeshRenderer>().transform);
        Invoke("Kill", 2.0f);
    }

    private void Kill()
    {
        this.gameObject.SetActive(false);
        GameManager.GetInstance().ChangeScene("MainMenu_Scene");
    }
}
