using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;    // A mask determining what is ground to the character
        

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        BoxCollider2D boxColl;


        public float hp = 3f;
        public float gold = 0;
        public bool key = false;
        public bool rotatey = false;

        public Image hpNum;
        public Sprite sprite0;
        public Sprite sprite1;
        public Sprite sprite2;
        public Sprite sprite3;

        public Image goldNum;
        public Sprite sprite4;
        public Sprite sprite5;
        public Sprite sprite6;
        public Sprite sprite7;
        public Sprite sprite8;
        public Sprite sprite9;



        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            boxColl = gameObject.GetComponent<BoxCollider2D>();
            

        }

        private void GetComponentInParent<T>(Vector2 vector2)
        {
            throw new NotImplementedException();
        }

        public void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag == "enemy")
            {
                Destroy(col.gameObject);
                if (hp >= 0)
                {

                    hp = hp - 1;
                }
                else if (hp < 0)
                {
                    hp = -1;
                }
                ChangeHPImg();
                Debug.Log(hp);
            }

            if (col.gameObject.tag == "door")
            {
                Destroy(col.gameObject);
            }
            if (col.gameObject.tag == "lock door")
            {
                if (key == true)
                {
                    Destroy(col.gameObject);
                }
            }

        }

        public void OnTriggerStay2D(Collider2D col)
        {
            if (col.gameObject.tag == "potion")
            {
                Destroy(col.gameObject);
                if (hp < 3)
                {
                    hp = hp + 1;
                }
                
                else if (hp == 3)
                {
                    hp = 3;
                }
                ChangeHPImg();
            }
            if (col.gameObject.tag == "key")
            {
                Destroy(col.gameObject);
                key = true;
            }
            if (col.gameObject.tag == "button" && Input.GetKeyDown("e"))
            {
                rotatey = true;
            }

            if (col.gameObject.tag == "gold")
            {
                Destroy(col.gameObject);
                gold = gold + 1;
                ChangeGoldImg();
                Debug.Log(gold);
            }

        }
        void ChangeGoldImg()
        {
            if (gold == 9)
            {
                goldNum.GetComponent<Image>().sprite = sprite9;

            }
            else if (gold == 8)
            {
                goldNum.GetComponent<Image>().sprite = sprite8;

            }
            else if (gold == 7)
            {
                goldNum.GetComponent<Image>().sprite = sprite7;

            }
            else if (gold == 6)
            {
                goldNum.GetComponent<Image>().sprite = sprite6;

            }
            else if (gold == 5)
            {
                goldNum.GetComponent<Image>().sprite = sprite5;

            }
            else if (gold == 4)
            {
                goldNum.GetComponent<Image>().sprite = sprite4;

            }
            else if (gold == 3)
            {
                goldNum.GetComponent<Image>().sprite = sprite3;

            }
            else if (gold == 2)
            {
                goldNum.GetComponent<Image>().sprite = sprite2;

            }
            else if (gold == 1)
            {
                goldNum.GetComponent<Image>().sprite = sprite1;

            }
            else if (gold == 0)
            {
                goldNum.GetComponent<Image>().sprite = sprite0;

            }

        }

        void ChangeHPImg()
        {
            if (hp == 3)
            {
                hpNum.GetComponent<Image>().sprite = sprite3;

            }
            else if (hp == 2)
            {
                hpNum.GetComponent<Image>().sprite = sprite2;

            }
            else if (hp == 1)
            {
                hpNum.GetComponent<Image>().sprite = sprite1;

            }
            else if (hp == 0)
            {
                hpNum.GetComponent<Image>().sprite = sprite0;

            }
        }

        private void FixedUpdate()
        {
            m_Grounded = false;
            
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }
            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
         
            
        }
        float delay = 5f; 
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                boxColl.isTrigger = true;
            }
            m_Anim = GetComponent<Animator>();

            m_Anim.SetBool("blink", false);
            delay -= Time.deltaTime;
            Debug.Log(delay);
            if (delay <= 1)
            {
                m_Anim.SetBool("blink", true);
                if (delay <= 0)
                {
                    delay = 5;
                }
            }

        }

        void OnTriggerExit2D(Collider2D coll)
        {
            boxColl.isTrigger = false;
        }
    


    public void Move(float move, bool crouch, bool jump)
        {
            // If crouching, check to see if the character can stand up
            if (!crouch && m_Anim.GetBool("Crouch"))
            {
                // If the character has a ceiling preventing them from standing up, keep them crouching
                if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
                {
                    crouch = true;
                }
            }

            // Set whether or not the character is crouching in the animator
            m_Anim.SetBool("Crouch", crouch);

            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                move = (crouch ? move*m_CrouchSpeed : move);

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            }
        }


        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
