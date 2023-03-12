using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseScroll : MonoBehaviour{
    [SerializeField]
    private GameObject prefab;
    public float speedMin=1;
    public float speedMax=3;

    private float speed;
    private bool isCrolling;

    private List<CaseCell> cells =new List<CaseCell>();

    public void Scroll(){
        if (isCrolling) return;

        isCrolling = true;
        speed = Random.Range(speedMin, speedMax);

        if(cells.Count == 0) {
            for (int i = 0; i < 10; i++)
                cells.Add(Instantiate(prefab,transform).GetComponentInChildren<CaseCell>());   
        }
        foreach (var cell in cells)
            cell.SetUp();
    }

    private void Update(){
        if(!isCrolling) return;


        transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.left * 100, speed*Time.deltaTime*500);
        if(speed>0)
            speed-=Time.deltaTime;
        else{
            speed= 0;
            isCrolling= false;
            transform.position = new Vector3(525, 0, 0);
        }
    }
}
