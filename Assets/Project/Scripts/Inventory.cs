using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> m_slots;

    public void UnlockItem(Item _item)
    {
         InventorySlot _slot = m_slots.Find((x) => x.Item.Id == _item.Id);
        _slot.IsLock = false;
        _slot.Quantity++;
    }

}
