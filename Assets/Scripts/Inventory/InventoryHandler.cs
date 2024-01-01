using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour
{

    public Inventory inventory;
    public GameObject inventorySlotPrefab;
    private GameObject slotsObject;
    public Sprite placeholder;

    // Start is called before the first frame update
    void Awake()
    {

        slotsObject = GameObject.Find("Slots");
        inventory = FindObjectOfType<Player>().inventory;
        inventory.onInventoryChanged += UpdateInventoryUI;

        // get the slots from the inventory and for each one create a new slot with the icon and count filled 

        foreach (Inventory.Slot slot in inventory.slots)
        {
            GameObject slotObject = Instantiate(inventorySlotPrefab, transform);
            slotObject.transform.SetParent(slotsObject.transform);
            Image slotIcon = slotObject.transform.GetChild(0).gameObject.GetComponent<Image>();
            TMP_Text slotCount = slotObject.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();
            if (slot.icon)
            {
                Debug.Log(slot.icon);
                slotIcon.sprite = slot.icon;
            }
            if (slot.count != 0)
            {
                slotCount.text = slot.count.ToString();
            }
            else
            {
                slotCount.text = "";
            }
        }
    }

    private void UpdateInventoryUI(Inventory.Slot slot)
    {


        if (slot.count == 0)
        {
            clearSlot(slot);
        }
        else
        {
            UpdateSlot(slot);
        }

    }

    private void UpdateSlot(Inventory.Slot slot)
    {
        GameObject slotObject = slotsObject.transform.GetChild(slot.index).gameObject;
        Image slotIcon = slotObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        TMP_Text slotCount = slotObject.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();
        slotIcon.sprite = slot.icon;
        slotCount.text = slot.count.ToString();
    }

    void clearSlot(Inventory.Slot slot)
    {
        slot.icon = null;
        slot.count = 0;
        slot.type = "";
    }
    void OnDestroy()
    {
        // Unsubscribe to avoid memory leaks
        inventory.onInventoryChanged -= UpdateInventoryUI;
    }


}
