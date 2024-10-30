using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class News : MonoBehaviour
{
    /// AI에서 받은 결과값을 가져와서
    /// 넣어줘야해
    ///
    public TMP_Text title;
    //public TMP_Text summary;

    public TMP_Text cleaned_body;
    //public TMP_Text[] field;
    public RawImage images;


    public void UseData(Article article)
    {
        title.text = article.cleaned_title;
        //summary.text = article.summary_50;

        cleaned_body.text = article.cleaned_body;
        images.texture = article.image;

    }
}
