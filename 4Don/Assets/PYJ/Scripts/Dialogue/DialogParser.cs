using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogParser : MonoBehaviour
{
    private void Start()
    {
        AllDialoguParse("AllDialogue");
    }
    
    private Dictionary<int, AllDialogue> allDialoguesDic = new Dictionary<int, AllDialogue>();

    public Dictionary<int, AllDialogue> AllDialoguParse(string _CSVAllDialogFileName)
    {
        TextAsset csvAllDialogData = Resources.Load<TextAsset>(_CSVAllDialogFileName);
        if (csvAllDialogData == null)
        {
            Debug.LogError("CSV 파일을 찾을 수 없습니다.");
            return null;
        }

        string[] data = csvAllDialogData.text.Split(new char[] { '\n' }); // 줄바꿈으로 분할

        Dictionary<int, AllDialogue> allDialoguesDic = new Dictionary<int, AllDialogue>(); // 반환할 딕셔너리 초기화

        for (int i = 1; i < data.Length; i++) // 첫 번째 줄은 헤더로 간주하고 건너뜀
        {
            string[] row = data[i].Split(new char[] { ',' }); // 쉼표로 구분

            if (row.Length < 8 || string.IsNullOrWhiteSpace(row[0])) // 필요한 데이터가 없으면 건너뜀
                continue;

            // 각 열에 대한 값 처리
            try
            {
                int innerkey = Int32.Parse(row[1].Trim()); // Line number (정수 변환)
                AllDialogue alldialogue = new AllDialogue
                {
                    questName = row[0].Trim(), // questName (공백 제거 후 사용)
                    line = Int32.Parse(row[1].Trim()), // line (정수 변환)
                    selectLine = Int32.Parse(row[2].Trim()), // selectLine (정수 변환)
                    name = row[3].Trim(), // name (공백 제거 후 사용)
                    content = row[4].Trim(), // content (공백 제거 후 사용)
                    choiceEventNum = Int32.Parse(row[5].Trim()), // choiceEventNum (정수 변환)
                    skipLine = Int32.Parse(row[6].Trim()), // skipLine (정수 변환)
                    questState = Int32.Parse(row[7].Trim()), // questState (정수 변환)
                };

                allDialoguesDic.TryAdd(innerkey, alldialogue); // 키와 값 추가
            }
            catch (Exception ex)
            {
                Debug.LogError($"CSV 파싱 중 오류 발생: {ex.Message} (줄 {i + 1})");
            }
        }

        return allDialoguesDic;
    }
}

