using UnityEngine;

public class ConsumptionManager : MonoBehaviour
{
    // 물건을 사는 시스템
    // 모든 오브젝트는 물건을 
    
    public static ConsumptionManager Instance;
    
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
    }
    
    public bool Consumption(double itemPrice)
    {
        if (PersonalFinancialManager.Instance.currentMoney < itemPrice)
        {
            Debug.Log("물건 구매 X");
            return false;
        }
        else
        {
            Debug.Log("물건 구매 O");
            PersonalFinancialManager.Instance.OutputMoney(itemPrice);
            return true;
        }
    }

}
