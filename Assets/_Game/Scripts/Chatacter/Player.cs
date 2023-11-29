using UnityEngine;

public class Player : Character
{
    [SerializeField] protected DynamicJoystick joystick;

    private void FixedUpdate()
    {
        if (isDead) return;

        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);

        if (Mathf.Abs(joystick.Horizontal) > 0.1f || Mathf.Abs(joystick.Vertical) > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
            isIdle = false;
            ChangeAnim(CacheString.ANIM_RUN);
            CancelInvoke(nameof(SpawnBullet));
        }
        else if(!isAttack)
        {
            isIdle = true;
            ChangeAnim(CacheString.ANIM_IDLE);
        }

    }

    public override void OnInit()
    {
        base.OnInit();
        if (weaponData == null)
        {
            weaponData = DataManager.Instance.GetWeaponData(currentWeaponType);
        }
    }

    public override void OnDeath()
    {
        base.OnDeath();
        //TO DO: Pop up UI when player die
        
    }
}