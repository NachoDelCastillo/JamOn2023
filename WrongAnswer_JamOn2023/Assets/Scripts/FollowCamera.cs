using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    Transform boss;
    [SerializeField]
    [Tooltip("0 = Mira siempre al player, 1 = mira siempre al boss")]
    float offset = 0.3f;
    [SerializeField]
    [Tooltip("Que tan arriba se coloca la camara")]
    float abovePlayer;
    [SerializeField]
    [Tooltip("Que tan atras se coloca la camara")]
    float behindPlayer;
    [SerializeField]
    [Tooltip("Cuanto lerp se le aplica a la posicion al moverse")]
    float posLerp;
    [SerializeField]
    [Tooltip("Cuanto lerp se le aplica al objetivo de la camara con respecto a su valor anterior")]
    float lookAtLerp;
    [SerializeField]
    [Tooltip("Cuanto lerp se le aplica al upwards de la camara")]
    float upLerp;

    Vector3 lastLook;

    private void Start()
    {
        transform.position = player.position;

        Vector3 playerToBoss = boss.position - player.position;
        Vector3 lookAt = (playerToBoss * offset) + player.position;
        transform.LookAt(lookAt, player.up);
        lastLook = lookAt;
    }

    void Update()
    {
        Vector3 finalPos = player.position;
        finalPos += player.forward * -behindPlayer;
        finalPos += player.up * abovePlayer;
        transform.position = Vector3.Lerp(transform.position, finalPos, posLerp);

        Vector3 playerToBoss = boss.position - player.position;
        Vector3 lookAt = (playerToBoss * offset) + player.position;
        transform.LookAt(Vector3.Lerp(lastLook, lookAt, lookAtLerp), Vector3.Lerp(transform.up, player.up, upLerp));
        lastLook = lookAt;
    }

    private void OnDrawGizmos()
    {
        Vector3 playerToBoss = boss.position - player.position;

        Vector3 lookAt = playerToBoss * offset;

        Gizmos.DrawLine(player.position, lookAt + player.position);
    }
}
