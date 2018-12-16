using UnityEngine;

public class InventorySlot
{
    [SerializeField]
    private bool m_isLock;
    [SerializeField]
    private Item m_item;
    [SerializeField]
    private int m_quantity;


    public Item Item { get { return m_item; } }
    public int Quantity { get { return m_quantity; } set { m_quantity = value; } }
    public bool IsLock { get { return m_isLock; } set { m_isLock = value; } }

}
