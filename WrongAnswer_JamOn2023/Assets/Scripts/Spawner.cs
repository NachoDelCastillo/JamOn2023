using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject toSpawn;
    [SerializeField]
    float tMin;
    [SerializeField]
    float tMax;
    [SerializeField]
    float percentAhead;
    [SerializeField]
    Transform player;
    [SerializeField]
    SplineComputer track;

    LayerMask trackLayer;

    private void Start()
    {
        trackLayer = track.gameObject.layer;
        SpawnObject(0.5);
    }

    private void SpawnObject(double percentInTrack)
    {
        SplineSample referencia = track.Evaluate(percentInTrack);

        Vector3 forwardDelObjeto = referencia.forward;
        Vector3 upwardDelObjeto = referencia.up;

        var rotacionFinal = Quaternion.LookRotation(forwardDelObjeto,upwardDelObjeto);

        Instantiate(toSpawn, referencia.position, rotacionFinal);
    }

}
