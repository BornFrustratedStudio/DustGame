using UnityEngine;

[CreateAssetMenu(menuName ="Item/NewItem")]
public class Item : Droppable
{
    [Header("Recipe to make this Item")]
    [SerializeField]
    private Recipe m_recipe;
    
}
