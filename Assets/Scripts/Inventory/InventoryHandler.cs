using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour
{

    public Inventory inventory;
    public GameObject inventorySlotPrefab;
    private GameObject slotsObject;
    public Sprite placeholder;

    // Start is called before the first frame update


    void Start()
    {
        Debug.Log("HERE");
        slotsObject = GameObject.Find("Slots");
        inventory = FindObjectOfType<Player>().inventory;
        inventory.onInventoryChanged += UpdateInventoryUI;


        // get the slots from the inventory and for each one create a new slot with the icon and count filled 

        foreach (Inventory.Slot slot in inventory.slots)
        {
            GameObject slotObject = Instantiate(inventorySlotPrefab, transform);
            slotObject.transform.SetParent(slotsObject.transform);
            slotObject.transform.localScale = new Vector3(1, 1, 1);
            Image slotIcon = slotObject.transform.GetChild(0).gameObject.GetComponent<Image>();
            ItemDrop itemDrop = slotObject.transform.GetChild(1).gameObject.GetComponent<ItemDrop>();
            itemDrop.index = slot.index;
            itemDrop.onDropClick += Drop;
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
                slotObject.transform.GetChild(1).gameObject.SetActive(false);
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
        slotObject.transform.GetChild(1).gameObject.SetActive(true);

        TMP_Text slotCount = slotObject.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();
        slotCount.gameObject.SetActive(true);
        slotIcon.sprite = slot.icon;
        slotCount.text = slot.count.ToString();

    }

    void clearSlot(Inventory.Slot slot)
    {
        GameObject slotObject = slotsObject.transform.GetChild(slot.index).gameObject;

        slot.icon = placeholder;
        slot.count = 0;
        slot.type = "";
        slotObject.transform.GetChild(1).gameObject.SetActive(false);
        slotObject.transform.GetChild(2).gameObject.SetActive(false);
        slotObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = placeholder;

    }


    public void Drop(int index)
    {
        Inventory.Slot slot = inventory.slots[index];
        if (slot.count > 0)
        {
            slot.count--;

            GameObject collectablePrefab = Resources.Load<GameObject>("Prefabs/Collectable");
            SpawnItem(collectablePrefab, slot);
            UpdateInventoryUI(slot);
            // spawn a new item at the player's position
            // get the collectable prefab from the folder

        }
    }

    void SpawnItem(GameObject collectablePrefab, Inventory.Slot slot)
    {
        Vector3 playerPosition = FindObjectOfType<Player>().transform.position;
        float offsetDistance = 1.0f; // Adjust this distance
        Vector3 spawnPosition = playerPosition + new Vector3(offsetDistance, offsetDistance, 0);

        GameObject item = Instantiate(collectablePrefab, playerPosition, Quaternion.identity); // Start at player's position
        item.GetComponent<SpriteRenderer>().sprite = slot.icon;

        Collectable collectableScript = item.GetComponent<Collectable>();
        collectableScript.type = slot.type;
        collectableScript.icon = slot.icon;

        // Temporarily disable the pickup functionality or collider
        Collider2D collider = item.GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false;

        // Start the animation coroutine
        StartCoroutine(MoveItemToPosition(item, spawnPosition, 1.0f)); // Duration of 1 second
    }

    IEnumerator MoveItemToPosition(GameObject item, Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0;
        Vector3 startPosition = item.transform.position;
        Debug.Log("Coroutine started for moving item.");

        while (elapsedTime < duration)
        {
            Debug.Log("time elapsed: " + elapsedTime);
            item.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        item.transform.position = targetPosition;
        Debug.Log("Coroutine ended, item reached target position.");

        // Re-enable the pickup functionality or collider
        Collider2D collider = item.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = true;
            Debug.Log("Collider re-enabled.");
        }
        else
        {
            Debug.LogError("Collider not found on the item.");
        }
    }


    void OnDestroy()
    {
        try
        {
            inventory.onInventoryChanged -= UpdateInventoryUI;
            // Unsubscribe to avoid memory leaks
            inventory.onInventoryChanged -= UpdateInventoryUI;

            foreach (Inventory.Slot slot in inventory.slots)
            {
                ItemDrop itemDrop = slotsObject.transform.GetChild(slot.index).gameObject.GetComponent<ItemDrop>();
                itemDrop.onDropClick -= Drop;
            }
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log(e);
        }
    }


}
