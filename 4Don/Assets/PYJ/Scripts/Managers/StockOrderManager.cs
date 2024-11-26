using System;
using TMPro;
using UnityEditor;
using UnityEngine;

public class StockOrderManager : MonoBehaviour
{
    public TMP_Text canToBuyText ;
    public TMP_Text wantText;
    public TMP_Text warningText;

    public GameObject priceObj;
    public GameObject orderObj;
    public GameObject dialogueObj; 
    
    private int wantToBuy = 0; //
    private double stockPrice = 0;

    private int canToBuy; 
    
    public double currentStock; // 주식 가격이랑 연동
    public double personalMoney = 50000; // 일단 이거 임의로 설정하기
    
    private bool isOrdered = false;
    
    private double[] stockPrices = new double[8];
    private string[] stockNames = new string[] { "애플IT", "삼성전자", "홀딩스", "바이오", "식품", "에너지", "에어항공", "테슬라"}; // 이거 정하기 
    private int stockNum = 0; // 일단 홀딩스 주식 살 수 있게 하기
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 이거 먼저 일어나야 함니다.
        {
            GetStockPrice(); // 가격 가지고 오는 쪽, 바깥에서 버튼 클릭을 받는 순간 실행 
            // 몇 번 주식을 했는지도 설정
        }

        if (Input.GetKeyDown(KeyCode.Alpha0)) // 그래야 가격에 반영할 수 있음
        {
            // 여기서 들어온 숫자를 stockNum 에 할당하는 거 필요, 다른 곳에서도 사용할 수 있게 
            Set(stockNum); 
        }
    }

    public void Set(int stockNum)
    {
        // 사고 싶은 주식 가지고 오기, 
        // 내 자산으로 얼마나 살 수 있는 지 계산하기 
        // 여기서 내 자산도 가지고 오기 
     
        //GetStockPrice();
        GetStockPrice(); 
        
        priceObj.SetActive(false);
        orderObj.SetActive(true);
        
        wantText.text = "";
        warningText.text = ""; 
        personalMoney = PersonalFinancialManager.Instance.currentMoney; // 이거만 personal 거기서 받아오게 하면 first 가 아니라 Set 으로 사용 가능
        stockPrice = stockPrices[stockNum]; // 일단 주식 가격 할당
        this.stockNum = stockNum;
        canToBuy = (int)(personalMoney / stockPrice); // 몇 주 살 수 있는 지 결정 
        //Debug.Log($"{stockPrice}");
        //canToBuyText.text = $"{canToBuy}({canToBuy*stockPrice:N0})"; // 가격 보여줌 
        //Debug.Log($"{stockNames[stockNum]}: {canToBuy}({canToBuy*stockPrice:N0})");
        CanBuyText(canToBuy, stockPrice*canToBuy);

    }

    private void Start()
    {
        priceObj.SetActive(false);
        orderObj.SetActive(false);
        //StockPriceManager.instance.onWantToBuyStock += CurrentStock; // 주식 구매를 하고 싶으면 FirstSet(int) 가지고 들어오면 됨
        // 거래 발생하면 이벤트 발생하게 하기, 아마 되어 있긴 할 듯? 
    }
    
    private void GetStockPrice()
    {
        for (int i = 0; i < stockPrices.Length; i ++)
        {
            stockPrices[i] = StockPriceManager.instance.roundStockPrices[i];
            //Debug.Log($"주식 가격 다 정해!{stockPrices[i]}");
        }
    }

    public void NumKeyPadText(int keyNum)
    {
        string currentstockNum = wantToBuy.ToString() + keyNum.ToString();
        //Debug.Log($"{currentstockNum}");
        wantToBuy = int.Parse(currentstockNum); // 원하는 주식 주 갯수
        MoneyCheck(wantToBuy);
    }

    public void Delete()
    {
        wantText.text = "";
        wantToBuy = 0;
        warningText.text = "";
    }

    public void MoneyCheck(int wantToBuy)
    {
        currentStock = stockPrice * wantToBuy;
        //stockPrice = currentStock * wantToBuy;
        if (currentStock <= personalMoney)
        {
            // 이때만 구매되게 
            TextUpdate(wantToBuy,currentStock);
            this.wantToBuy = wantToBuy;
            isOrdered = true; 
            
        }
        else
        {
            isOrdered = false;
            warningText.text = "구매가능 수량을 초과하였습니다."; // 경고 문구
        }
    }

    private void TextUpdate(int wantTOBuy, double stockPrice )
    {
        wantToBuy = wantTOBuy;
        wantText.text = $"{wantTOBuy}({stockPrice:N0})";
    }

    public void CheckOrder()
    {
        if (isOrdered)
        {
            PersonalFinancialManager.Instance.OutputMoney(stockPrice * wantToBuy);
            Debug.Log("거래가 완료되었습니다.");
            //Set();
            Set(stockNum);
            
            ReturnDialogue();
            // 이 때 주식 사는 이벤트 발생하면 될 듯 // 
            // CHECK 에 관련된 이벤트 하던가. 
            // 이 때 다이알로그랑 관련된 이벤트 추가
            

            BEStocksRecordPost.stockId = stockNum;
            BEStocksRecordPost.amount = wantToBuy;
            BEStocksRecordPost.stockRecordPost.StockStart();
           
        }
        else
        {
            warningText.text = "불가능한 거래 입니다.";
        }
    }

    private void CanBuyText(int canToBuy, double currentStock)
    {
        canToBuyText.text = $"{canToBuy}({currentStock:N0})";
    }

    public void ReturnDialogue()
    {
        dialogueObj.SetActive(true);
        priceObj.SetActive(false);
        orderObj.SetActive(false);
        
    }
}

