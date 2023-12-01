using UnityEngine;

public class Player : Character
{
    [SerializeField] protected DynamicJoystick joystick;

    private void Update()
    {
        if (GameManager.Instance.IsStage(GameState.GamePlay))
        {
            if (isDead)
            {
                return;
            }

            FindClosestTarget(transform.position, listTarget);

            if (isIdle && !isAttack && mainTarget != null)
            {
                Attack();
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);

        if (Mathf.Abs(joystick.Horizontal) > 0.1f || Mathf.Abs(joystick.Vertical) > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
            isIdle = false;
            if (isAttack)
            {
                ResetAttack();
                CancelInvoke(nameof(Shoot));
            }
            ChangeAnim(CacheString.ANIM_RUN);

        }
        else if (!isAttack)
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
