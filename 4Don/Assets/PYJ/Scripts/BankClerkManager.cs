using UnityEngine;

public class BankClerkManager : MonoBehaviour
{
    public static BankClerkManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Visit()
    {
        // 대화창 생성
    }
    
    // 클릭하면 
    // 대)안녕하세요. 무슨일로 오셨나요? 
    // 선)저축이나 주식 버튼 제공 후 선택 받음 
    // 대)저축 이라고 선택하면 저축 말씀이시군요?!?
    // 대)저희는 자유 예적금, 정기 예금, 정기 적금 3가지 상품을 제공하고 있습니다. 
    // 대)어떤 상품에 가입하실 건가요? 
    // 선)3가지 중 하나 선택 
    // 선)자유 예적금 상품 선택 
    // 대)자유 예적금은 돈을 자유롭게 입출급할 수 있는 상품입니다. 좋은 결정이네요!
    // 선)하고 입금하실 금액을 선택해 주세요 -> 입력 받음 
    // //현재 금리 입니다. 
    // //다음주에 받으실 예상 이자입니다. 
    // 대)감사합니다. 
    // 대화 종료
    
    

}
