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
    private Vector3 movement;
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

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        m_Camera = Camera.main;
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
            mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSentitivity;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

            verticalRotStore += mouseInput.y;
            verticalRotStore = Mathf.Clamp(verticalRotStore, -60, 60);
            viewPoint.rotation = Quaternion.Euler(-verticalRotStore, viewPoint.rotation.eulerAngles.y, viewPoint.rotation.eulerAngles.z);

            movDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
            {
                activeSpeed = runSpeed;

            }
            else
            {
                activeSpeed = moveSpeed;

            }
            float yVel = movement.y;
            movement = ((transform.forward * movDir.z) + (transform.right * movDir.x).normalized) * activeSpeed;
            movement.y = yVel;
            if (characterController.isGrounded)
            {
                movement.y = 0;
            }

            isGrounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, .25f, groundLayers);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                movement.y = JumpForce;
            }
            movement.y += Physics.gravity.y * Time.deltaTime * gravityMod;
            characterController.Move(movement * Time.deltaTime);


        }
    }
    private void LateUpdate()
    {
        if (photonView.IsMine)
        {
            m_Camera.transform.position = viewPoint.position;
            m_Camera.transform.rotation = viewPoint.rotation;
        }


    }
   

}
