using UnityEngine;

public class Item : MonoBehaviour
{
    public delegate void OnConsumedEvent();
    public event OnConsumedEvent OnConsumed;

    // Add any other properties or methods specific to your consumable item

    public void Consume()
    {
        // Perform any actions you want when the item is consumed
        // For example, play an animation, add points, or destroy the item
        // ...

        // Signal that the item has been consumed
        OnConsumed?.Invoke();
    }
}
