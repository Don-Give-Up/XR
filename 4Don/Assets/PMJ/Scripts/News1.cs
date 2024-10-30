using TMPro;
using UnityEngine;

public class News1 : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text summary;

    public void UseData(Article article)
    {
        title.text = article.cleaned_title;
        summary.text = article.summary_50;
    }
}
