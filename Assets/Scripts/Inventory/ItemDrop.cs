using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Inventory;

public class ItemDrop : MonoBehaviour
{
    private Button button;
    public int index = 0;
    public delegate void OnDropClick(int index);
    public event OnDropClick onDropClick;
    void Start()
    {
        // Get the Button component from this GameObject
        button = GetComponent<Button>();

        if (button != null)
        {
            // Add a listener to the button's onClick event
            button.onClick.AddListener(Click);
        }
        else
        {
            Debug.LogError("Button component not found on the GameObject.");
        }
    }
    private void Click()
    {
        Debug.Log("Clicked");
        onDropClick?.Invoke(index);


    }
}
