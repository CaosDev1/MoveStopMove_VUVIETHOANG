using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Hammer = 0,
    Knife = 1,
    Uzi = 2,
    Axe = 3,
    Boomerang = 4,
}

[Serializable]
public class WeaponData
{
    public WeaponType WeaponType;
    public Weapon weapon;
    public Bullet bullet;
}
