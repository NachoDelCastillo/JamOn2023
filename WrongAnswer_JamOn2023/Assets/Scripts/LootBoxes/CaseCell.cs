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
    private void Start(){
        caseScroll = GetComponentInParent<CaseScroll>();
    }
    public void SetUp(){
        int index = Randomize();

        GetComponent<Image>().sprite = spriteList[index].sprites[Random.Range(0, spriteList[index].sprites.Count)];
        transform.parent.GetComponent<Image>().color= colors[index];
    }

    private int Randomize(){
        int ind = 0;
        int rand = Random.Range(0, 100);
        for (int i = 0; i < chances.Length; i++){
            if (rand <= chances[i]) return i;
            ind++;
        } 
        return ind;
    }
    private void Update(){
        if (transform.position.x < 0) {
            caseScroll.generateCell();
            Destroy(gameObject); 
        }
    }
}
