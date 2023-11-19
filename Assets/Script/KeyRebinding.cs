using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class KeyRebinding : MonoBehaviour
{
    public List<TMP_InputField> inputFields;

    private void Start()
    {
        inputFields = new List<TMP_InputField> (GetComponentsInChildren<TMP_InputField>());

        foreach (var inputField in inputFields)
        {
            inputField.onValueChanged.AddListener(inputValue => LimitToOneChar(inputField, inputValue));
        }
    }

    private void LimitToOneChar(TMP_InputField inputField, string inputValue)
    {
        // Limit to only 1 character
        if (inputValue.Length > 1)
        {
            inputField.text = inputValue.Substring(0, 1);
        }

        if (inputValue.Length > 0)
        {
            EventSystem.current.SetSelectedGameObject(null);
            Debug.Log("deselect");
        }

        /*if (!IsValidKey(inputValue))
        {
                Debug.LogWarning("Invalid key: " + inputValue);
        }*/
    }
/*
    private bool IsValidKey(string key)
    {
        return key.ToLower() != "tab";
    }*/
}