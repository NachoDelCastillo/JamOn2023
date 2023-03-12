using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialPanel : MonoBehaviour
{
    TMP_Text text;
    Image image;
    // Start is called before the first frame update
    void Start()
    {
        text=gameObject.GetComponentInChildren<TMP_Text>();
        image=gameObject.GetComponentInChildren<Image>();
        Invoke("Fade", 4.0f);
    }
    private void Fade()
    {
        text.DOFade(0, 1.5f);
        image.DOFade(0, 1.5f);
    }
}
