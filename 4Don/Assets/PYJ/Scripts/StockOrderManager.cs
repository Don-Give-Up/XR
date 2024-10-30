using System;
using TMPro;
using UnityEditor;
using UnityEngine;

public class StockOrderManager : MonoBehaviour
{
    public TMP_Text canToBuyText ;
    public TMP_Text wantText;
    public TMP_Text warningText;
    private int wantToBuy = 0;
    private double stockPrice = 0;

    private int canToBuy; 
    public int currentStock = 500; // 주식 가격이랑 연동
    public double personalMoney = 5500;
    private bool isOrdered = false; 
    
    private void Awake()
    {
        // 
        Set();
    }

    private void Set()
    {
        wantText.text = "";
        warningText.text = "";
        currentStock = 500; // 매일 바뀜
        personalMoney = 5500; // 거래가 발생하면 바뀜
        isOrdered = false; 
        canToBuy = (int)(personalMoney / currentStock); // 내림 값으로 얼마나 살 수 있는 지 게산 
        CanBuyText(canToBuy, currentStock); // 거래 가능 수량 업데이트 
    }

    public void NumKeyPadText(int keyNum)
    {
        string currentstockNum = wantToBuy.ToString() + keyNum.ToString();
        Debug.Log($"{currentstockNum}");
        wantToBuy = int.Parse(currentstockNum); // 원하는 주식 주 갯수
        MoneyCheck(wantToBuy);
    }

    public void Delete()
    {
        wantText.text = "";
        wantToBuy = 0;
        warningText.text = "";
        /*
        int length = wantToBuy.ToString().Length; // 이 단어의 길어
        string newText = "";

        for (int i = 0; i < length - 1; i++)
        {
            newText += wantText.text[i];
        }

        //TextUpdate(newText);
        */
    }

    public void MoneyCheck(int wantToBuy)
    {
        stockPrice = currentStock * wantToBuy;
        if (stockPrice <= personalMoney)
        {
            Debug.Log("거래 가능 수량");
            // 이때만 구매되게 
            TextUpdate(wantToBuy,stockPrice);
            isOrdered = true; 
            
        }
        else
        {
            Debug.Log("거래 불가 수량");
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
            Debug.Log("거래가 완료되었습니다.");
            Set();
        }
        else
        {
            warningText.text = "불가능한 거래 입니다.";
        }
    }

    private void CanBuyText(int canToBuy, double currentStock)
    {
        double canBuyStock = canToBuy * currentStock;
        canToBuyText.text = $"{canToBuy}({canBuyStock:N0})";
    }
}

// 연결이 필요하다. 
