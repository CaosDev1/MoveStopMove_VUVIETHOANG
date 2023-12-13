
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public WeaponDataSO weaponDataSO;
    public NameDataSO nameDataSO;
    private PlayerData playerData;

    public void Init()
    {
        //playerData.weaponTypeData = WeaponType.Hammer;
        if(!PlayerPrefs.HasKey(CacheString.PLAYER_DATA_KEY))
        {
            CreatePlayerData();
        }
        playerData = LoadPlayerData();
    }

    public void CreatePlayerData()
    {
        playerData = new PlayerData();
        SavePlayerData(this.playerData);
    }

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

    public void ChangeWeapon(WeaponType weaponType)
    {
        playerData.weaponTypeData = weaponType;
        SavePlayerData(playerData);
    }

    public void SavePlayerData(PlayerData playerData)
    {
        string dataString = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(CacheString.PLAYER_DATA_KEY,dataString);
        
    }

    public PlayerData LoadPlayerData()
    {
        string dataString = PlayerPrefs.GetString(CacheString.PLAYER_DATA_KEY);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(dataString);
        return playerData;
    }

    //public NameData GetNameData(string characterName)
    //{
    //    List<NameData> nameDatas = nameDataSO.listName;
    //    for (int i = 0; i < nameDatas.Count; i++)
    //    {

    //    }
    //    return null;
    //}
}
