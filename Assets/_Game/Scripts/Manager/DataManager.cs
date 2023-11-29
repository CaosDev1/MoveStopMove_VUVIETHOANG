using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public WeaponDataSO weaponDataSO;
    public NameDataSO nameDataSO;

    public WeaponData GetWeaponData(WeaponType weaponType)
    {
        List<WeaponData> weapons = weaponDataSO.listWeaponData;
        for (int i = 0; i < weapons.Count; i++)
        {
            if(weaponType == weapons[i].weaponType)
            {
                return weapons[i];
            }
        }
        
        return null;
    }

    public NameData GetNameData(string characterName)
    {
        List<NameData> nameDatas = nameDataSO.listName;
        for (int i = 0; i < nameDatas.Count; i++)
        {
            
        }

        
        return null;
    }
}
