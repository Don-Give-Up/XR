using TMPro;
using UnityEngine;

public class News1 : MonoBehaviour // 뉴스 전문
{
    public TMP_Text title;
    public TMP_Text summary;

    public void UseData(NewsBig newsBig)
    {
        title.text = newsBig.title;
        summary.text = newsBig.summary;
    }
}
