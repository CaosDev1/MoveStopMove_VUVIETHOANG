
using UnityEngine;

public class PlayerData
{
    public WeaponType weaponTypeData;
    public HatItemType hatTypeData;
    public PantItemType pantTypeData;
    public Level currentLevel;
    public PlayerData()
    {
        weaponTypeData = WeaponType.Hammer;
        pantTypeData = PantItemType.PANT_1;
        //TO DO: Them thang none cho hattype va pant type
        currentLevel = LevelManager.Instance.currentLevel;
    }
}
