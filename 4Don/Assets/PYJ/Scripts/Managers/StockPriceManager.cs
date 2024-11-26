using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class StockPriceManager : MonoBehaviour
{
    // 일단 돈의 가격만 보이게 함 
    public TMP_Text[] stockCurrentObj;
    
    // 주식 가격 및 전일 가격을 저장할 배열
    public double[] roundStockPrices = new double[8];  // 예시로 8개의 주식
    private double[] beforeRoundStockPrice = new double[8];
    
    public double wantToBuyStockPrice = 0;
    
    private Color red = Color.red;
    private Color blue = Color.blue;
    private Color block = Color.black;

    // 주식 구매 관련 이벤트
    public Action<double> onWantToBuyStock;

    // 싱글톤
    public static StockPriceManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        RoundSystem.Instance.onRoundChange += RoundStockPriceGet; // 라운드가 바뀔 때 가격이 바뀜
        StockPrice().Forget();
    }

    // 라운드 변경 시 주식 가격 업데이트
    private void RoundStockPriceGet(int year)
    {
        var stockPrices = GoogleSheetManager.Instance.YearlyStockDataGet(year);

        roundStockPrices[0] = stockPrices.ITStockPrice; //stockPrices.ITStockPrice;//133703;
        roundStockPrices[1] = stockPrices.ThreeStarStockPrice; //stockPrices.ThreeStarStockPrice;67100;
        roundStockPrices[2] = stockPrices.HoldingsStockPrice; //stockPrices.HoldingsStockPrice;4180;
        roundStockPrices[3] = stockPrices.BioStockPrice; //stockPrices.BioStockPrice;43533;
        roundStockPrices[4] = stockPrices.FoodStockPrice; //stockPrices.FoodStockPrice;102000;
        roundStockPrices[5] = stockPrices.InnovationStockPrice; //stockPrices.InnovationStockPrice;176000;
        roundStockPrices[6] = stockPrices.AirStockPrice; //stockPrices.AirStockPrice;25950;
        roundStockPrices[7] = stockPrices.ElectonicCarStockPrice; //stockPrices.ElectonicCarStockPrice;220096;

        foreach (var a in roundStockPrices)
        {
            Debug.Log($"{a}");
        }

        int beforyear = year - 1;
        var beforStockPrice = GoogleSheetManager.Instance.YearlyStockDataGet(beforyear);

        beforeRoundStockPrice[0] = beforStockPrice.ITStockPrice;//beforStockPrice.ITStockPrice;32807;
        beforeRoundStockPrice[1] = beforStockPrice.ThreeStarStockPrice;//beforStockPrice.ThreeStarStockPrice;1294000;
        beforeRoundStockPrice[2] = beforStockPrice.HoldingsStockPrice;//beforStockPrice.HoldingsStockPrice;8830;
        beforeRoundStockPrice[3] = beforStockPrice.BioStockPrice;//beforStockPrice.BioStockPrice;32343;
        beforeRoundStockPrice[4] = beforStockPrice.FoodStockPrice;//beforStockPrice.FoodStockPrice;22500;
        beforeRoundStockPrice[5] = beforStockPrice.InnovationStockPrice;//beforStockPrice.InnovationStockPrice;83200;
        beforeRoundStockPrice[6] = beforStockPrice.AirStockPrice;//beforStockPrice.AirStockPrice;41513;
        beforeRoundStockPrice[7] = beforStockPrice.ElectonicCarStockPrice;//beforStockPrice.ElectonicCarStockPrice;17768;
    
        foreach (var a in beforeRoundStockPrice)
        {
            Debug.Log($"{a}");
        }
        Debug.Log("가격 받았음 둥");
        
    }

    // 값에 변동을 주는 코드
    private async UniTask StockPrice()
    {
        while (true)  // 주식 가격을 지속적으로 갱신
        {
            for (int i = 0; i < roundStockPrices.Length; i++)
            {
                // 변동 범위 설정
                float lowerBound = (float)(-(roundStockPrices[i] / 100) * 1);  // -1%
                float upperBound = (float)((roundStockPrices[i] / 100) * 1);   // +1%

                // 현재 주식 가격과, 변동량 계산
                double currentStockPrice = roundStockPrices[i] + Random.Range(lowerBound, upperBound);
                double dayOverDay = (((currentStockPrice - beforeRoundStockPrice[i]) / beforeRoundStockPrice[i]) * 100);
                
                StockText(i, currentStockPrice, dayOverDay);
            }

            // 1초 간격으로 갱신
            await UniTask.Delay(1000);
        }
    }

    // 가격 표시하게 할 거임
    private void StockText(int i, double stockPrice, double dayDif)
    {
        
        stockCurrentObj[2*i].text = stockPrice.ToString("N0");

        if (dayDif > 0)
        {
            stockCurrentObj[2 * i + 1].color = red;
        }
        else if (dayDif < 0)
        {
            stockCurrentObj[2 * i + 1].color = blue;
        }
        else
        {
            stockCurrentObj[2 * i + 1].color = block; 
        }

        stockCurrentObj[2*i + 1].text = dayDif.ToString("N0");
        
    }

    // 사려고 누른 순간 가격 
    public void WantToBuyStock(int stockNum)
    {
        if (stockNum >= 0 && stockNum < roundStockPrices.Length)
        {
            wantToBuyStockPrice = roundStockPrices[stockNum];  // 선택한 주식의 가격을 반환
            onWantToBuyStock?.Invoke(wantToBuyStockPrice);      // 이벤트 호출
        }
    }
}