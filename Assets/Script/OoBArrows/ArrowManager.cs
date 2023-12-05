using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private RectTransform parentRectTransform;


    public Rigidbody2D GetRB(){ return rb; }  

    public RectTransform GetParentRectTransform(){ return parentRectTransform; }
    



}
