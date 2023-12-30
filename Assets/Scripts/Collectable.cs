using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Inventory playerInventory; // Assign this in the Inspector
    public string Name = "";
    void OnMouseDown()
    {
        if (playerInventory != null)
        {
            playerInventory.Add(Name); // Add the item to the inventory
            Destroy(gameObject); // Remove the item from the world
        }
    }
}


