using System;
using System.Collections.Generic;

[Serializable]
public class Stock
{
    public int stockId;
    public string stockName;
    public int stockPrice;
}

[System.Serializable]
public class StockRecord
{
    public int stockId;
    public int stockTradeRecordAmount;
    public string tradeType;
}

[System.Serializable]
public class StockRecordMemberGet
{
    public int stockTradeRecordId;
    public int gameMemberId;
    public int stockTradeRecordAmount;
    public string tradeType;
    public string stockName;
    public int stockTotalPrice;
}

[System.Serializable]
public class ChoiceProduct
{
    public int selectProductId;
    public int selectProductPurchaseAmount;
}

[System.Serializable]
public class ChoiceProductMember
{
    public int selectProductPurchaseRecordId;
    public string selectProductName;
    public int gameMemberId;
    public int selectProductPurchaseAmount;
    public int productTotalPrice;
}

[System.Serializable]
public class BanklogMember
{
    public int bankLogId;
    public int gameMemberId;
    public string savingProductName;
    public int bankTotalPrice;
}

[System.Serializable]
public class Banklogs
{
    public int savingProductId;
    public int bankTotalPrice;
}
