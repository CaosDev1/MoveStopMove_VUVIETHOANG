
using UnityEngine;

public class PlayerData
{
    public WeaponType weaponTypeData;
    public HatItemType hatTypeData;
    public PantItemType pantTypeData;
    public PlayerData()
    {
        weaponTypeData = WeaponType.Hammer;
        pantTypeData = PantItemType.PANT_1;
    }
}
