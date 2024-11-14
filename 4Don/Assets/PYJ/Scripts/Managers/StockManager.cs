using UnityEngine;

public class StockManager : MonoBehaviour
{
 // 주식 상품은 일단 한개 
 // 일단 한 개의 주식 상품 
// 주식의 특성 
// 그때의 가격 (한 주당 가격) 만 있으면 될 듯? 

// 1. 주식 가격은 날마다 변화함
// 2. 한 번 살 때 몇 주를 살 것인지 입력 받음 
// 3. 날마다 가격을 계산함
 private double currentStockMoney = 0;
 private double currentStockPrice = 0; 

 private void Awake()
 {
  RoundSystem.Instance.onRoundChange += StockPrice;
 }

 private void StockPrice(int round)
 {
   currentStockPrice = GoogleSheetManager.Instance.YearlyStockDataGet(round).ITStockPrice;
 }
    
 private void BuyStock(int count)
 {
  // 몇 주 살 건지 물어봄 
  double wantToBuy = currentStockPrice * count;

  if (ConsumptionManager.Instance.Consumption(wantToBuy))
  {
   currentStockMoney += wantToBuy;
   PersonalFinancialManager.Instance.OutputMoney(wantToBuy);
  }
 }

 private void wantToSell(int count)
 {
  double wantToSell = currentStockPrice * count;

  if (ConsumptionManager.Instance.Consumption(wantToSell))
  {
   currentStockMoney -= wantToSell;
   PersonalFinancialManager.Instance.InputMoney(wantToSell);
  }
 }
 
}
