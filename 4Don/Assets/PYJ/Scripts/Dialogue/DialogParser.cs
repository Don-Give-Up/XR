using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogParser : MonoBehaviour
{
    private void Start()
    {
        DialoguParse("NPCDialgue");
        SeletDialoguesParser("SeletionDialgue");
    }

    Dictionary<int, Dialogue> dialoguesDic = new Dictionary<int, Dialogue>();
    Dictionary<int, SeletDialogue> seletDialoguesDic = new Dictionary<int, SeletDialogue>();

    public Dictionary<int, Dialogue> DialoguParse(string _CSVDialogFileName)
    {
        TextAsset csvDialogData = Resources.Load<TextAsset>(_CSVDialogFileName);
        if (csvDialogData == null)
        {
            //Debug.LogError($"Failed to load CSV file: {_CSVDialogFileName}");
            return null;
        }

        string[] data = csvDialogData.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });

            if (row.Length < 5 || string.IsNullOrWhiteSpace(row[0]))
                continue;

            int key = Int32.Parse(row[0]);
            Debug.Log($"key: {key}");
            
            Dialogue dialogue = new Dialogue
            {
                npcname = row[1],
                content = row[2],
                eventNum = Int32.Parse(row[3]),
                skipNum = Int32.Parse(row[4])
            };

            // 중복 체크
            if (!dialoguesDic.TryAdd(key, dialogue))
            {
                //Debug.LogWarning($"Duplicate key {key} found in DialoguParse. Updating entry.");
                dialoguesDic[key] = dialogue; // 기존 값을 덮어씀
            }
        }

        return dialoguesDic;
    }

    public Dictionary<int, SeletDialogue> SeletDialoguesParser(string _CSVSeletDialogFileName)
    {
        TextAsset csvSeletDialogData = Resources.Load<TextAsset>(_CSVSeletDialogFileName);
        if (csvSeletDialogData == null)
        {
            //Debug.LogError($"Failed to load CSV file: {_CSVSeletDialogFileName}");
            return null;
        }

        string[] data = csvSeletDialogData.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });

            if (row.Length < 5 || string.IsNullOrWhiteSpace(row[0]))
                continue;

            int key = Int32.Parse(row[0]);
            SeletDialogue seletDialogue = new SeletDialogue
            {
                eventNum = Int32.Parse(row[1]),
                choiceContent = row[2],
                resumeNum = Int32.Parse(row[3]),
                eventType = ParseEventType(row[4]) // eventType = ParseEventType(row[4]) ?? EventType.defaultValue; // ?? 왼쪽 값이 널이면 오른쪽 값이다. 
            };

            // 중복 체크
            if (!seletDialoguesDic.TryAdd(key, seletDialogue))
            {
                //Debug.LogWarning($"Duplicate key {key} found in SeletDialoguesParser. Updating entry.");
                seletDialoguesDic[key] = seletDialogue; // 기존 값을 덮어씀
            }
        }

        return seletDialoguesDic;
    }

    private EventType? ParseEventType(string eventType)
    {
        if (string.IsNullOrEmpty(eventType) || eventType == "-1")
        {
            return null;
        }

        return (EventType)Enum.Parse(typeof(EventType), eventType);
    }
}

/*
using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogParser : MonoBehaviour
{
    private void Start()
    {
        DialoguParse("NPCDialgue");
        SeletDialoguesParser("SeletionDialgue");
    }

    Dictionary<int, Dialogue> dialoguesDic = new Dictionary<int, Dialogue>(); //대화를 순서대로 추가할 딕셔너리 완성
    Dictionary<int, SeletDialogue> seletDialoguesDic = new Dictionary<int, SeletDialogue>();
    public Dictionary<int, Dialogue> DialoguParse(string _CSVDialogFileName)
    {
        TextAsset csvDialogData = Resources.Load<TextAsset>(_CSVDialogFileName);

        string[] data = csvDialogData.text.Split(new char[] { '\n' }); // e

        for (int i = 1; i < data.Length; i++) // 첫 번째 줄은 헤더로 가정
        {
            string[] row = data[i].Split(new char[] { ',' }); // ROW 배열을 생성하기 위해 현재 줄을 쉼표로 나눔
            
            if (row.Length < 5 || string.IsNullOrWhiteSpace(row[0])) // 데이터가 부족하거나 첫 번째 값이 비어 있는지 확인
                continue;

            int key = Int32.Parse(row[0]);
            Debug.Log($"key: {key}");
            
            Dialogue dialogue = new Dialogue();
            dialogue.npcname = row[1];
            dialogue.content = row[2];
            dialogue.eventNum = Int32.Parse(row[3]);
            dialogue.skipNum = Int32.Parse(row[4]);

            dialoguesDic.Add(key, dialogue);
        }
        return dialoguesDic; // 배열로 반환하여 저장 // 배열이 아니라 딕셔너리로 반환 
    }

    public Dictionary<int, SeletDialogue> SeletDialoguesParser(string _CSVSeletDialogFileName)
    {
        TextAsset csvSeletDialogData = Resources.Load<TextAsset>(_CSVSeletDialogFileName);
        
        string[] data = csvSeletDialogData.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length; i++) // 첫 번째 줄은 헤더로 가정
        {
            string[] row = data[i].Split(new char[] { ',' });

            if (row.Length < 4 || string.IsNullOrWhiteSpace(row[0])) // 데이터가 부족하거나 첫 번째 값이 비어 있는지 확인
                continue;

            int key = Int32.Parse(row[0]);

            SeletDialogue seletDialogue = new SeletDialogue();

            seletDialogue.eventNum = Int32.Parse(row[1]);
            seletDialogue.choiceContent = row[2];
            seletDialogue.resumeNum = Int32.Parse(row[3]);

            seletDialoguesDic.Add(key, seletDialogue);
            
        }

        return seletDialoguesDic;
    }
}

*/
