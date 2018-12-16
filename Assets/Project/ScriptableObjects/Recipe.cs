using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class Recipe : Droppable
{
    [SerializeField]
    private List<Item>  m_recipeItems;

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
