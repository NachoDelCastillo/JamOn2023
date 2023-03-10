using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject toSpawn;
    [SerializeField]
    Transform player;
    [SerializeField]
    SplineComputer track;
    [SerializeField]
    float tMin;
    [SerializeField]
    float tMax;
    [SerializeField]
    double percentAhead;
    [SerializeField]
    float maxPosVariation;

    float nextSpawn = 0;
    float cont = 0;

    LayerMask trackLayer;

    private void Start()
    {
        trackLayer = track.gameObject.layer;
        nextSpawn = UnityEngine.Random.Range(tMin, tMax);
    }

    private void Update()
    {
        if (cont > nextSpawn)
        {
            SpawnObject(GetWhereToSpawnPercent());
            cont = 0; 
            nextSpawn = UnityEngine.Random.Range(tMin, tMax);
        }
        else
        {
            cont += Time.deltaTime;
        }
    }

    private double GetWhereToSpawnPercent()
    {
        SplineSample nearestPer = new SplineSample();
        track.Project(nearestPer, player.position);
        if (nearestPer.percent + percentAhead > 1.0)
        {
            return nearestPer.percent + percentAhead - 1.0;
        }
        else
        {
            return nearestPer.percent + percentAhead;
        }
    }

    private void SpawnObject(double percentInTrack)
    {
        SplineSample referencia = track.Evaluate(percentInTrack);

        Vector3 forwardDelObjeto = referencia.forward;
        Vector3 upwardDelObjeto = referencia.up;

        var rotacionFinal = Quaternion.LookRotation(forwardDelObjeto, upwardDelObjeto);

        Vector3 rayDir = -referencia.up;

        rayDir = Quaternion.AngleAxis(UnityEngine.Random.Range(-maxPosVariation, maxPosVariation), forwardDelObjeto) * rayDir;

        if (Physics.Raycast(referencia.position, rayDir, out RaycastHit hit, 300, ~trackLayer))
        {
            var spawn = Instantiate(toSpawn, hit.point, rotacionFinal);
        }
    }

}
