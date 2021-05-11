using UnityEngine;
using RPGCharacterAnims.Actions;

namespace RPGCharacterAnims
{
    [HelpURL("https://docs.unity3d.com/Manual/class-InputManager.html")]

    public class lji_characterInputContoller : MonoBehaviour
    {
        RPGCharacterController rpgCharacterController;
        lji_playerStatus playerStatus;

        float speed = 30f;
        Rigidbody rigidbody;
        Vector3 movement; //물체의 xyz값 담을 변수
        Quaternion newRotation;
        
        //공격 속도 제한 함수(기본은 2초)
        public float attackDelay = 2f;
        float attackTimer = 0f;

        SwitchWeaponContext weaponContext = new SwitchWeaponContext();

        // Inputs.
        private float inputHorizontal = 0;
        private float inputVertical = 0;
        private bool inputJump;
        private bool inputLightHit;
        private bool inputDeath;
        private bool inputAttackL;
        private bool inputAttackR;
        private bool inputCastL;
        private bool inputCastR;
        private float inputSwitchUpDown;
        private float inputSwitchLeftRight;
        private float inputAimBlock;
        private bool inputAiming;
        private bool inputFace;
        private float inputFacingHorizontal;
        private float inputFacingVertical;
        private bool inputRoll;
        private bool inputShield;
        private bool inputRelax;

        //추가된 Inputs
        private bool inputDash;

        // Variables.
        private Vector3 moveInput;
        private bool isJumpHeld;
        private Vector3 currentAim;
        private float bowPull;
        private bool blockToggle;
        private float inputPauseTimeout = 0;
        private bool inputPaused = false;

        //추가된 Variables.
        private bool isDash = false;
        float dashTimer = 0f;


        private void Awake()
        {
            rpgCharacterController = GetComponent<RPGCharacterController>();
            playerStatus = GetComponent<lji_playerStatus>();
            currentAim = Vector3.zero;

            rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            // Pause input for other external input.
            if (inputPaused)
            {
                if (Time.time > inputPauseTimeout) { inputPaused = false; }
                else { return; }
            }

            Inputs();
            Blocking();

            if (isDash == false)
            {
                if (inputDash)
                    isDash = true;
                else
                    Moving();
            }
            else
            {
                Dashing();
            }

            Damage();
            SwitchWeapons();

            if (!rpgCharacterController.IsActive("Relax"))
            {
                Strafing();
                Facing();
                Aiming();
                Rolling();
                Attacking();
            }

            StatusUpdate();
        }

        /// <summary>
        /// Pause input for a number of seconds.
        /// </summary>
        /// <param name="timeout">The amount of time in seconds to ignore input</param>
        public void PauseInput(float timeout)
        {
            inputPaused = true;
            inputPauseTimeout = Time.time + timeout;
        }

        /// <summary>
        /// Input abstraction for easier asset updates using outside control schemes.
        /// </summary>
        private void Inputs()
        {
            try
            {
                //inputJump = Input.GetButtonDown("Jump");
                //isJumpHeld = Input.GetButton("Jump");
                //inputLightHit = Input.GetButtonDown("LightHit");
                //inputDeath = Input.GetButtonDown("Death");

                //inputAttackL = Input.GetMouseButtonDown(1);
                if (Input.GetMouseButtonDown(1))
                {
                    if (attackTimer == 0f)
                    {
                        attackTimer += Time.deltaTime;
                        if (rpgCharacterController.leftWeapon.Equals((int)Weapon.TwoHandStaff))
                        {
                            inputCastL = Input.GetMouseButtonDown(1);
                        }
                        else
                        {
                            inputAttackL = Input.GetMouseButtonDown(1);
                        }
                    }
                    else
                    {
                        if (attackTimer > attackDelay)
                        {
                            attackTimer = 0f;
                        }
                        else
                        {
                            attackTimer += Time.deltaTime;
                        }
                    }
                    
                }
                else
                {
                    inputCastL = Input.GetMouseButtonDown(1);
                    inputAttackL = Input.GetMouseButtonDown(1);

                    if (attackTimer != 0f)
                    {
                        if (attackTimer > attackDelay)
                        {
                            attackTimer = 0f;
                        }
                        else
                        {
                            attackTimer += Time.deltaTime;
                        }
                    }
                    
                }
                
                //inputAttackR = Input.GetMouseButtonDown(0);
                if (Input.GetMouseButtonDown(0))
                {
                    if (attackTimer == 0f)
                    {
                        attackTimer += Time.deltaTime;
                        if (rpgCharacterController.rightWeapon.Equals((int)Weapon.TwoHandStaff))
                        {
                            inputCastR = Input.GetMouseButtonDown(0);
                        }
                        else
                        {
                            inputAttackR = Input.GetMouseButtonDown(0);
                        }
                    }
                    else
                    {
                        if (attackTimer > attackDelay)
                        {
                            attackTimer = 0f;
                        }
                        else
                        {
                            attackTimer += Time.deltaTime;
                        }
                    }
                    
                }
                else
                {
                    inputCastR = Input.GetMouseButtonDown(0);
                    inputAttackR = Input.GetMouseButtonDown(0);


                    if (attackTimer != 0f)
                    {
                        if (attackTimer > attackDelay)
                        {
                            attackTimer = 0f;
                        }
                        else
                        {
                            attackTimer += Time.deltaTime;
                        }
                    }
                }

                //inputCastL = Input.GetButtonDown("CastL");
                //inputCastR = Input.GetButtonDown("CastR");
                //inputSwitchUpDown = Input.GetAxisRaw("SwitchUpDown");
                //inputSwitchLeftRight = Input.GetAxisRaw("SwitchLeftRight");
                //inputAimBlock = Input.GetAxisRaw("Aim");
                //inputAiming = Input.GetButton("Aiming");

                inputHorizontal = Input.GetAxisRaw("Horizontal");
                inputVertical = Input.GetAxisRaw("Vertical");
                inputFace = (Input.GetMouseButton(1)|| Input.GetMouseButton(0));
                //inputFacingHorizontal = Input.GetAxisRaw("FacingHorizontal");
                //inputFacingVertical = Input.GetAxisRaw("FacingVertical");
                //inputRoll = Input.GetButtonDown("L3");

                //inputShield = Input.GetButtonDown("Shield");
                if(rpgCharacterController.rightWeapon.Equals((int)Weapon.Shield))
                    inputShield = Input.GetMouseButton(0);
                if (rpgCharacterController.leftWeapon.Equals((int)Weapon.Shield))
                    inputShield = Input.GetMouseButton(1);
                //inputRelax = Input.GetButtonDown("Relax");

                //추가 input
                inputDash = Input.GetKeyDown(KeyCode.Space);

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    weaponContext.type = "Switch";
                    weaponContext.side = "Dual";
                    weaponContext.sheathLocation = "Hips";
                    weaponContext.rightWeapon = (int)Weapon.RightSword;
                    weaponContext.leftWeapon = (int)Weapon.Shield;
                    rpgCharacterController.StartAction("SwitchWeapon", weaponContext);
                }

                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    weaponContext.type = "Switch";
                    weaponContext.side = "None";
                    weaponContext.sheathLocation = "Back";
                    weaponContext.rightWeapon = (int)Weapon.TwoHandBow;
                    weaponContext.leftWeapon = -1;
                    rpgCharacterController.StartAction("SwitchWeapon", weaponContext);
                }

                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    weaponContext.type = "Switch";
                    weaponContext.side = "Both";
                    weaponContext.rightWeapon = (int)Weapon.Unarmed;
                    weaponContext.leftWeapon = (int)Weapon.Unarmed;
                    rpgCharacterController.StartAction("SwitchWeapon", weaponContext);
                }
                //// Injury toggle.
                //if (Input.GetKeyDown(KeyCode.I))
                //{
                //    if (rpgCharacterController.CanStartAction("Injure"))
                //    {
                //        rpgCharacterController.StartAction("Injure");
                //    }
                //    else if (rpgCharacterController.CanEndAction("Injure"))
                //    {
                //        rpgCharacterController.EndAction("Injure");
                //    }
                //}
                // Headlook toggle.
                //if (Input.GetKeyDown(KeyCode.L)) { rpgCharacterController.ToggleHeadlook(); }


            }
            catch (System.Exception) { Debug.LogError("Inputs not found!"); }
        }

        public bool HasMoveInput()
        {
            return moveInput != Vector3.zero;
        }

        public bool HasAimInput()
        {
            return inputAiming || inputAimBlock < -0.1f;
        }

        public bool HasBlockInput()
        {
            return inputAimBlock > 0.1;
        }

        public bool HasFacingInput()
        {
            if ((inputFacingHorizontal < -0.05 || inputFacingHorizontal > 0.05) || (inputFacingVertical < -0.05 || inputFacingVertical > 0.05) || inputFace)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Blocking()
        {
            bool blocking = HasBlockInput();
            if (blocking && rpgCharacterController.CanStartAction("Block"))
            {
                rpgCharacterController.StartAction("Block");
                blockToggle = true;
            }
            else if (!blocking && blockToggle && rpgCharacterController.CanEndAction("Block"))
            {
                rpgCharacterController.EndAction("Block");
                blockToggle = false;
            }
        }

        public void Moving()
        {
            moveInput = new Vector3(inputHorizontal, inputVertical, 0f);
            rpgCharacterController.SetMoveInput(moveInput);

            // Set the input on the jump axis every frame.
            Vector3 jumpInput = isJumpHeld ? Vector3.up : Vector3.zero;
            rpgCharacterController.SetJumpInput(jumpInput);

            // If we pressed jump button this frame, jump.
            if (inputJump && rpgCharacterController.CanStartAction("Jump")) { rpgCharacterController.StartAction("Jump"); }
            else if (inputJump && rpgCharacterController.CanStartAction("DoubleJump")) { rpgCharacterController.StartAction("DoubleJump"); }

            
        }

        public void Dashing()
        {
            if (dashTimer == 0)//대쉬 시작할 때. 이때 방향을 고정시켜줘야할 것 같다
            {
                if (inputHorizontal == 0 && inputVertical == 0)
                {
                    isDash = false;
                }
                else
                {
                    movement= new Vector3(inputHorizontal, 0f, inputVertical);
                }

                //대쉬 방향 바라보기
                //newRotation = Quaternion.LookRotation(movement);
                //rigidbody.MoveRotation(newRotation);
                dashTimer += Time.deltaTime;
            }
            else
            {
                if (dashTimer > 0.2f)//dash 시간 설정하기
                {
                    if(rpgCharacterController.isSprinting)
                        rpgCharacterController.EndAction("Sprint");
                    isDash = false;
                    dashTimer = 0f;
                }
                else //대쉬 중
                {
                    if (rpgCharacterController.CanStartAction("Sprint"))
                        rpgCharacterController.StartAction("Sprint");
                    movement = movement.normalized * Time.deltaTime*speed;//숫자는 대쉬할 때 배속되는 속도
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + movement, Time.deltaTime * speed);
                    //rigidbody.MovePosition(transform.position + movement);
                    dashTimer += Time.deltaTime;
                }
            }
        }

        public void Rolling()
        {
            if (!inputRoll) { return; }
            if (!rpgCharacterController.CanStartAction("DiveRoll")) { return; }

            rpgCharacterController.StartAction("DiveRoll", 1);
        }

        private void Aiming()
        {
            if (rpgCharacterController.hasAimedWeapon)
            {
                if (HasAimInput())
                {
                    if (rpgCharacterController.CanStartAction("Aim")) { rpgCharacterController.StartAction("Aim"); }
                }
                else
                {
                    if (rpgCharacterController.CanEndAction("Aim")) { rpgCharacterController.EndAction("Aim"); }
                }
                if (rpgCharacterController.rightWeapon == (int)Weapon.TwoHandBow)
                {

                    // If using the bow, we want to pull back slowly on the bow string while the
                    // Left Mouse button is down, and shoot when it is released.
                    if (Input.GetMouseButton(0)) { bowPull += 0.05f; }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        if (rpgCharacterController.CanStartAction("Shoot")) { rpgCharacterController.StartAction("Shoot"); }
                    }
                    else { bowPull = 0f; }
                    bowPull = Mathf.Clamp(bowPull, 0f, 1f);
                }
                else
                {
                    // If using a gun or a crossbow, we want to fire when the left mouse button is pressed.
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (rpgCharacterController.CanStartAction("Shoot")) { rpgCharacterController.StartAction("Shoot"); }
                    }
                }
                // Reload.
                if (Input.GetMouseButtonDown(2))
                {
                    if (rpgCharacterController.CanStartAction("Reload")) { rpgCharacterController.StartAction("Reload"); }
                }
                // Finally, set aim location and bow pull.
                rpgCharacterController.SetAimInput(rpgCharacterController.target.position);
                rpgCharacterController.SetBowPull(bowPull);
            }
            else
            {
                Strafing();
            }
        }

        private void Strafing()
        {
            if (rpgCharacterController.canStrafe)
            {
                if (inputAimBlock < -0.1f || inputAiming)
                {
                    if (rpgCharacterController.CanStartAction("Strafe")) { rpgCharacterController.StartAction("Strafe"); }
                }
                else
                {
                    if (rpgCharacterController.CanEndAction("Strafe")) { rpgCharacterController.EndAction("Strafe"); }
                }
            }
        }

        private void Facing()
        {
            if (rpgCharacterController.canFace)
            {
                if (HasFacingInput())
                {
                    if (inputFace)
                    {
                        // Get world position from mouse position on screen and convert to direction from character.
                        Plane playerPlane = new Plane(Vector3.up, transform.position);
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        float hitdist = 0.0f;
                        if (playerPlane.Raycast(ray, out hitdist))
                        {
                            Vector3 targetPoint = ray.GetPoint(hitdist);
                            Vector3 lookTarget = new Vector3(targetPoint.x - transform.position.x, transform.position.z - targetPoint.z, 0);
                            rpgCharacterController.SetFaceInput(lookTarget);
                        }
                    }
                    else
                    {
                        rpgCharacterController.SetFaceInput(new Vector3(inputFacingHorizontal, inputFacingVertical, 0));
                    }
                    if (rpgCharacterController.CanStartAction("Face")) { rpgCharacterController.StartAction("Face"); }
                }
                else
                {
                    if (rpgCharacterController.CanEndAction("Face")) { rpgCharacterController.EndAction("Face"); }
                }
            }
        }

        private void Attacking()
        {
            if ((inputCastL || inputCastR) && rpgCharacterController.IsActive("Cast")) { rpgCharacterController.EndAction("Cast"); }
            if (!rpgCharacterController.CanStartAction("Attack")) { return; }
            if (inputAttackL) { rpgCharacterController.StartAction("Attack", new Actions.AttackContext("Attack", "Left")); }
            else if (inputAttackR) { rpgCharacterController.StartAction("Attack", new Actions.AttackContext("Attack", "Right")); }
            else if (inputCastL) { rpgCharacterController.StartAction("Cast", new Actions.CastContext("Cast", "Left")); }
            else if (inputCastR) { rpgCharacterController.StartAction("Cast", new Actions.CastContext("Cast", "Right")); }
        }

        private void Damage()
        {
            // Hit.
            if (inputLightHit) { rpgCharacterController.StartAction("GetHit", new HitContext()); }

            // Death.
            if (inputDeath)
            {
                if (rpgCharacterController.CanStartAction("Death")) { rpgCharacterController.StartAction("Death"); }
                else if (rpgCharacterController.CanEndAction("Death")) { rpgCharacterController.EndAction("Death"); }
            }
        }

        /// <summary>
        /// Cycle weapons using directional pad input. Up and Down cycle forward and backward through
        /// the list of two handed weapons. Left cycles through the left hand weapons. Right cycles through
        /// the right hand weapons.
        /// </summary>
        private void SwitchWeapons()
        {
            // Bail out if we can't switch weapons.
            if (!rpgCharacterController.CanStartAction("SwitchWeapon")) { return; }

            // Switch to Relaxed state.
            if (inputRelax)
            {
                rpgCharacterController.StartAction("Relax");
                return;
            }

            bool doSwitch = false;
            SwitchWeaponContext context = new SwitchWeaponContext();
            int weaponNumber = 0;

            // Switch to Shield.
            if (inputShield)
            {
                doSwitch = true;
                context.side = "Left";
                context.type = "Switch";
                context.leftWeapon = 7;
                context.rightWeapon = weaponNumber;
                rpgCharacterController.StartAction("SwitchWeapon", context);
                return;
            }

            // Cycle through 2H weapons any input happens on the up-down axis.
            if (Mathf.Abs(inputSwitchUpDown) > 0.1f)
            {
                int[] twoHandedWeapons = new int[] {
                    (int) Weapon.TwoHandSword,
                    (int) Weapon.TwoHandSpear,
                    (int) Weapon.TwoHandAxe,
                    (int) Weapon.TwoHandBow,
                    (int) Weapon.TwoHandCrossbow,
                    (int) Weapon.TwoHandStaff,
                    (int) Weapon.Rifle,
                };
                // If we're not wielding 2H weapon already, just switch to the first one in the list.
                if (System.Array.IndexOf(twoHandedWeapons, rpgCharacterController.rightWeapon) == -1) { weaponNumber = twoHandedWeapons[0]; }

                // Otherwise, we should loop through them.
                else
                {
                    int index = System.Array.IndexOf(twoHandedWeapons, rpgCharacterController.rightWeapon);
                    if (inputSwitchUpDown < -0.1f) { index = (index - 1 + twoHandedWeapons.Length) % twoHandedWeapons.Length; }
                    else if (inputSwitchUpDown > 0.1f) { index = (index + 1) % twoHandedWeapons.Length; }
                    weaponNumber = twoHandedWeapons[index];
                }
                // Set up the context and flag that we actually want to perform the switch.
                doSwitch = true;
                context.type = "Switch";
                context.side = "None";
                context.leftWeapon = -1;
                context.rightWeapon = weaponNumber;
            }

            // Cycle through 1H weapons if any input happens on the left-right axis.
            if (Mathf.Abs(inputSwitchLeftRight) > 0.1f)
            {
                doSwitch = true;
                context.type = "Switch";

                // Left-handed weapons.
                if (inputSwitchLeftRight < -0.1f)
                {
                    int[] leftWeapons = new int[] {
                        (int) Weapon.Unarmed,
                        (int) Weapon.Shield,
                        (int) Weapon.LeftSword,
                        (int) Weapon.LeftMace,
                        (int) Weapon.LeftDagger,
                        (int) Weapon.LeftItem,
                        (int) Weapon.LeftPistol,
                    };
                    // If we are not wielding a left-handed weapon, switch to unarmed.
                    if (System.Array.IndexOf(leftWeapons, rpgCharacterController.leftWeapon) == -1) { weaponNumber = leftWeapons[0]; }

                    // Otherwise, cycle through the list.
                    else
                    {
                        int currentIndex = System.Array.IndexOf(leftWeapons, rpgCharacterController.leftWeapon);
                        weaponNumber = leftWeapons[(currentIndex + 1) % leftWeapons.Length];
                    }

                    context.side = "Left";
                    context.leftWeapon = weaponNumber;
                    context.rightWeapon = -1;
                }
                // Right-handed weapons.
                else if (inputSwitchLeftRight > 0.1f)
                {
                    int[] rightWeapons = new int[] {
                        (int) Weapon.Unarmed,
                        (int) Weapon.RightSword,
                        (int) Weapon.RightMace,
                        (int) Weapon.RightDagger,
                        (int) Weapon.RightItem,
                        (int) Weapon.RightPistol,
                        (int) Weapon.RightSpear,
                    };
                    // If we are not wielding a right-handed weapon, switch to unarmed.
                    if (System.Array.IndexOf(rightWeapons, rpgCharacterController.rightWeapon) == -1) { weaponNumber = rightWeapons[0]; }
                    // Otherwise, cycle through the list.
                    else
                    {
                        int currentIndex = System.Array.IndexOf(rightWeapons, rpgCharacterController.rightWeapon);
                        weaponNumber = rightWeapons[(currentIndex + 1) % rightWeapons.Length];
                    }
                    context.side = "Right";
                    context.leftWeapon = -1;
                    context.rightWeapon = weaponNumber;
                }
            }
            // If we've received input, then "doSwitch" is true, and the context is filled out,
            // so start the SwitchWeapon action.
            if (doSwitch) { rpgCharacterController.StartAction("SwitchWeapon", context); }
        }

        //player의 최종 스탯 업데이트
        public void StatusUpdate()
        {
            //무기나 장비에 따른 공격력 속도 방어력 계산식 추가
            playerStatus.totalAttackPower = playerStatus.attackPower;
            playerStatus.totalAttackSpeed = playerStatus.attackSpeed;
            playerStatus.totalDefense = playerStatus.defense;
            playerStatus.movementStat.runSpeed = playerStatus.runSpeed+3;

            //attackDelay에다가 공속 반영해서 계산하는 식 추가해야한다.
        }
    }
}