using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasWeaponShop : MonoBehaviour
{
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button selectButton;
    [SerializeField] private TextMeshProUGUI weaponText;
    [SerializeField] private WeaponDataSO weaponData;
    [SerializeField] protected GameObject spawnWeaponSkinPos;
    [SerializeField] private int index;
    private Weapon weaponSkin;

    private void OnEnable()
    {

        nextButton.onClick.AddListener(NextWeapon);

        prevButton.onClick.AddListener(PrevWeapon);
        
    }

    private void Start()
    {
        //LoadWeapon(index);
    }
    private void LoadWeapon(int index)
    {
        weaponSkin = Instantiate(weaponData.listWeaponData[index].weapon, spawnWeaponSkinPos.transform);
    }

    private void DestroyWeapon(Weapon weapon)
    {
        Destroy(weapon.gameObject);
    }

    public void NextWeapon()
    {
        index++;
        DestroyWeapon(weaponSkin);
        LoadWeapon(index);
    }

    public void PrevWeapon()
    {
        
        index--;
        DestroyWeapon(weaponSkin);
        LoadWeapon(index);
    }
}
