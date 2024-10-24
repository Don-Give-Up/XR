using System;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public InventoryManager inventoryManager;

    
    public void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) // 마우스 좌측키로 클릭
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // 레이캐스트를 통해 클릭한 오브젝트인지
            {
                if (hit.transform.CompareTag("Bread1"))
                {
                    //inventoryManager.AddItem(type);
                    inventoryManager.AddItem(ItemType.Bread1);
                    
                }
                else if (hit.transform.CompareTag("Bread5"))
                {
                    inventoryManager.AddItem(ItemType.Bread5);
                }
                else if (hit.transform.CompareTag("Bread10"))
                {
                    inventoryManager.AddItem(ItemType.Bread10);
                }
                
            }
            

        }
    }
}
