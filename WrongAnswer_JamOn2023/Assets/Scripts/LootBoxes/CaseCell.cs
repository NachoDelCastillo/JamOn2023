using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaseCell : MonoBehaviour
{
    [System.Serializable]
    private class ListOfSprites{
        public List<Sprite> sprites;        
    }
    [SerializeField]
    private List<ListOfSprites> spriteList;

    [SerializeField]
    private int[] chances;

    [SerializeField]
    private Color[] colors;

    CaseScroll caseScroll;
    public int idList;
    public int id;

    private void Start(){
        caseScroll = GetComponentInParent<CaseScroll>();
    }
    public void SetUp(){
        int index = Randomize();
        id = Random.Range(0, spriteList[index].sprites.Count);
        GetComponent<Image>().sprite = spriteList[index].sprites[id];
        transform.parent.GetComponent<Image>().color= colors[index];
    }

    private int Randomize(){
        int ind = 0;
        int rand = Random.Range(0, 100);
        for (int i = 0; i < chances.Length; i++){
            if (rand <= chances[i]) { idList = i; return i; }
            ind++;
        }
        idList = ind;
        return ind;
    }

    //private void Update(){
    //    if (transform.position.x < 0) {
    //        caseScroll.generateCell();
    //        Destroy(transform.parent.gameObject); 
    //    }
    //}
}
