using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogParser : MonoBehaviour
{
    private void Start()
    {
        //AllDialoguParse("AllDialogue");
    }
    
    private Dictionary<int, AllDialogue> allDialoguesDic = new Dictionary<int, AllDialogue>();

    public Dictionary<int, AllDialogue> AllDialoguParse(string _CSVAllDialogFileName)
    {
        TextAsset csvAllDialogData = Resources.Load<TextAsset>(_CSVAllDialogFileName);
        if (csvAllDialogData == null)
        {
            return null;
        }

        string[] data = csvAllDialogData.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });

            if (row.Length < 9 || string.IsNullOrWhiteSpace(row[0]))
                continue;

            //string outterkey = row[0]; // questName
            int innerkey = Int32.Parse(row[1]); // Line number

            AllDialogue alldialogue = new AllDialogue
            {
                questName = row[0],
                line = Int32.Parse(row[1]),
                selectLine = Int32.Parse(row[2]),
                name = row[3],
                content = row[4],
                choiceEventNum = Int32.Parse(row[5]),
                skipLine = Int32.Parse(row[6]),
                questState = Int32.Parse(row[7]),
            };

            allDialoguesDic.TryAdd(innerkey, alldialogue);
        }
        return allDialoguesDic;
    }
}

