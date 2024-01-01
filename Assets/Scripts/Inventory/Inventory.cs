using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Inventory
{
    public delegate void OnInventoryChanged(Slot slot);
    public event OnInventoryChanged onInventoryChanged;

    [System.Serializable]
    public class Slot
    {
        public string type;
        public int count;
        public int maxAllowed;
        public Sprite icon;
        public int index = -1;

        public Slot()
        {
            type = "";
            count = 0;
            maxAllowed = 999;
            icon = null;
        }

        public bool canAddItem()
        {
            // log this (count:maxallowed) with the variables instead of the strings


            if (count < maxAllowed)
            {
                return true;
            }
            return false;
        }
        public void addItem(Collectable item)
        {
            type = item.type;
            icon = item.icon;


            count++;
        }
    }

    public List<Slot> slots = new List<Slot>();

    public Inventory(int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            Slot slot = new()
            {
                index = i
            };
            slots.Add(slot);
        }
    }
    public bool Add(Collectable item)
    {
        foreach (Slot slot in slots)
        {

            if (slot.type == item.type && slot.canAddItem())
            {
                slot.count++;
                onInventoryChanged?.Invoke(slot);

                return true;
            }
        }
        foreach (Slot slot in slots)
        {
            if (slot.type == "")
            {
                slot.addItem(item);
                onInventoryChanged?.Invoke(slot);

                return true;
            }
        }
        return false;
    }

}
