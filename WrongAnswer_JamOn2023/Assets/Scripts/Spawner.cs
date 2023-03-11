using Dreamteck.Splines;
using System;
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
    List<double> spawnperc = new List<double>();

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

        DespawnPassedObjects();
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
            spawns.Add(Instantiate(toSpawn, hit.point, rotacionFinal));
            spawnperc.Add(percentInTrack);
        }
    }

    private void DespawnPassedObjects()
    {
        SplineSample nearestPer = new SplineSample();
        track.Project(nearestPer, player.transform.position);
        double minClamp = (nearestPer.percent - 0.1) < 0 ? nearestPer.percent + 0.9 : nearestPer.percent - 0.1;

        double maxClamp = (minClamp + 0.5) > 1.0 ? minClamp - 0.5 : minClamp + 0.5;

        if (minClamp < 0.5)
            if (!(spawnperc[0] > minClamp && spawnperc[0] < maxClamp))
            {
                spawnperc.RemoveAt(0);
                Destroy(spawns[0]);
                spawns.RemoveAt(0);
            }
        else
            if (spawnperc[0] < minClamp || spawnperc[0] > maxClamp)
            {
                spawnperc.RemoveAt(0);
                Destroy(spawns[0]);
                spawns.RemoveAt(0);
            }
    }

}
