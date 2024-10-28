using TMPro;
using UnityEngine;

public class MoneyTextManager : MonoBehaviour
{

    public TMP_Text currentMoneyText;
    
    public void Money(double currentMonney)
    {
        currentMoneyText.text = $"보유 현금\n{(int)currentMonney}만원";
    }
}
