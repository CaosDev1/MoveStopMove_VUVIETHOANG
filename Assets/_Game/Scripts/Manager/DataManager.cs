
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    
    public WeaponDataSO weaponDataSO;
    public HatDataSO hatDataSO;
    public PantDataSO pantDataSO;
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

    public HatData GetHatData(HatItemType hatItemData)
    {
        List<HatData> hatDatas = hatDataSO.hatDataList;
        for (int i = 0; i < hatDatas.Count; i++)
        {
            if(hatItemData == hatDatas[i].hatItemType)
            {
                return hatDatas[i];
            }
        }
        return null;
    }

    public PantData GetPantData(PantItemType pantItemData)
    {
        List<PantData> pantDatas = pantDataSO.pantDataList;
        for (int i = 0; i < pantDatas.Count; i++)
        {
            if(pantItemData == pantDatas[i].pantItemType)
            {
                return pantDatas[i];
            }
        }
        return null;
    }

    public void ChangeWeapon(WeaponType weaponType)
    {
        playerData.weaponTypeData = weaponType;
        SavePlayerData(playerData);
    }

    public void ChangeHatSkin(HatItemType hatSkinItemType)
    {
        playerData.hatTypeData = hatSkinItemType;
        SavePlayerData(playerData);
    }

    public void ChangePantSkin(PantItemType pantSkinItemType)
    {
        playerData.pantTypeData = pantSkinItemType;
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
