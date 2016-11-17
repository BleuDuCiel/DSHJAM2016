using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace characterControl
{
    [RequireComponent(typeof(CharacterController))]

    public class CharacterInputs : MonoBehaviour
    {

        private CharacterController m_Character;
        private bool m_Jump;
        private bool m_Attack;
        private float m_Move;


        private void Awake()
        {
            m_Character = GetComponent<CharacterController>();
        }

        // Use this for initialization
        void Start()
        {

        }

        private void Update()
        {


            m_Jump = Input.GetKeyDown(KeyCode.Space);
            m_Attack = Input.GetKeyDown(KeyCode.M);
            m_Move = Input.GetAxis("Horizontal0");

            // Pass all parameters to the character control script.

            m_Jump = false;
            m_Attack = false;
            m_Move = 0.0f;

        }
        private void FixedUpdate()
        {


        }
    }
}