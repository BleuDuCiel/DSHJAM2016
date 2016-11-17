using UnityEngine;
using System.Collections;

namespace characterControl
{


    public class CharacterController : MonoBehaviour
    {

        //variables
        [SerializeField]
        private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField]
        private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [SerializeField]
        private LayerMask m_WhatIsGround;
        [SerializeField]
        private Transform m_Proj;

        //ground detection
        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .5f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.

        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private Collider2D[] m_Colliders;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.

        //inputs
        private bool i_Jump = false;
        private bool i_Attack = false;
        private bool i_Item = false;
        private float i_Move = 0.0f;
        private string pre = "KB";
        private string suf = "0";

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.GetChild(0);
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
            m_Colliders = GetComponents<Collider2D>();

        }

        // Use this for initialization
        void Start()
        {

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
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

        }


        // Update is called once per frame
        void Update()
        {
            //m_Anim.SetBool("Hit", false);
            //first thing: get all inputs
            SetInputs();

            Move(i_Move, i_Attack, i_Jump);
        }

        //inputs to set: i_Jump, i_Attack, i_Item, i_Move
        private void SetInputs()
        {

            i_Jump = Input.GetButtonDown(pre + "Jump" + suf);
            i_Attack = Input.GetButtonDown(pre + "Attack" + suf);
            i_Item = Input.GetButtonDown(pre + "Item" + suf);
            i_Move = Input.GetAxis(pre + "Move" + suf);
        }

        void SetupInputs(string i)
        {
            string[] fix = i.Split(',');
            pre = fix[0];
            suf = fix[1];

            Debug.Log(i);
        }

        private void Move(float move, bool attack, bool jump)
        {

            m_Anim.SetFloat("Speed", Mathf.Abs(move));


            // Move the character
            m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

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


            // If the player should jump...
            if (m_Grounded && jump)
            //if(jump && m_Rigidbody2D.velocity.y == 0)
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce + m_Rigidbody2D.velocity.y));
            }


            if (attack)
            {
                Attack();
                m_Anim.SetBool("Attack", true);

            }
            else
            {
                m_Anim.SetBool("Attack", false);
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

        private void Attack()
        {
            Transform p = Instantiate(m_Proj);
            Collider2D c = p.GetComponent<Collider2D>();
            //ignore collision between projectile and all player colliders
            foreach (Collider2D coll in m_Colliders)
            {
                Physics2D.IgnoreCollision(coll, c);
            }

            Vector3 pos = p.position;
            float dir = 1.0f;
            if (!m_FacingRight)
            {
                dir = -1.0f;
                pos.x *= -1.0f;
            }

            p.position = transform.position + pos;

            p.gameObject.SendMessage("Setup", dir);

        }

        private void Hit()
        {
            m_Anim.SetBool("Hit", true);
            StartCoroutine(Wait(0.2f));

        }
        private IEnumerator Wait(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            m_Anim.SetBool("Hit", false);
        }
    }


}