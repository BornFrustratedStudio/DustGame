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
public class Item : MonoBehaviour
{
    [SerializeField]
    private int m_id;
    [SerializeField]
    private Sprite m_graphic;
    [SerializeField]
    private string m_name;
    [SerializeField]
    private string m_description;
    [SerializeField]
    private Rarity m_rarity;
    [SerializeField]
    private Recipe m_recipe;

    public int Id               { get { return m_id; }          set { m_id = value; } }
    public Sprite Graphic       { get { return m_graphic; }     set { m_graphic = value; } }
    public string Name          { get { return m_name; }        set { m_name = value; } }
    public string Description   { get { return m_description; } set { m_description = value; } }
    public Rarity Rarity        { get { return m_rarity; }      set { m_rarity = value; } }
    public Recipe Recipe        { get { return m_recipe; }      set { m_recipe = value; } }
}
