using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogObject;
    public TMP_Text dialogText;
    public TMP_Text npcName;
    
    public GameObject seletDialogObject;

    public Button choiceButtonPrefabs;
    
    private List<Button> choiceObjList = new();

    public static DialogueManager instance;

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

    public void DialogueText(string npcNamme, string talkContent) // 출력되게 하기 
    {
        dialogObject.SetActive(true);

        npcName.text = npcNamme;
        dialogText.text = talkContent;
    }

    public void SeletDialogText(int sameEventNum, string[] choiceContents, int[] resumNum, EventType?[] eventTypes)// 나중에 다른 타이밍에 다른 매개변수로 실행되게 
    {
        seletDialogObject.SetActive(true);
        Debug.Log("옮길번호: " + string.Join(", ", resumNum));
        
        for (int i = 0; i < sameEventNum; i++)
        {
            int dialogueResumNum = resumNum[i];
            Button obj = Instantiate(choiceButtonPrefabs);
            obj.transform.SetParent(seletDialogObject.transform, false);// 로컬 좌표제를 유지하도록 설정 
            obj.onClick.AddListener(() => 
            {
                /*
                if (eventTypes != null)
                {
                    Events.instance.EventOccure(eventTypes[i]);
                }
                */

                BankClerkDialogManager.instance.Dialogue(dialogueResumNum);
                EndSelectDialog();
            });
            TMP_Text objText = obj.GetComponentInChildren<TMP_Text>();
            objText.text = choiceContents[i];
            choiceObjList.Add(obj);
        }
    }

    public void EndDialog()// 나중에 대화창 초기화까지 실시
    {
        dialogObject.SetActive(false);
        // 선택 대화창에 추가된 모든 오브젝트 파괴하게 하고 싶음
    }

    public void EndSelectDialog()
    {
        seletDialogObject.SetActive(false);

        foreach (Button button in choiceObjList)
        {
            Destroy(button.gameObject);
        }
        
        choiceObjList.Clear();
    }

}
