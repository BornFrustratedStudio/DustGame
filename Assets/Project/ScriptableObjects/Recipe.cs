using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class Recipe : ScriptableObject
{
    [SerializeField]
    private string      m_name;
    [SerializeField]
    private Rarity      m_rarity;
    [SerializeField]
    private float       m_dropRate;
    [SerializeField]
    private List<Item>  m_recipeItems;

    public string       Name        { get { return m_name;      } }
    public Rarity       Rarity      { get { return m_rarity;    } }
    public float        DropRate    { get { return m_dropRate;  } }
    public List<Item>   RecipeItems
    {
        get 
        {
            if(m_recipeItems == null)
                m_recipeItems = new List<Item>();

            return m_recipeItems;
        }
    }
}
