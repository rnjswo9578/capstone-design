using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGCharacterAnims.Actions;

namespace RPGCharacterAnims
{
    [HelpURL("https://docs.unity3d.com/Manual/class-InputManager.html")]

    public class lji_characterInputContoller : MonoBehaviour
    {
        RPGCharacterController rpgCharacterController;
        public lji_playerStatus playerStatus;

        float speed = 30f;
        Rigidbody rigidbody;
        Vector3 movement; //물체의 xyz값 담을 변수
        Quaternion newRotation;
        
        //공격 속도 제한 함수(기본은 2초)
        public float attackDelay = 2f;
        float attackTimer = 0f;
        private bool isAttack = false;

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
        float dashTimer = 0.3f;
        private float DashingHorizontal;
        private float DashingVertical;

        public int side = 1;
        public int nowWeaponSet = 0;

        private void Awake()
        {
            rpgCharacterController = GetComponent<RPGCharacterController>();
            playerStatus = GetComponent<lji_playerStatus>();
            currentAim = Vector3.zero;

            rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            WeaponSwitch(playerStatus.nowWeaponSet+1);
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

            //if (isDash == false)
            //{
            //    if (inputDash)
            //        isDash = true;
            //    else
            //        Moving();
            //}
            //else
            //{
            //    Dashing();
            //}
            if (inputDash)
            {
                DashingHorizontal = inputHorizontal;
                DashingVertical = inputVertical;
                StartCoroutine(DashCounter());
            }
            else
            {
                if (isDash)
                {
                    Dashing();
                }
                else
                    Moving();
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

            if (rpgCharacterController.GetHandler("Attack").IsActive())
            {
                Debug.Log("isAttacking");
            }

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
                    if (isAttack == false)
                    {
                        StartCoroutine(IsAttack());

                        if (rpgCharacterController.leftWeapon.Equals((int)Weapon.TwoHandStaff))
                        {
                            inputCastL = Input.GetMouseButtonDown(1);
                        }
                        else
                        {
                            inputAttackL = Input.GetMouseButtonDown(1);
                        }
                        playerStatus.side = 1;
                    }
                }
                else
                {
                    inputCastL = Input.GetMouseButtonDown(1);
                    inputAttackL = Input.GetMouseButtonDown(1);
                    
                    
                }
                
                //inputAttackR = Input.GetMouseButtonDown(0);
                if (Input.GetMouseButtonDown(0))
                {
                    
                    if (isAttack == false)
                    {
                        StartCoroutine(IsAttack());

                        attackTimer += Time.deltaTime;
                        if (rpgCharacterController.rightWeapon.Equals((int)Weapon.TwoHandStaff))
                        {
                            inputCastR = Input.GetMouseButtonDown(0);
                        }
                        else
                        {
                            inputAttackR = Input.GetMouseButtonDown(0);
                        }
                        playerStatus.side = 2;
                    }
                }
                else
                {
                    inputCastR = Input.GetMouseButtonDown(0);
                    inputAttackR = Input.GetMouseButtonDown(0);

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
                if (isAttack)
                    inputFace = true;
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
                    playerStatus.nowWeaponSet = 0;
                    WeaponSwitch(1);
                }

                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    playerStatus.nowWeaponSet = 1;
                    WeaponSwitch(2);
                }

                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    playerStatus.nowWeaponSet = 2;
                    WeaponSwitch(3);
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

            moveInput = new Vector3(DashingHorizontal,DashingVertical, 0f);
            rpgCharacterController.SetMoveInput(moveInput);
            
        }
        
        IEnumerator DashCounter()
        {

            if (rpgCharacterController.CanStartAction("Sprint"))
                rpgCharacterController.StartAction("Sprint");
            isDash = true;

            yield return new WaitForSeconds(dashTimer);

            if (rpgCharacterController.isSprinting)
                rpgCharacterController.EndAction("Sprint");
            isDash = false;
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

        IEnumerator IsAttack()
        {
            isAttack= true;
            yield return new WaitForSeconds(attackDelay);
            isAttack = false;
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
        //무기 바꾸기
        public void WeaponSwitch(int set)
        {
            weaponContext = new SwitchWeaponContext();
            int rightWeapon = 0;
            int leftWeapon = 0;

            if (set == 3)
            {
                rightWeapon = (int) Weapon.Unarmed;
                leftWeapon = (int)Weapon.Unarmed;
            }
            else
            {
                rightWeapon = playerStatus.rightWeapon[playerStatus.nowWeaponSet];
                leftWeapon = playerStatus.leftWeapon[playerStatus.nowWeaponSet];
            }

            //만약 양손 무기라면?
            if (rightWeapon == (int)Weapon.TwoHandSword ||
               rightWeapon == (int)Weapon.TwoHandSpear ||
               rightWeapon == (int)Weapon.TwoHandAxe ||
               rightWeapon == (int)Weapon.TwoHandBow ||
               rightWeapon == (int)Weapon.TwoHandCrossbow ||
               rightWeapon == (int)Weapon.TwoHandStaff ||
               rightWeapon == (int)Weapon.Rifle)
            {
                weaponContext.type = "Switch";
                weaponContext.side = "None";
                weaponContext.rightWeapon = rightWeapon;
                weaponContext.leftWeapon = -1;
                rpgCharacterController.StartAction("SwitchWeapon", weaponContext);
            }
            else if(rightWeapon == (int)Weapon.Unarmed|| leftWeapon == (int)Weapon.Unarmed)
            {
                if(rightWeapon == leftWeapon)
                {
                    weaponContext.type = "Switch";
                    weaponContext.side = "Dual";
                    weaponContext.rightWeapon = rightWeapon;
                    weaponContext.leftWeapon = leftWeapon;
                    rpgCharacterController.StartAction("SwitchWeapon", weaponContext);
                }
                else if(rightWeapon == (int)Weapon.Unarmed)
                {
                    weaponContext.type = "Switch";
                    weaponContext.side = "Left";
                    weaponContext.rightWeapon = rightWeapon;
                    weaponContext.leftWeapon = leftWeapon;
                    rpgCharacterController.StartAction("SwitchWeapon", weaponContext);
                }
                else
                {
                    weaponContext.type = "Switch";
                    weaponContext.side = "Right";
                    weaponContext.rightWeapon = rightWeapon;
                    weaponContext.leftWeapon = leftWeapon;
                    rpgCharacterController.StartAction("SwitchWeapon", weaponContext);
                }
            }
            else if (rightWeapon == leftWeapon)//양손이 같은 무기?
            {
                weaponContext.type = "Switch";
                weaponContext.side = "Dual";
                weaponContext.rightWeapon = rightWeapon;
                weaponContext.leftWeapon = leftWeapon;
                rpgCharacterController.StartAction("SwitchWeapon", weaponContext);
            }
            else//양손이 다른 무기
            {
                weaponContext.type = "Switch";
                weaponContext.side = "Both";
                weaponContext.rightWeapon = rightWeapon;
                weaponContext.leftWeapon = leftWeapon;
                rpgCharacterController.StartAction("SwitchWeapon", weaponContext);
            }
        }
        

        //player의 최종 스탯 업데이트
        public void StatusUpdate()
        {
            //무기나 장비에 따른 공격력 속도 방어력 계산식 추가
            playerStatus.totalAttackPower = playerStatus.attackPower+playerStatus.addAttackPower;
            AttackSpeedSet();
            playerStatus.totalAttackSpeed = playerStatus.attackSpeed + playerStatus.addAttackSpeed+playerStatus.rightWeaponSpeed[playerStatus.nowWeaponSet];
            //실드 장착했으면 방어력 +5
            if(playerStatus.leftWeapon[playerStatus.nowWeaponSet]==(int)Weapon.Shield)
                playerStatus.totalDefense = playerStatus.defense + playerStatus.addDefense+5;
            else
                playerStatus.totalDefense = playerStatus.defense+playerStatus.addDefense;
            playerStatus.totalRunSpeed = playerStatus.runSpeed+playerStatus.addRunSpeed;

            if (playerStatus.totalAttackSpeed > 1)
                playerStatus.totalAttackSpeed = 1;

            //attackDelay에다가 공속 반영해서 계산하는 식 추가해야한다.
            attackDelay = 2.1f - (2f * playerStatus.totalAttackSpeed);//총 공속은 0~1사이 값으로 1이면 공속 제한 풀린다
            rpgCharacterController.animationSpeed = 1f + (playerStatus.totalAttackSpeed*0.5f);
            if(isDash)
                playerStatus.movementStat.sprintSpeed = playerStatus.totalRunSpeed*5;
            else
                playerStatus.movementStat.runSpeed = playerStatus.totalRunSpeed;
        }

        //무기에 따른 공격 속도 설정//공격 속도는 플레이어의 오른손 무기에 따라 결정된다
        void AttackSpeedSet()
        {
            float attackSpeed = 0f;
            switch (playerStatus.rightWeapon[playerStatus.nowWeaponSet])
            {
                case (int)Weapon.Unarmed: attackSpeed = 0.5f; break;
                case (int)Weapon.RightDagger: attackSpeed = 0.7f; break;
                case (int)Weapon.RightItem: attackSpeed = 0.7f; break;
                case (int)Weapon.RightMace: attackSpeed = 0.3f; break;
                case (int)Weapon.RightSword: attackSpeed = 0.3f; break;
                case (int)Weapon.RightSpear: attackSpeed = 0.5f; break;
                case (int)Weapon.TwoHandAxe: attackSpeed = 0f; break;
                case (int)Weapon.TwoHandSpear: attackSpeed = 0.1f; break;
                case (int)Weapon.TwoHandSword: attackSpeed = 0f; break;
            }
            playerStatus.rightWeaponSpeed[playerStatus.nowWeaponSet]=attackSpeed;
        }
    }
}