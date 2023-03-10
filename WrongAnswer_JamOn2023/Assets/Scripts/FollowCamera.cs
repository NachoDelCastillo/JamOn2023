using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    Transform target;

    Vector3 offset = new Vector3(0, 6, -17);

    void Update()
    {
        //transform.position = Mathf.Lerp(transform.position, target.position + offset, Time.deltaTime);
    }
}
