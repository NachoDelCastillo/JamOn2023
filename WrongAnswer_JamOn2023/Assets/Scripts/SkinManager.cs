using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    [System.Serializable]
    private class ListOfGameobject{
        public List<GameObject> mallas;
    }

    [SerializeField]
    List<ListOfGameobject> skins;
    Vector2Int skin;
    public void setActivatedSkin(Vector2Int skin){
        this.skin = skin;
        skins[skin.x].mallas[skin.y].SetActive(true);
        GetComponent<HitsController>().setGfx(skins[skin.x].mallas[skin.y]);
    }
    public GameObject skinActive() { return skins[skin.x].mallas[skin.y]; }
}
