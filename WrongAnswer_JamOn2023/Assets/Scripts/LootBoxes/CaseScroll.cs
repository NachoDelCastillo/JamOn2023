using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseScroll : MonoBehaviour{
    [SerializeField]
    private GameObject prefab;
    public float speedMin=1;
    public float speedMax=3;

    private float speed;
    private bool isCrolling=false;

    private List<CaseCell> cells =new List<CaseCell>();

    public void Scroll(){
        if (isCrolling) return;              

        if(cells.Count == 0) {
            for (int i = 0; i < 10; i++)
                generateCell();
        }
        foreach (var cell in cells)
            cell.SetUp();

        isCrolling = true;
        speed = Random.Range(speedMin, speedMax);
    }

    private void Update(){
        if(!isCrolling) return;


        transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.left * 100, speed*Time.deltaTime*500);
        if(speed>0)
            speed-=Time.deltaTime;
        else{
            speed= 0;
            transform.position = new Vector3(525, 0, 0);
            isCrolling = false;
        }
    }
    public void generateCell(){
        cells.Add(Instantiate(prefab, transform).GetComponentInChildren<CaseCell>());
    }
}
