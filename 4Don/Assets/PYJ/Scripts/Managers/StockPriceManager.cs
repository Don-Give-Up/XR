using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class StockPriceManager : MonoBehaviour
{
    // 일단 돈의 가격만 보이게 함 

    public TMP_Text[] stockCurrentObj;
    
    private double[] roundStockPrices;
    private double[] beforeRoundStockPrice;
    
    public double wantToBuyStockPrice = 0;

    //public Action<double> OnWantToBuyStock; 
    
    private void OnEnable()
    {
        RoundSystem.Instance.onRoundChange += RoundStockPriceGet; // 라운드가 바뀔 때 가격이 바뀜
    }

    private void RoundStockPriceGet(int year)
    {
        var stockPrices = GoogleSheetManager.Instance.YearlyStockDataGet(year);

        roundStockPrices[0] = stockPrices.ITStockPrice;
        roundStockPrices[1] = stockPrices.ThreeStarStockPrice;
        roundStockPrices[2] = stockPrices.HoldingsStockPrice;
        roundStockPrices[3] = stockPrices.BioStockPrice;
        roundStockPrices[4] = stockPrices.FoodStockPrice;
        roundStockPrices[5] = stockPrices.InnovationStockPrice;
        roundStockPrices[6] = stockPrices.AirStockPrice;
        roundStockPrices[7] = stockPrices.ElectonicCarStockPrice;

        var beforStockPrice = GoogleSheetManager.Instance.YearlyStockDataGet(year - 1);
        
        beforeRoundStockPrice[0] = beforStockPrice.ITStockPrice;
        beforeRoundStockPrice[1] = beforStockPrice.ThreeStarStockPrice;
        beforeRoundStockPrice[2] = beforStockPrice.HoldingsStockPrice;
        beforeRoundStockPrice[3] = beforStockPrice.BioStockPrice;
        beforeRoundStockPrice[4] = beforStockPrice.FoodStockPrice;
        beforeRoundStockPrice[5] = beforStockPrice.InnovationStockPrice;
        beforeRoundStockPrice[6] = beforStockPrice.AirStockPrice;
        beforeRoundStockPrice[7] = beforStockPrice.ElectonicCarStockPrice;
    
        Debug.Log(" 가격 받았음 둥");
        StockPrice();
    }

    private async UniTask StockPrice() // 값에 변동을 주는 코드
    {
        for (int i = 0; i < roundStockPrices.Length; i++)
        {
            float lowerBound = (float)(-(roundStockPrices[i] / 100) * 1);
            float upperBound = (float)((roundStockPrices[i] / 100) * 1);
            
            // 현재 주식 가격과, 변동량 계산
            double currentStockPrice = roundStockPrices[i] + Random.Range(lowerBound, upperBound);
            double dayOverDay = (((currentStockPrice - beforeRoundStockPrice[i]) / beforeRoundStockPrice[i]) * 100);

            StockText(i, currentStockPrice, dayOverDay);
        }

        await UniTask.WaitForSeconds(1f);

        Debug.Log("1F 기다림");
        StockPrice();

    }

    private void StockText(int i, double stockPrice, double dayDif)
    {
        Debug.Log("가격 표시하게 할 거임");
        stockCurrentObj[i].text = stockPrice.ToString("N0");
        stockCurrentObj[i + 1].text = dayDif.ToString("N0");
    }

    public void WantToBuyStock(int stockNum) // 사려고 누른 순간 가격
    {
         wantToBuyStockPrice = roundStockPrices[stockNum]; // 선택한 주식의 가격을 반환하게
         //OnWantToBuyStock?.Invoke(wantToBuyStockPrice);
    }
}
