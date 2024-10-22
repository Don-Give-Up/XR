using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{

    public ItemInfo info;
    public Image iconImage;

    public void SetInfo(ItemInfo info)
    {
        this.info = info;
        iconImage.sprite = info.icon;
    }

}
