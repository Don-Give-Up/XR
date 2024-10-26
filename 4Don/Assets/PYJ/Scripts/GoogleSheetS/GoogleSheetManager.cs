using System;
using Google.Apis.Auth.OAuth2;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.Overlays;
using UnityEngine;
using static GoogleSheets;
public class GoogleSheetManager : MonoBehaviour
{
    public string googleSpreadsheetId = "your-spreadsheet-id";
    public string credentialsNameInStreamingAssets = "your-credentials.json";

    public static GoogleSheetManager Instance;
    
    public struct Finance
    {
        public int Year { get; set; } //년도 
        public double Price { get; set; } //물가
        public double Salary { get; set; } //월급 
        public double Rate { get; set; } //금리 
        public double BreadPrice { get; set; } //빵가격
        public double Tax { get; set; } // 세금

        public Finance(int year, double price, double salary, double rate, double breadPrice, double tax)
        {
            Year = year;
            Price = price;
            Salary = salary;
            Rate = rate;
            BreadPrice = breadPrice;
            Tax = tax;
        }
    }
    
    public struct Stock 
    {
        public int Day { get; set; }
        public double StockPrice { get; set; }

        public Stock(int day, double stockPrice)
        {
            Day = day;
            StockPrice = stockPrice;
        }
    }

    public bool IsLoaded;

    // 전역 변수로 딕셔너리 저장
    private static Dictionary<int, Finance> government = new Dictionary<int, Finance>();
    private static Dictionary<int, Stock> stocks = new Dictionary<int, Stock>();
    
    private void Awake()
    {
        //GoogleSheetManager.Instance 가 null 인 걸 대비
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        Process();
    }

    public void Process()
    {
        Initialize(googleSpreadsheetId, $"{Application.streamingAssetsPath}/{credentialsNameInStreamingAssets}");
        
        DataGet();
    }

    public async void DataGet()
    {
        var data = await Get("Whole!A2:F29"); // 범위
        
        foreach (var row in data)
        {
            //Debug.Log(string.Join(", ", row));
            
            var yearlydata = new Finance(
                Convert.ToInt32(row[0]),
                Convert.ToDouble(row[1]),
                Convert.ToDouble(row[2]),
                Convert.ToDouble(row[3]),
                Convert.ToDouble(row[4]),
                Convert.ToDouble(row[5])
            );
            
            government.Add(yearlydata.Year, yearlydata);
        }

        var data2 = await Get("Stock!B2:C141");

        foreach (var row in data2)
        {
            var dayilydata = new Stock(
                Convert.ToInt32(row[0]),
                Convert.ToDouble(row[1])
            );
            
            stocks.Add(dayilydata.Day, dayilydata);
        }
        
        IsLoaded = true;
    }

    //년도 입력하면 데이터 주는 쪽
    public Finance YearlyDataGet(int year)
    {
        return government[year];
    }

    // 하루 데이터 제공
    public Stock DailyDataGet(int day)
    {
        return stocks[day];
    }
}
