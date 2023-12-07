using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AbilityPower : MonoBehaviour
{


    [SerializeField] public KeyCode abilityPress;
    [SerializeField] public float abilityPowerForce;




    protected bool downAbilityPress;
    protected bool isAbilityPress;
    protected bool upAbilityPress;
    private bool hasPressedAbility;

    public static bool hasPressedAbilityInGhostPusher;
    public static bool hasPressedAbilityInGhostPuller;

    public float minChargingTime = 2f;
    public float maxChargeingTime = 4;


    private float timeSinceLastPressedUp = 0f;
    private float timeSinceLastPressedDown = 0f;
    
    private bool isHit = false;

    [SerializeField] private static float deadTimeBetweenPress = .2f;
    [SerializeField] private float hitDuration = .2f;


    private void OnEnable()
    {
        if (isAbilityPress)
        {
            hasPressedAbility = true;
            return;
        }

        hasPressedAbility = false;
    }

   

    protected void KeyInputs() 
    {
        downAbilityPress = PressAbilityDown();

        isAbilityPress = PressAbilityHold();
      
        upAbilityPress = PressAbilityUp();

    }


    public bool PressAbilityDown() 
    {
        bool result;

        if (Input.GetKeyDown(abilityPress))
        {
            result = true;
            hasPressedAbility = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

    public bool PressAbilityHold() {
        
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

    public bool PressAbilityUp()
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
                hasPressedAbility = false; 
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

    public bool HasPressedAbility() {
        
        bool result = false;

        if (upAbilityPress)
        {
            result = false;
        }
        else if (isAbilityPress)
        {
            result = true;
            
        }

        return result;
    }

    public IEnumerator SetIsHit()
    {
        isHit = true;
        yield return new WaitForSeconds(GetHitDuration());
        isHit = false;
    }

    public bool IsHit()
    {
        return isHit;
    }

    public float GetHitDuration()
    {
        return hitDuration;
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
