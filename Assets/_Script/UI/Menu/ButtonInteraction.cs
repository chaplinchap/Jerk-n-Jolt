using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHighlight : MonoBehaviour
{
    public Button[] buttons;  // Buttons need to be in correct lineup
    private int currentIndex = -1;
    private bool startedSelection;

    void Update()
    {
        // Checks if any key is pressed at the start. Used to start selection at 0
        if (!startedSelection)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) 
               || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                startedSelection = true;
                currentIndex = 0;
                SelectButton(0);  // Select the first button.
                return;
            }
            
        }



        if (startedSelection)
        {
            // Key input for button selection
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                SelectButton(-1);  // Move selection up
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                SelectButton(1);   // Move selection down
            }

            // Key input for button activation
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                ActivateSelectedButton();
            }
        }
    }

    void SelectButton(int direction)
    {
        // Deselect the currently selected button
        buttons[currentIndex].Select();

        // Move the selection index in the given direction
        currentIndex = (currentIndex + direction + buttons.Length) % buttons.Length;

        // Select the the new button.
        buttons[currentIndex].Select();
    }

    void ActivateSelectedButton()
    {
        // click the selected button
        buttons[currentIndex].onClick.Invoke();
    }
}

