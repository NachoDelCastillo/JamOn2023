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

        DeactivateAllSkins();

        skins[skin.x].mallas[skin.y].SetActive(true);
        GetComponent<HitsController>().setGfx(skins[skin.x].mallas[skin.y]);
    }
    public GameObject skinActive() { return skins[skin.x].mallas[skin.y]; }


    void DeactivateAllSkins()
    {
        var parent = skins[skin.x].mallas[skin.y].transform.parent;

        for (int i = 1; i < parent.childCount; i++)
        {
            parent.GetChild(i).gameObject.SetActive(false);
        }
    }
}
