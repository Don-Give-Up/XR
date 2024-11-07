using System;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour // csv 파일을 파싱한 결과를 저장하고 어떤 대화를 반환할 지 결정
{
    public static DataBaseManager instance;

    [SerializeField] private string csv_DialogueFileName;
    [SerializeField] private string csv_SeletionDialogueFileName;

    private Dictionary<int, Dialogue> dialogDic = new Dictionary<int, Dialogue>(); 
    private Dictionary<int, SeletDialogue> seletDialogDic = new Dictionary<int, SeletDialogue>();

    private DialogParser theParser;

    public static bool isDFinish = false;
    public static bool isSFinish = false;

    private void Awake()
    {
        if (instance == null)
        {
            theParser = GetComponent<DialogParser>();
            instance = this; 
            DialogDataSave();
            SeletDialogDataSave();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void DialogDataSave()
    {
        dialogDic = theParser.DialoguParse(csv_DialogueFileName);
        isDFinish = true;
    }

    private void SeletDialogDataSave()
    {
        seletDialogDic = theParser.SeletDialoguesParser(csv_SeletionDialogueFileName);
        isSFinish = true;
    }
  
    public Dictionary<int, Dialogue> GetDialogues(int _StartNum, int _EndNum)
    {
        Dictionary<int, Dialogue> targetDialogueDic = new Dictionary<int, Dialogue>();

        for (int i = _StartNum; i <= _EndNum; i++)
        {
            if (dialogDic.TryGetValue(i, out Dialogue dialogue))
            {
                targetDialogueDic.Add(i, dialogue);
            }
            else
            {
                Debug.LogWarning($"Key {i} not found in dialogDic.");
            }
        }
        return targetDialogueDic;
    }

    public Dictionary<int, SeletDialogue> GetSeletDialogues(int _StartNum, int _EndNum)
    {
        Dictionary<int, SeletDialogue> targetSeletDialogueDic = new Dictionary<int, SeletDialogue>();

        for (int i = _StartNum; i <= _EndNum; i++)
        {
            if (seletDialogDic.TryGetValue(i, out SeletDialogue seletDialogue))
            {
                targetSeletDialogueDic.Add(i, seletDialogue);
            }
            else
            {
                Debug.LogWarning($"Key {i} not found in seletDialogDic.");
            }
        }
        return targetSeletDialogueDic;
    }
}
/*
using System;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour // csv 파일을 파싱한 결과를 저장하고 어떤 대화를 반환할 지 결정
{
  public static DataBaseManager instance;

  [SerializeField] private string csv_DialogueFileName;
  [SerializeField] private string csv_SeletionDialogueFileName;

  private Dictionary<int, Dialogue> dialogDic = new Dictionary<int, Dialogue>(); 
  private Dictionary<int, SeletDialogue> seletDialogDic = new Dictionary<int, SeletDialogue>();

  private DialogParser theParser;

  public static bool isDFinish = false;
  public static bool isSFinish = false;

  
  private void Awake()
  {
    if (instance == null)
    {
      theParser = GetComponent<DialogParser>();
      instance = this; 
      DialogDataSave();
      SeletDialogDataSave();
      // 다이알로그를 변환해서 내용 변경 후 저장해 놓기
    }
    else
    {
      Destroy(gameObject);
    }
  }

  private void DialogDataSave()
  {
    dialogDic = theParser.DialoguParse(csv_DialogueFileName);
    
    isDFinish = true;
  }

  private void SeletDialogDataSave()
  {
    seletDialogDic = theParser.SeletDialoguesParser(csv_SeletionDialogueFileName);

    //
    for (int i = 0; i < seletDialogues.Length; i++)
    {
      seletDialogDic.Add(i+1, seletDialogues[i]); 
    }
    //
    
    isSFinish = true;
  }
  
  public Dictionary<int, Dialogue> GetDialogues(int _StartNum, int _EndNum)
  {
    Dictionary<int, Dialogue> targetDialogueDic = new Dictionary<int, Dialogue>();
    //List<Dialogue> dialoguesList = new List<Dialogue>();

    for (int i = 0; i <= _EndNum - _StartNum; i++)
    {
      targetDialogueDic.Add(_StartNum+i, dialogDic[_StartNum+i]);
      //dialoguesList.Add(dialogDic[_StartNum + i]); // 딕셔너리에서 값을 찾을 수가 없어서 
    }
    return targetDialogueDic;
  }

  public Dictionary<int, SeletDialogue> GetSeletDialogues(int _StartNum, int _EndNum)
  {
    
    Dictionary<int, SeletDialogue> targetSeletDialogueDic = new Dictionary<int, SeletDialogue>();
    //List<Dialogue> dialoguesList = new List<Dialogue>();

    for (int i = 0; i <= _EndNum - _StartNum; i++)
    {
      targetSeletDialogueDic.Add(_StartNum+i, seletDialogDic[_StartNum+i]);
      //dialoguesList.Add(dialogDic[_StartNum + i]); // 딕셔너리에서 값을 찾을 수가 없어서 
    }
    return targetSeletDialogueDic;
    
    //
    List<SeletDialogue> seletDialoguesList = new List<SeletDialogue>();

    for (int i = 0; i <= _EndNum - _StartNum; i++)
    {
      Debug.Log($"{i}");
      seletDialoguesList.Add(seletDialogDic[_StartNum+i]);
    }
    return seletDialoguesList.ToArray();
    //
  }
  
  
}
*/