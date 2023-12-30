using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Inventory 
{
    [System.Serializable]
   public class Slot
    {
        public ItemType type;
        public int count;
        public int maxAllowed;

        public Slot()
        {
            type = ItemType.NONE;
            count = 0;
            maxAllowed = 99;
        }
        
        public bool canAddItem()
        {
            if (count<=maxAllowed)
            {
                return true;
            }
            return false;
        }
        public void addItem(ItemType type)
        {
            this.type = type;
            Debug.Log(type);
            Debug.Log(this);
            count++;
        }
    }
    public List<Slot> slots=new List<Slot>();

    public Inventory(int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }
    public void Add(ItemType type)
    {
        Debug.Log(type);
        foreach (Slot slot in slots)
        {
            
            if (slot.type==type && slot.canAddItem())
            {
                slot.count++;
                return;
            }
        }
        foreach (Slot slot in slots)
        {
            if (slot.type == ItemType.NONE)
            {
                slot.addItem(type);
                return ;
            }
        }
    }
}
