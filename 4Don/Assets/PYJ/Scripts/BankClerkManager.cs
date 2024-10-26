using System;
using System.Collections.Generic;
using UnityEngine;

public class BankClerkManager : MonoBehaviour
{
    // 각 NPC의 대사 리스트를 저장하는 딕셔너리
    private Dictionary<string, List<string>> talkData;
    private List<string> currentDialogueLines;
    private int currentLineIndex;

    private string name = "행행이";

    private int talkIndex = 0; 

    public Queue<string> sentences;
    
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

        talkData = new Dictionary<string, List<string>>(); // 초기화 
        GenerateData();
    }

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void Visit() // 탐지된 쪽에서 불러주는 거
    {
        talkData = new Dictionary<string, List<string>>
        {
            {
                "행행", new List<string>
                {
                    "안녕하세요, 무슨일로 오셨나요?", // 저축과 주식 버튼 제공 
                    
                    "저축 말씀이시군요!", // 저축 선택시 시나리오
                    "저희는 자유 예적금, 정기 예금, 정기 적금 3가지 상품을 제공하고 있습니다.", // 3가지 제공 
                    
                    "어떤 상품에 가입하실 건가요?", // 3가지 제공, 상품에 따라 분기 나뉨
                    "좋은 결정이네요!", // 자유 예적금은 돈을 자유롭게 입출금할 수 있는 상품입니다.
                    "감사합니다.", // 대화 끝
                }
            }
        };
    }

    private void GenerateData()
    {
        talkData.Add(name, new List<string>
            {
                "안녕하세요, 무슨 일로 오셨나요?",
                "저축 말씀이시군요!",
                "저희는 자유 예적금, 정기 예금. 정기 적금 3가지 상품을 제공하고 있습니다.",
                "어떤 상품에 가입하실 건가요?",
                "좋은 결정이네요!",
                "감사합니다.",
            }
        );
    }

    public string GetTalk(string npcName, int talkIndex)
    {
        return talkData[npcName][talkIndex];
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