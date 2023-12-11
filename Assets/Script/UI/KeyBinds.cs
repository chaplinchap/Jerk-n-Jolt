using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBinds : MonoBehaviour
{
    public Animator keybinds;

   public void ShowKeybinds()
    {
        Debug.Log("ShowKeys");
        //StartCoroutine (Show_Keybinds());
        keybinds.SetTrigger("ShowKeys");
    }

    public void HideKeybinds()
    {
        Debug.Log("HideKeys");
        //StartCoroutine (Hide_Keybinds());
        keybinds.SetTrigger("HideKeys");
    }

    public void KeybindButton()
    {
        Debug.Log("Show");
        StartCoroutine (Show_Keybinds());
        //keybinds.SetTrigger("Show");
    }
    
    IEnumerator Show_Keybinds()
    {        
        yield return new WaitForSeconds (1);
        keybinds.SetTrigger("Show");;
    }
    /*
    IEnumerator Hide_Keybinds()
    {
        keybinds.SetTrigger("HideKeys");
        yield return new WaitForSeconds (1);
    }*/
}
