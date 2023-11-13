using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Powerup/ChargeBuff")]
public class OverrideToChargeBuff : ConsumableScriptableObject
{
    // Start is called before the first frame update
    public float startingMinChargingTime = 2;
    public float startingMaxChargingTime = 4;
   


    public float amountMinTime = 2;
    public float amountMaxTime = 1.5f;

    public override void ApplyPusher(GameObject target)
    {
        target.GetComponent<Push>().minChargingTime /= amountMinTime;
        target.GetComponent<Push>().maxChargeingTime *= amountMaxTime;

    }

    public override void ApplyPuller(GameObject target)
    {
        target.GetComponent<Pull>().minChargingTime /= amountMinTime;
        target.GetComponent<Pull>().maxChargeingTime *= amountMaxTime;
    }

    public override void DeApplyPusher(GameObject target)
    {
        target.GetComponent<Push>().minChargingTime = startingMinChargingTime;
        target.GetComponent<Push>().maxChargeingTime = startingMaxChargingTime;
    }

    public override void DeApplyPuller(GameObject target)
    {
        target.GetComponent<Pull>().minChargingTime = startingMinChargingTime;
        target.GetComponent<Pull>().maxChargeingTime = startingMaxChargingTime;
    }
}
