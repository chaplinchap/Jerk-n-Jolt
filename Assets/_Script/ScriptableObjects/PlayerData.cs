using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player")]
public sealed class PlayerData : ScriptableObject
{

    [SerializeField, Range(0, 250)] private float movementSpeed;
    [SerializeField, Range(0, 50)] private float maxSpeed;
    [SerializeField, Range(0, 100)] private float jumpingPower;
    [SerializeField, Range(0, 50)] private float fallSpeed;

    public float MovementSpeed { get => movementSpeed; }
    public float MaxSpeed { get => maxSpeed; }
    public float JumpingPower { get => jumpingPower; }
    public float FallSpeed { get => fallSpeed; }

}
