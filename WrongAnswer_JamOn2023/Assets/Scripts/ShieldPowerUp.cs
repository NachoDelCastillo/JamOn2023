using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : PowerUp{
    private void Update(){
        RotatePowerUp();
    }
    public override void Power(GameObject player){
        var cmp = player.transform.GetComponentInChildren<Shield>();
        cmp.setActive(true);
    }
}
