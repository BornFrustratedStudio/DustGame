using System.Collections;
using System.Collections.Generic;
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

public abstract class Droppable : ScriptableObject
{
    [Header("Object Info")]
    [Header("Object ID")]
    [SerializeField]
    protected int m_id;
    [Header("Object Name")]    
    [SerializeField]
    protected string m_name;
    [Header("Object Description")]
    [SerializeField]
    protected string m_description;
    [Header("Object Image")]
    [SerializeField]
    protected Sprite m_graphic;

    [Header("Object Drop Info")]
    [Header("Object DropRate")]
    [SerializeField]
    protected float m_dropRate;
    [Header("Object Rarity")]
    [SerializeField]
    protected Rarity m_rarity;  

    public int    Id            { get { return m_id;          } set { m_id = value;          } }
    public Sprite Graphic       { get { return m_graphic;     } set { m_graphic = value;     } }
    public string Name          { get { return m_name;        } set { m_name = value;        } }
    public string Description   { get { return m_description; } set { m_description = value; } }
    public Rarity Rarity        { get { return m_rarity;      } set { m_rarity = value;      } }
    public float  DropRate      { get { return m_dropRate;    } set { m_dropRate = value;    } }
    
    
}
