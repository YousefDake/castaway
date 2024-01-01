using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Inventory inventory;
    public GameObject canvasGO;
    private Canvas canvas;

    private void Awake()
    {
        inventory = new Inventory(21);
        canvas = canvasGO.gameObject.GetComponent<Canvas>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {

            canvas.enabled = !canvas.enabled;

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.enabled = false;
        }


    }
}
