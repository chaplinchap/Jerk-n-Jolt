using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AbiltyPower : MonoBehaviour
{


    public KeyCode abilityPress;


    protected bool downAbilityPress;
    protected bool isAbilityPress;
    protected bool upAbilityPress;


    private float timeSinceLastPressedUp = 0f;
    private float timeSinceLastPressedDown = 0f;

    [SerializeField] private static float deadTimeBetweenPress = .2f;

    protected void KeyInputs() 
    {
        downAbilityPress = PressAbilityDown(abilityPress);

        isAbilityPress = PressAbility(abilityPress);
      
        upAbilityPress = PressAbilityUp(abilityPress);

    }


    protected bool PressAbilityDown(KeyCode abilityPress) 
    {
        bool result;

        if (Input.GetKeyDown(abilityPress))
        {
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

    protected bool PressAbility(KeyCode abilityPress) {

        bool result;
        
        if (Input.GetKey(abilityPress))
        {
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

    protected bool PressAbilityUp(KeyCode abilityPress)
    {

        bool result;

        if (Input.GetKeyUp(abilityPress))
        {
            float timeBetween = Time.time - timeSinceLastPressedDown;


            if (timeBetween <= deadTimeBetweenPress)
            {
                result = false;

            }
            else
            {
                result = true;
            }

            timeSinceLastPressedDown = Time.time;
        }
        else
        {
            result = false;
        }

        return result;
    }

    protected Vector3 VectorBetween(GameObject otherPlayer)
    {
        Vector3 position;
        position = gameObject.GetComponent<Transform>().position;
        return (otherPlayer.transform.position - position);
    }



    /*

protected void Timer() 
{
    if (Input.GetKeyDown(abilityPress))
    {
        chargeTrackingTimer = 0;
        ifFailedChargeTime = false;
        ifSuccesChargeTime = false;
    }
    else if (Input.GetKey(abilityPress))
    {
        if (chargeTrackingTimer > maxChargeingTime)
        {

            chargeTrackingTimer = 0;
            ifFailedChargeTime = false;
            ifSuccesChargeTime = false;
            return;
        }

        chargeTrackingTimer += Time.deltaTime;
    }
    else if (Input.GetKeyUp(abilityPress) && chargeTrackingTimer > minChargingTime && pushField.inField)
    {
        ifSuccesChargeTime = true;
        slowMotion.DoSlowmotion();
        //freeze.Freeze();
        SetPitch();
    }
    else if (Input.GetKeyUp(abilityPress) && pushField.inField)
    {
        ifFailedChargeTime = true;
    }

}
     */


}
