using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{
    private Camera m_Camera;
    public Transform viewPoint;
    public float mouseSentitivity = 1f;
    private float verticalRotStore;
    private Vector2 mouseInput;
    private Vector3 movDir;
    private Vector3 direction;
    public float moveSpeed;
    public float runSpeed;
    private float activeSpeed;
    private CharacterController characterController;

    public float JumpForce = 12f;
    public float gravityMod;

    public Transform groundCheckPoint;
    private bool isGrounded;
    public LayerMask groundLayers;

    public GameObject characterModel;


    private Joystick joystick;

    public float speed;
    public float smoothTurnTime;

    private float smoothTurnVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        m_Camera = Camera.main;
        joystick = UIController.instance.joystick;
    }
    private void Start()
    {

        if (photonView.IsMine)
        {
            characterModel.SetActive(false);
        }
        
    }

    private void Update()
    {
        if (photonView.IsMine)
        {



            float horizontal = joystick.Horizontal;
            float vertical = joystick.Vertical;



            float yVel = direction.y;
            direction.y = yVel;
            if (characterController.isGrounded)
            {
                direction.y = 0;
            }

            isGrounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, .25f, groundLayers);


            
            direction = new Vector3(horizontal, direction.y, vertical).normalized;
            direction.y += Physics.gravity.y * Time.deltaTime * gravityMod;

        Vector3 vector3 = new Vector3(direction.x, 0, direction.z);
        if (vector3.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothTurnVelocity, smoothTurnTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                characterController.Move(direction * speed * Time.deltaTime);
            }


        }
    }
        private void LateUpdate()
    {
        //if (photonView.IsMine)
        //{
        //    m_Camera.transform.position = viewPoint.position;
        //    m_Camera.transform.rotation = viewPoint.rotation;
        //}


    }
   

}
