using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    [SerializeField]
    Material mat;
    private float timeLeft = 0;
    private Color targetColor;

    [SerializeField]
    Color[] colors;
    int currentIndex = 0;

    float intensity;
    private void Awake()
    {
        targetColor = colors[0];

        InvokeRepeating("ChangeColor", 1, 1);
    }

    void Update()
    {
        //Color newCOlor = Color.Lerp(Color.red, Color.blue, Time.deltaTime);
        //mat.SetColor("_EmissionColor", Color.blue);

        Color middleColor = Color.Lerp(mat.GetColor("_EmissionColor"), targetColor * 5.3f, Time.deltaTime * 2);

        float r = middleColor.r;
        float g = middleColor.g;
        float b = middleColor.b;

        if (r > g && r > b)
            middleColor.r = 1;
        else if (g > r && g > b)
            middleColor.g = 1;
        else if (b > r && b > g)
            middleColor.b = 1;

        mat.SetColor("_EmissionColor", middleColor);
    }

    private void ChangeColor()
    {
        if (currentIndex < colors.Length - 1)
            currentIndex++;
        else currentIndex = 0;

        targetColor = colors[currentIndex];
    }
}
