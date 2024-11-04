using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

[Serializable]
public class ItemInfo // 아이템인포를 분할
{
    public string name;
    public Sprite icon;
    public double price;
    public ItemType type;
    // 배고픔에 대한 항목이 추가되면 좋을듯 = level
    // 혹은 다른 특성에 대한 항목 추가 
    // scriptableObject로 관리할 수 있게 만드는 게 더 좋을 듯?
}

public enum ItemType // 이걸 통합해도 될 듯?
{
    Bread1,
    Bread5,
    Bread10,
}


public class InventoryManager : MonoBehaviour
{
   
    // 필수상점에서 1) 진열대를 누르면 인벤토리창처럼 구매할 수 있는 창이 뜨게 하거나
    // 2) 해당 상품 근처로 가면 NPC가 해당 상품에 대한 정보(이 빵은 배고픔 게이지가 5 찹니다, 가격은 2000원입니다. 구매하시겠습니까?)
    // 이후 살래 or 안 살래 중 선택하여 구매 / 인벤토리에 저장 / 단, 필수상점 내의 빵은 사라지지 않음 인벤토리에 추가될 땐 아이콘 + 개수 (빵, 3)
    // 2번 선택 에쁘니까~~~~~
    
    /// Slot list
    public Slot[] slotList;
    
    /// Item Prefab
    public GameObject itemPrefab;

    public ItemInfo[] itemInfoList;
    
    public static InventoryManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        gameObject.SetActive(false);
    }


    /// Add Item
    public void AddItem(ItemType type)
    {
        //await UniTask.WaitUntil(() => true); // 빵 가격을 확인해서 내가 가진 현금보다 작은지 확인 후 진행
            
        Slot emptySlot = null;
        foreach (var slot in slotList)
        {
            if (!slot.HasItem)
            {
                emptySlot = slot;
                break;
            }
        }

        if (emptySlot == null)
        {
            Debug.LogWarning("빈 슬롯이 없음");
            return;
        }

        GameObject itemGO = Instantiate(itemPrefab, emptySlot.transform);
        Item item = itemGO.GetComponent<Item>();
        ItemInfo itemInfoInfo = itemInfoList[(int)type];
        item.SetInfo(itemInfoInfo);
        
        // 하기 전에 판단
        emptySlot.Add(item);

    }

    /// Remove Item
    public void RemoveItem(Item item)
    {
        // 내가 써졌다 하고 알려주고 지워야 함
        // 여기서 써진 건 지우는 코드 하나만 있으면 됨
        
        foreach (var slot in slotList)
        {
            if (slot.HasItem && slot.currentItem == item)
            {
                slot.RemoveItem(item); // 슬롯에서 아이템 제거
                break;
            }
        }
    }
}
