using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskDescriptor : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private Color normalColor;
    [SerializeField]
    private Color taskCompletionColor;
    [SerializeField]
    private Color taskSuccessCountColor;
    [SerializeField]
    private Color strikeThroughColor;

    public void UpdateText(string text)
    {
        this.text.fontStyle = FontStyles.Normal;
        this.text.text = text;
    }

    public void UpdateText(Task task)
    {
        text.fontStyle = FontStyles.Normal;

        if (task.IsComplete)
        {
            var colorCode = ColorUtility.ToHtmlStringRGB(taskCompletionColor);
            text.text = BuildText(task, colorCode, colorCode);
        }
        else
            text.text = BuildText(task, ColorUtility.ToHtmlStringRGB(normalColor), ColorUtility.ToHtmlStringRGB(taskSuccessCountColor));
    }

    public void UpdateTextUsingStrikeThrough(Task task)
    {
        var colorCode = ColorUtility.ToHtmlStringRGB(strikeThroughColor);
        text.fontStyle = FontStyles.Strikethrough;
        text.text = BuildText(task, colorCode, colorCode);
    }

    private string BuildText(Task task, string textColorCode, string successCountColorCode)
    {
        return $"<color=#{textColorCode}>● {task.Description} <color=#{successCountColorCode}>{task.CurrentSuccess}</color>/{task.NeedSuccessToComplete}</color>"; 
        // 문장 전체를 감싸는 컬러 코드 하나 와  성공 횟수를 감싸는 컬러 마크 하나
    }
}