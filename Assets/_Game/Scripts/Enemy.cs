using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    
    private void FixedUpdate()
    {
        isIdle = true;
        anim.SetBool("IsIdle", true);
    }

    
}
