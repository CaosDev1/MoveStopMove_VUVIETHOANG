using UnityEngine;

public class Player : Character
{
    [SerializeField] protected DynamicJoystick joystick;
    private bool isWin = false;

    public override void Update()
    {
        base.Update();
        if (isDead)
        {
            return;
        }

        if (isIdle && !isAttack && mainTarget != null && !mainTarget.isDead)
        {
            Attack();
        }

    }

    private void FixedUpdate()
    {
        if (isDead) return;
        if (isWin) return;

        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);

        if (Mathf.Abs(joystick.Horizontal) > 0.1f || Mathf.Abs(joystick.Vertical) > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
            isIdle = false;
            ChangeAnim(CacheString.ANIM_RUN);
            
            if (isAttack)
            {
                ResetAttack();
                CancelInvoke(nameof(Shoot));
            }

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
        isWin = false;

        if (WeaponData == null)
        {
            //Take Data from Player Data
            CurrentWeaponType = DataManager.Instance.LoadPlayerData().weaponTypeData;
            WeaponData = DataManager.Instance.GetWeaponData(CurrentWeaponType);
        }

        if (playerHatData == null)
        {
            PlayerHatType = DataManager.Instance.LoadPlayerData().hatTypeData;
            PlayerHatData = DataManager.Instance.GetHatData(PlayerHatType);
        }

        if (PlayerPantData == null)
        {
            PlayerPantType = DataManager.Instance.LoadPlayerData().pantTypeData;
            PlayerPantData = DataManager.Instance.GetPantData(PlayerPantType);
        }
    }

    public override void OnDeath()
    {
        base.OnDeath();
        //TO DO: Pop up UI when player die
        UIManager.Instance.OpenFinishUI();
        GameManager.Instance.ChangeStage(GameState.Finish);
    }

    public void OnWin()
    {
        isWin= true;
        
        UIManager.Instance.OpenWinUI();
        ChangeAnim(CacheString.ANIM_WIN);
    }
}
