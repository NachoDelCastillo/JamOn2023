using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePowerUp : PowerUp
{
    [SerializeField]
    GameObject missile;
    private void Update()
    {
        RotatePowerUp();
    }
    public override void Power(GameObject player)
    {
        GameObject m = Instantiate(missile, player.transform);
       // m.GetComponent<MissilePowerUp>().enabled = false;
       // m.AddComponent<Missile>();
        m.gameObject.transform.position = player.transform.position + new Vector3(0, 3, 0);
    }
}
