using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class WeaponDataSO : ScriptableObject
{
    public static WeaponDataSO instance;
    public List<WeaponData> weaponData;

}
