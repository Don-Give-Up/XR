using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{

    public ItemInfo info;
    public Image iconImage;
    private HungryBar hungryBar;


    public void SetInfo(ItemInfo info)
    {
        this.info = info;
        iconImage.sprite = info.icon;
        
    }
    
    

    void Start()
    {
        hungryBar = FindObjectOfType<HungryBar>();
        if (hungryBar == null)
        {
            Debug.LogError("HungryBar 없다");
        }
    }

    public void Use()
    {
        Debug.Log(info.name);

        switch (info.type)
        {
            case ItemType.Bread1:
                OnEatBread1();
                break;
            case ItemType.Bread5:
                OnEatBread5();
                break;
            case ItemType.Bread10:
                OnEatBread10();
                break;
            default:
                Debug.LogWarning("알 수 없는 아이템입니다.");
                break;
        }
        
        // 인벤토리매니저를 불러와서
        GetComponentInParent<InventoryManager>().RemoveItem(this);


    }

    public void OnEatBread1()
    {
        hungryBar.EatBread(1);
        Debug.Log("빵1 섭취, 0.1 증가");
    }

    public void OnEatBread5()
    {
        hungryBar.EatBread(5);
        Debug.Log("빵5 섭취, 0.5 증가");
    }

    public void OnEatBread10()
    {
        hungryBar.EatBread(10);
        Debug.Log("빵10 섭취, 1 증가");
    }


}
