using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{
    protected Camera m_Camera;
    protected Vector3 direction;
    protected CharacterController characterController;
    public float gravityMod;
    public Transform groundCheckPoint;
    protected bool isGrounded;
    public LayerMask groundLayers;

    public float speed;
    public float smoothTurnTime;
    protected float smoothTurnVelocity;

    protected Joystick joystick;



    protected void Awake()
    {
        characterController = GetComponent<CharacterController>();
        m_Camera = Camera.main;
        joystick = UIController.instance.joystick;
    }


    protected void Update()
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
    

}
