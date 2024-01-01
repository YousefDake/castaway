using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory inventory;
    public GameObject canvas;
    private void Awake()
    {
        inventory = new Inventory(21);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            canvas.SetActive(!canvas.activeSelf);
        }


    }
}
