using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.GetInstance().SetGameOverPanel(this.gameObject);

        this.gameObject.SetActive(false);
    }
}
