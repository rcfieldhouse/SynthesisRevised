using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public CharacterController3D controller;
    public CharacterCombat Combat;
    public float runSpeed = 40f;
   public AnimationManager animationManager; 
    [SerializeField] private float LRMove = 0f;
    [SerializeField] private float UDMove = 0f;
    [SerializeField] private Vector3 Direction = Vector3.right;
    private bool SusMove = false;
    private bool crouch = false, jump = false,DoubleJump=false,CanDoubleJump=false,Sprint = false;
    // Update is called once per frame
    private bool[] attacks = new bool[3]; 
    void Start()
    {
        attacks[0] = false;
        attacks[1] = false;
        attacks[2] = false;
        animationManager = GetComponent<AnimationManager>();
    }

    void Update()
    {
        //LR move
        if (controller.GetIfGrounded() == true) { GetComponent<HealthSystem>().SetSuspendMove(false); }
        LRMove = Input.GetAxisRaw("Horizontal");
        UDMove = Input.GetAxisRaw("Vertical");
        Direction = new Vector3(LRMove, UDMove, transform.localScale.x);
        //for input manager later on, but for now, i have the big stupid 
        // if (Input.GetButtonDown("Jump"))
        // {
        //     jump = true;
        // }
        //  if (Input.GetKeyUp("space"))
        //  {
        //      CanDoubleJump = true;
        //  }

    
        
        //Jump
        if (Input.GetKeyDown("space"))
        {

            if (controller.GetIfGrounded() == false && CanDoubleJump == true)
            {
                DoubleJump = true;
                CanDoubleJump = false;
            }
            else if (controller.GetIfGrounded()==true)
            {
                CanDoubleJump = true;
                jump = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
            Sprint = true; 
        else 
            Sprint = false; 
        //Attack1 
        if (Input.GetKeyDown("1"))
        {
            attacks[0] = true;
            Combat.Attack(controller.transform,1,Direction);
        }
        else if (Input.GetKeyUp("1"))
           attacks[0] = false;
      
        //Attack2 
        if (Input.GetKeyDown("2"))
        {
            attacks[1] = true;
            Combat.Attack(controller.transform, 2,Direction);
        }
        else if(Input.GetKeyUp("2"))
            attacks[1] = false;

        //Attack3 
        if (Input.GetKeyDown("3"))
        {
            attacks[2] = true;
            Combat.Attack(controller.transform, 3,Direction);
        }
      //devhacks 
        if (Input.GetKeyDown("p"))
        {
            if (GetComponent<HealthSystem>().GetHealthLimit()> GetComponent<HealthSystem>().GetHealth())
            GetComponent<HealthSystem>().HealthUp(1);
        }
        // if (Input.GetButtonDown("Crouch"))
        // {
        //     crouch = true;
        // }
        // else if (Input.GetButtonUp("Crouch"))
        // {
        //     crouch = false;
        // }
        // {
        //     crouch = true;
        // }
        animationManager.SetAnimation(LRMove, UDMove, jump, Sprint, attacks);
    }
 

    public void SetJump(bool jump)
    {
        CanDoubleJump = jump;
    }
    private void FixedUpdate()
    {
        Combat.SetCanAttack(controller.GetIfGrounded());
        Combat.SetAttackBox(Direction);

        //directional attacks disable player control temporarily
        if (Combat.GetIsDirAttacking() == true)
        {
            Combat.SetIsDirAttacking(false,Direction);
            GetComponent<HealthSystem>().SetSuspendMove(true);
            controller.AttackMovement(Direction);
        }

        //if player isnt getting knocked back or in mid attack
        SusMove = GetComponent<HealthSystem>().GetSuspendMove();
            controller.Move(LRMove * Time.fixedDeltaTime * runSpeed, crouch, jump, DoubleJump,UDMove,SusMove);
          
        jump = false;
        DoubleJump = false;
    }
  // private IEnumerator(int i)
  // {
  //
  //
  //     yield return i; 
  // }
}
