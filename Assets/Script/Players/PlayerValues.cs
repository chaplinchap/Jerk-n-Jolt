using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerValues 
{

    private static float playerSpeed = 160f;
    private static float jumpingPower = 60f;
    private static float fastFallSpeed = 10f;

    
    // Pusher \\
    public static readonly float pushPlayerSpeed = playerSpeed;
    public static readonly float pushJumpingPower = jumpingPower;
    public static readonly float pushFastFallSpeed = fastFallSpeed;
    public static readonly float pushForce =  100f;


    // Puller \\
    public static readonly float pullPlayerSpeed = playerSpeed;
    public static readonly float pullJumpingPower = jumpingPower;
    public static readonly float pullFastFallSpeed = fastFallSpeed;
    public static readonly float pullForce = 35f;



}
