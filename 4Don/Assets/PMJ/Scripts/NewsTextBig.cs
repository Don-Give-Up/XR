using TMPro;
using UnityEngine;

public class NewsTextBig : MonoBehaviour // 뉴스 전문
{
    public TMP_Text title;
    public TMP_Text summary;

    public void UseData(Article article)
    {
        title.text = article.title;
        summary.text = article.summary;
    }
}
