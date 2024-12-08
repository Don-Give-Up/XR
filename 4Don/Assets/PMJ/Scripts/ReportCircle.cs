using UnityEngine;
using UnityEngine.UI;

public class ReportCircle : MonoBehaviour
{
    public GameObject gameObj;
    public Transform parents;

    private Image[] assetImages = new Image[4];  // 5개의 자산 항목
    private float[] assetValues; // 자산 값들
    private Color[] assetColors = new Color[]
    {
        Color.red,    // stocks
        Color.blue,   // cash
        Color.green,  // savings
        Color.yellow, // products
        //Color.cyan    // total
    };
    
    private float total = 0f;
    private float[] reviseValue;
    private float[] fillAmounts = new float[4];
    private Assets assets;

    private void Start()
    {
        assets = ReportDataManager.instance.currentAssets;
        
        // 자산 값 배열 초기화
        assetValues = new float[] 
        { 
           // assets.total,
            assets.cash = 0,
            assets.savings,
            assets.products,
            assets.stocks 
        };

        reviseValue = new float[assetValues.Length];

        // 이미지 객체 생성
        for (int i = 0; i < assetValues.Length; i++)
        {
            GameObject newObj = Instantiate(gameObj);
            newObj.transform.SetParent(parents, false);
            assetImages[i] = newObj.GetComponent<Image>();
        }

        // 총합 및 비율 계산
        total = assets.total;
        for (int i = 0; i < assetValues.Length; i++)
        {
            reviseValue[i] = assetValues[i] / total;
        }
    }

    void Update()
    {
        if (ReportDataManager.instance.currentAssets != assets)
        {
            assets = ReportDataManager.instance.currentAssets;
        }
        
        float accumulator = 1f;
        for (int i = 0; i < assetValues.Length; i++)
        {
            fillAmounts[i] = Mathf.Lerp(fillAmounts[i], accumulator, 2f * Time.deltaTime);
            assetImages[i].color = assetColors[i];
            assetImages[i].fillAmount = fillAmounts[i];
            accumulator -= reviseValue[i];
        }
    }
}