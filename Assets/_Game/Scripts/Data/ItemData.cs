using UnityEngine;

public enum HatItemType
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
}
public enum PantItemType
{
    PANT_1 = 0,
    PANT_2 = 1,
    PANT_3 = 2,
    PANT_4 = 3,
    PANT_5 = 4,
    PANT_6 = 5,
    PANT_7 = 6,
    PANT_8 = 7,
    PANT_9 = 8,
}
public class ItemData
{
    public HatItemType hatItemType;
    public PantItemType pantItemType;
    public Sprite itemSprite;
    public Material itemMaterial;
    public Hat itemPrefab;
}
