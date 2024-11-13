
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class NewsHintsmall : MonoBehaviour // 뉴스 힌트에서 써야합니다. 
{
    /// AI에서 받은 결과값을 가져와서
    /// 넣어줘야해
    ///
    public TMP_Text title_1;
    //public TMP_Text summary;
    public TMP_Text summary_100;
    //public TMP_Text cleaned_body;
    //public TMP_Text[] field;
    //public RawImage images;


    public void UseData(CompanyInfo companyInfo)
    {

        title_1.text = companyInfo.hint_title_1;
        summary_100.text = companyInfo.summary_100;
    }
    
}
//cleaned_body.text = article.cleaned_body;
        /*if (article.image == null)
        {
            return;
        }#1#
       // images.texture = article.image;

    }
}*/

