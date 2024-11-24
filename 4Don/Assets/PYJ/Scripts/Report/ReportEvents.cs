using System;
using UnityEngine;

public class ReportEvents // 게임 오브젝트에 붙여서 작동할 필요가 없으니 모노비해비어 사용하지 않음
{
  // 퀴즈 
  public event Action<string, float> onBuyStock; 
  // 이거 부르면 이걸 구독하고 있는 매소드에 알림이 가고 작동하게 됨 
  // 해야할 일: 필요한 곳에 이걸 구독하게 하기
  // GameEventsManager.instance.reportEvents.onBuyStock += 주식 샀을 때 백앤드랑 통신하는 매서드

  
  // 주식 을 샀을 때
  // 주식 구매를 했을 때 이 메서드를 부르게 한다. //GameEventsManager.instance.reportEvents.BuyStock(매개변수 넣어서);
  // 그럼 이 매소드가 발생하고 안에 있는 코드가 작동한다. 
  // 안에 있는 코드는 저걸 구독하고 있는 모든 매소드를 불러준다. 
  // 따라서 주식 구매를 했을 떄 이 매서드를 부르고 저 이벤트가 발생했을 떄 어떤 매소드가 작동할지 구독만 해주면 된다.
  public void BuyStock(string stockName,float stockPrice)
  {
    onBuyStock?.Invoke(stockName, stockPrice); 
  }
  
  //주식 살때 
  //

  /*private void Update()
  {
   if(Input.GetKeyDown(Keycode.Space))
   {
    GameEventsManager.instance.reportEvents.BuyStock(매개변수 넣어서)
   }
  }*/
  
  // 이 클래스로 한번에 관리하고 데이터 보내버리고 싶다면 
  // 레포트에 필요한 통신을 모두 적어 놓을 것ㅠ


}
