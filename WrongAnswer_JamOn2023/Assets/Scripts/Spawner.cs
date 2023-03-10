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
    SplineFollower boss;
    [SerializeField]
    SplineComputer track;
    [SerializeField]
    float tMin;
    [SerializeField]
    float tMax;
    [SerializeField]
    float maxPosVariation;

    float nextSpawn = 0;
    float cont = 0;

    LayerMask trackLayer;

    List<GameObject> spawns = new List<GameObject>();

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
        track.Project(nearestPer, boss.transform.position);
        return nearestPer.percent;
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
            spawns.Insert(0, Instantiate(toSpawn, hit.point, rotacionFinal));
        }
    }

}
