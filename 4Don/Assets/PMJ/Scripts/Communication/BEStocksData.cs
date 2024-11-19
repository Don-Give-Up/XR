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
    public int gameId;
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

[System.Serializable]
public class GameMemberId
{
    public int gameId;
}

[System.Serializable]
public class QuizSolve
{
    public int gameId;
    public int quizId;
    public string correct;
}

[System.Serializable]
public class QuizSloveMember
{
    public int quizSolveRecordId;
    public int gameMemberId;
    public int quizId;
    public string createdAt;
    public string correct;
    public int quizCorrectMoney;
}

[System.Serializable]
public class GameMemberIdPost
{
    public int gameMemberId { get; set; }
    public int memberId { get; set; }
    public int gameId { get; set; }
    public int gameMemberMoney { get; set; }
}