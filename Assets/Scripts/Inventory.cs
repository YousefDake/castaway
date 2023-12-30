using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Inventory 
{
    [System.Serializable]
   public class Slot
    {
        public string Name;
        public int count;
        public int maxAllowed;

        public Slot()
        {
            Name = "";
            count = 0;
            maxAllowed = 99;
        }
        
        public bool canAddItem()
        {
            if (count<maxAllowed)
            {
                return true;
            }
            return false;
        }
        public void addItem(string name)
        {
            this.Name = name;
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
    public void Add(string item)
    {
        foreach (Slot slot in slots)
        {
            if (slot.Name==item && slot.canAddItem())
            {
                slot.addItem(item);
                return;
            }
        }
        foreach (Slot slot in slots)
        {
            if (slot.Name == item)
            {
                slot.addItem(item);
                return ;
            }
        }
    }
}
