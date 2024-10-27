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

                    ItemInfo itemInfo = InventoryManager.Instance.itemInfoList[(int)ItemType.Bread1]; // null
                    // consumptionManager, PersonalFinancialManager 가 필요함

                    if (itemInfo == null)
                    {
                        Debug.Log("ItemInfo 없음");
                        return;
                    }

                    double itemPrice = itemInfo.price;

                    if (ConsumptionManager.Instance.Consumption(itemPrice))
                    {
                        inventoryManager.AddItem(ItemType.Bread1);
                        Destroy(hit.transform.gameObject);
                    }

                }
                else if (hit.transform.CompareTag("Bread5"))
                {
                    ItemInfo itemInfo = InventoryManager.Instance.itemInfoList[(int)ItemType.Bread5]; // null
                    // consumptionManager, PersonalFinancialManager 가 필요함

                    if (itemInfo == null)
                    {
                        Debug.Log("ItemInfo 없음");
                        return;
                    }

                    double itemPrice = itemInfo.price;

                    if (ConsumptionManager.Instance.Consumption(itemPrice))
                    {
                        inventoryManager.AddItem(ItemType.Bread5);
                        Destroy(hit.transform.gameObject);
                    }

                }
                else if (hit.transform.CompareTag("Bread10"))
                {
                    ItemInfo itemInfo = InventoryManager.Instance.itemInfoList[(int)ItemType.Bread10]; // null
                    // consumptionManager, PersonalFinancialManager 가 필요함

                    if (itemInfo == null)
                    {
                        Debug.Log("ItemInfo 없음");
                        return;
                    }

                    double itemPrice = itemInfo.price;

                    if (ConsumptionManager.Instance.Consumption(itemPrice))
                    {
                        inventoryManager.AddItem(ItemType.Bread10);
                        Destroy(hit.transform.gameObject);
                    }

                }
                else if (hit.collider.CompareTag("ATM"))
                {
                    ATMManager.Instance.ATM();
                }
                else if (hit.collider.CompareTag("Bank"))
                {
                    BankClerkManager.Instance.Visit();
                }

                /*
                Collider collider = hit.collider; // 레이캐스드는 콜라이더를 통해 다른 컨포넌트에 접근
                Item item = collider.GetComponent<Item>(); // Item 에 대해 접근

                // Item 컴포넌트가 없는 경우 처리
                if (item == null) //모든 오브젝트에 아이템 컨포넌트가 없음 <- 문제
                {
                    Debug.Log("Item 컴포넌트 없음");
                    return;
                }

                // info가 없는 경우 처리
                if (item.info == null)
                {
                    Debug.Log("가격 정보 없음");
                    return;
                }

                double itemPrice = item.info.price != null ? item.info.price : 1000; // price가 null일 경우 기본값 설정
                // 삼항 연산자
                // 조건 ? true 일 때 값 : false 일 떄 값

                bool _CanBeItem = ConsumptionManager.Instance.Consumption(itemPrice);

                if (_CanBeItem) // 살 수 있는 가격이면 실행
                {
                    if (hit.transform.CompareTag("Bread1"))
                    {
                        //inventoryManager.AddItem(type);

                        inventoryManager.AddItem(ItemType.Bread1);
                        Destroy(hit.transform.gameObject);
                    }
                    else if (hit.transform.CompareTag("Bread5"))
                    {
                        inventoryManager.AddItem(ItemType.Bread5);
                        Destroy(hit.transform.gameObject);
                    }
                    else if (hit.transform.CompareTag("Bread10"))
                    {
                        inventoryManager.AddItem(ItemType.Bread10);
                        Destroy(hit.transform.gameObject);
                    }
                }
                #1#

            }

        }
    }*/

                /*

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
                                Destroy(hit.transform.gameObject);


                            }
                            else if (hit.transform.CompareTag("Bread5"))
                            {
                                inventoryManager.AddItem(ItemType.Bread5);
                                Destroy(hit.transform.gameObject);
                            }
                            else if (hit.transform.CompareTag("Bread10"))
                            {
                                inventoryManager.AddItem(ItemType.Bread10);
                                Destroy(hit.transform.gameObject);
                            }

                        }


                    }
                }
                */
            }
        }
    }
}

