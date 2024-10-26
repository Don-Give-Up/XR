using UnityEngine;

public class SavingProduct : MonoBehaviour //ScriptableObject
{
  //저축 상품에 대해 정의 
  public SavingType type; // 타입
  public double interestRate; // 금리 
  public int period; // 만기 기간
  
  /*
  public double mininumDeposit; // 최소 예치금 
  public double interestPaymentCycle; // 이자 지급 주기
  public double penalty;// 중도해지 할 시 패널티 
  public double limit;// 가입한도
  public double bonus; // 특정 조건을 충족하는 결우 추가로 지급되는 금리
  */
}

//은행에서 제공할 저축 타입
public enum SavingType
{
  freeSaving,
  termDeposit,//한번에 큰 금액 
  fixedDeposit, // 여러번에 나누워 금액입금 // 적금은 단리로 이자 지급
  
}

// 이자소득세 15.4 % (2019년), (https://eiec.kdi.re.kr/publish/columnView.do?cidx=12293&sel_year=2019&sel_month=11)

// 기본 금리, 최고금리(우대금리 포함), 최고금리 적용 가능 금액, 이자 지급 방식, 상세보기

// 정기 예금의 금리 = +0.5~1.0% 사이 
// 정기 적금의 금리 = +0.2~0.5% 사이