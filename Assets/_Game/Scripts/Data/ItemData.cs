using UnityEngine;

public enum SkinItemType
{
    //Hat Type
    ARROW = 0,
    CAP = 1,
    COWBOY = 2,
    CROWN = 3,
    EAR = 4,
    HEADPHONE = 5,
    HORN = 6,
    POLICECAP = 7,
    STRAWHAT = 8,

    //Pant Type
    PANT_1 = 9,
    PANT_2 = 10,
    PANT_3 = 11,
    PANT_4 = 12,
    PANT_5 = 13,
    PANT_6 = 14,
    PANT_7 = 15,
    PANT_8 = 16,
    PANT_9 = 17,
}
public class ItemData
{
    public SkinItemType skinItemType;
    public Sprite itemSprite;
    public Material itemMaterial;
    public Hat itemPrefab;
}
