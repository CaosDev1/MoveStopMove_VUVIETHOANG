using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasWeaponShop : MonoBehaviour
{
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button selectButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI weaponText;
    [SerializeField] private WeaponDataSO weaponData;
    [SerializeField] private GameObject spawnWeaponSkinPos;
    [SerializeField] private Character player;
    [SerializeField] private int index;
    private Weapon weaponSkin;
    

    private void Start()
    {
        index = 0;
        nextButton.onClick.AddListener(NextWeapon);
        prevButton.onClick.AddListener(PrevWeapon);
        selectButton.onClick.AddListener(SelectWeapon);
        closeButton.onClick.AddListener(CloseWeaponShop);
        LoadWeapon(index);
    }
    private void LoadWeapon(int index)
    {
        weaponSkin = Instantiate(weaponData.listWeaponData[index].weapon, spawnWeaponSkinPos.transform);
        weaponText.text = weaponData.listWeaponData[index].weaponName;
    }

    private void DestroyWeapon(Weapon weapon)
    {
        Destroy(weapon.gameObject);
    }

    public void NextWeapon()
    {
        if(index < weaponData.listWeaponData.Count - 1)
        {
            index++;
            DestroyWeapon(weaponSkin);
            LoadWeapon(index);
        }
        
    }

    public void PrevWeapon()
    {
        if(index > 0)
        {
            index--;
            DestroyWeapon(weaponSkin);
            LoadWeapon(index);
        }
        
    }

    private void SelectWeapon()
    {
        player.currentWeaponType = weaponData.listWeaponData[index].weaponType;
        player.WeaponData = DataManager.Instance.GetWeaponData(player.currentWeaponType);
        player.SpawnWeapon(player.WeaponData.weapon);
    }

    private void CloseWeaponShop()
    {
        UIManager.Instance.CloseWeaponShop();
    }
}
