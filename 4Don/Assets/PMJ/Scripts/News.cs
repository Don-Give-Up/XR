using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class News : MonoBehaviour // 본문
{
    /// AI에서 받은 결과값을 가져와서
    /// 넣어줘야해
    ///
    public TMP_Text title;
    //public TMP_Text summary;
    public TMP_Text summary;
    //public TMP_Text cleaned_body;
    //public TMP_Text[] field;
    public RawImage images;


    public void UseData(Article article)
    {
        
        title.text = article.title;
        summary.text = article.summary;
        //cleaned_body.text = article.cleaned_body;
        /*if (article.image == null)
        {
            return;
        }*/
       // images.texture = article.image;

    }
}
