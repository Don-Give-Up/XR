using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool HasItem => currentItem != null;

    private Item currentItem;

    public void Add(Item item)
    {
        currentItem = item;
    }
    
    
    
    
    
}
