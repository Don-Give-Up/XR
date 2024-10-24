using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool HasItem => currentItem != null;

    public Item currentItem;

    public void Add(Item item)
    {
        currentItem = item;
        // 추가적인 UI 업데이트 필요
    }

    public void Remove()
    {
        currentItem = null;
        // 추가적인 UI 업데이트 필요
    }

    public void RemoveItem(Item item)
    {
        if (currentItem == item)
        {
            Remove(); // 슬롯에서 아이템 제거
            Destroy(item.gameObject); // 아이템 오브젝트 삭제
            Debug.Log("제거되나?");
        }
    }
    
}
