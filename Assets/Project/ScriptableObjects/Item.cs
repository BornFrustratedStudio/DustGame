using UnityEngine;

public enum Rarity
{
    Common,
    UnCommon,
    Rare,
    Epic,
    Legendary,
    Unique
}

[CreateAssetMenu(menuName ="Item/NewItem")]
public class Item : ScriptableObject
{
    [Header("Item ID")]
    [SerializeField]
    private int m_id;
    [Header("Item Name")]    
    [SerializeField]
    private string m_name;
    [Header("Item Description")]
    [SerializeField]
    private string m_description;
    [Header("Item Image")]
    [SerializeField]
    private Sprite m_graphic;
    [Header("Item Rarity")]
    [SerializeField]
    private Rarity m_rarity;
    [Header("Recipe to make this Item")]
    [SerializeField]
    private Recipe m_recipe;
    

    public int Id               { get { return m_id; }          set { m_id = value; } }
    public Sprite Graphic       { get { return m_graphic; }     set { m_graphic = value; } }
    public string Name          { get { return m_name; }        set { m_name = value; } }
    public string Description   { get { return m_description; } set { m_description = value; } }
    public Rarity Rarity        { get { return m_rarity; }      set { m_rarity = value; } }
    public Recipe Recipe        { get { return m_recipe; }      set { m_recipe = value; } }
}
