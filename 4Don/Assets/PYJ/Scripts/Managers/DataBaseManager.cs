using System;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour // csv 파일을 파싱한 결과를 저장하고 어떤 대화를 반환할 지 결정
{
    public static DataBaseManager instance;

    [SerializeField] private string csv_AllDialogueFileName;
    
    private Dictionary<int, AllDialogue> allDialogueDic = new Dictionary<int, AllDialogue>();

    private DialogParser theParser;

    public static bool isFinish = false;

    private void Awake()
    {
        if (instance == null)
        {
            theParser = GetComponent<DialogParser>();
            instance = this;
            //DialogDataSave();
            //SeletDialogDataSave();
            DontDestroyOnLoad(gameObject);
            AllDialogueDataSave();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void AllDialogueDataSave()
    {
        allDialogueDic = theParser.AllDialoguParse(csv_AllDialogueFileName);
        isFinish = true;
    }

    
    public Dictionary<int, AllDialogue> GetAllDialogues(Vector2 dialogueNum)
    {
        Dictionary<int, AllDialogue> targetDialoguesDic = new Dictionary<int, AllDialogue>();

        int startNum = (int)dialogueNum.x;
        int endNum = (int)dialogueNum.y;
        
        for (int i = startNum; i <= endNum; i++)
        {
            if (allDialogueDic.TryGetValue(i, out AllDialogue allDialogue)) 
            {
                targetDialoguesDic.Add(i, allDialogue);
            }
            else
            {
                Debug.Log("찾는 다이알로그 없음");
            }
        }

        return targetDialoguesDic;
    }
}
