using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private void FixedUpdate()
    {
        if(isDead) return;
        
        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);

        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
            isIdle = false;
            Anim.SetBool(ConstString.IS_IDLE_STRING, isIdle);
            Anim.SetBool(ConstString.IS_ATTACK_STRING, false);
        }
        else
        {
            isIdle = true;
            Anim.SetBool(ConstString.IS_IDLE_STRING, isIdle);
        }
        
    }
}
