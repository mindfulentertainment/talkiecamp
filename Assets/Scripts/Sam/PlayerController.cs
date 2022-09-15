using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{
    
    [Header("Movement")]
    protected CharacterController characterController;
    protected Joystick joystick;
    protected Button pickBtn;
    public float speed;
    public float smoothTurnTime;
    protected float smoothTurnVelocity;
    [SerializeField] Animator animator;

    [Header("PickUpObjects")]
    [SerializeField] private Transform slot;
   
    private PickAndDropNetWork pickAndDropNetWork;
    private IPickable _currentPickable;

    protected Camera m_Camera;
    protected Vector3 direction;
    public float gravityMod;
    public Transform groundCheckPoint;
    protected bool isGrounded;
    public LayerMask groundLayers;
    public string role;
    bool isRunning;
    protected  bool isBuilding;
    public Transform snap;


    public Transform GetSnap()
    {
        return snap;
    }
    protected void Awake()
    {
      
        pickAndDropNetWork = GetComponentInChildren<PickAndDropNetWork>();
        characterController = GetComponent<CharacterController>();
        m_Camera = Camera.main;
        joystick = UIController.instance.joystick;
        pickBtn = UIController.instance.pickBtn;
        if (photonView.IsMine)
        {
            pickBtn.onClick.AddListener(HandleButton);
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            Movement();
        }
    }

    private void Movement()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;


        if (characterController.isGrounded)
        {
            direction.y = 0;
        }
        Debug.Log(isGrounded);
        isGrounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, .25f, groundLayers);

        direction = new Vector3(horizontal, direction.y, vertical).normalized;
        direction.y += Physics.gravity.y * Time.deltaTime * gravityMod;

        Vector3 vector3 = new Vector3(direction.x, 0, direction.z);
        if (vector3.magnitude >= 0.1f)
        {
            animator.SetBool("isRunning", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothTurnVelocity, smoothTurnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            isRunning = true;
        }
        else {

            animator.SetBool("isRunning", false);
            isRunning = false;

        }


        characterController.Move(direction * speed * Time.deltaTime);

    }

    private void HandleButton()
    {
        if (photonView.IsMine)
        {
            if (!isRunning)
            {

                float x =snap.position.x;
                float y =snap.position.y;
                float z =snap.position.z;
                photonView.RPC("HandlePickUp", RpcTarget.AllViaServer,x,y,z) ;

            }

        }
    }



    [PunRPC]
    public virtual void HandlePickUp(float x, float y, float z)
    {
        Vector3 pos = new Vector3(x,y,z);
        var snapZone = pickAndDropNetWork.CurrentSnapZone;

        // empty hands, try to pick
        if (_currentPickable == null)
        {
            _currentPickable = snapZone as IPickable;
            if (_currentPickable != null)
            {
                _currentPickable.Pick();
                animator.SetBool("isLifting", true);
                pickAndDropNetWork.Remove(_currentPickable as SnapZone);
                _currentPickable.gameObject.transform.SetPositionAndRotation(slot.transform.position, Quaternion.identity);
                _currentPickable.gameObject.transform.SetParent(slot);
                return;
            }

            // SnapZone only (not a IPickable)
            
                _currentPickable = snapZone?.TryToPickUpFromSlot(_currentPickable);

            

            if (_currentPickable != null )
            {
               // animator.SetBool("isLifting", true);
                _currentPickable?.gameObject.transform.SetPositionAndRotation(slot.position, Quaternion.identity);
                _currentPickable?.gameObject.transform.SetParent(slot);
            }
           
            return;
        }

        // we carry a pickable, let's try to drop it

        // no snap zone in range or at most a Pickable in range (we ignore it)
        if (snapZone == null || snapZone is IPickable)
        {
            if (_currentPickable != null)
            {
                animator.SetBool("isLifting", false);
                _currentPickable.Drop(pos);
                _currentPickable = null;
                return;
            }
           
        }

        // we carry a pickable and we have an snap zone in range
        // we may drop into the snap zone

        // Try to drop on the snap zone. It may refuse it, e.g. dropping a plate into the CuttingBoard,
        // or simply it already have something on it
        //Debug.Log($"[PlayerController] {_currentPickable.gameObject.name} trying to drop into {interactable.gameObject.name} ");

        if (_currentPickable != null)
        {
            if (snapZone != null)
            {
                animator.SetBool("isLifting", false);
                bool dropSuccess = snapZone.TryToDropIntoSlot(_currentPickable);
                if (!dropSuccess) return;

            }
        }
        

        _currentPickable = null;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (photonView.IsMine)
        {

            if (other.gameObject.CompareTag("Store"))
            {
                if (photonView.IsMine)
                {
                    if (LayerMask.LayerToName(other.gameObject.layer) == role && !isBuilding)
                    {
                        UIController.instance.storeButton.SetActive(true);
                    }
                    else
                    {
                        string message = "Solo " + LayerMask.LayerToName(other.gameObject.layer) + " puede interactuar con esto";

                        UIController.instance.ShowCaption(message);

                    }
                }

            }

            if (other.gameObject.CompareTag("Obstacle"))
            {
                if (photonView.IsMine)
                {
                    string message = "Parece que aqui faltan unas escaleras";

                    UIController.instance.ShowCaption(message);
                }

            }

            if (other.gameObject.CompareTag("Ball"))
            {
                if (photonView.IsMine)
                {
                    photonView.RPC("GetBall", RpcTarget.AllBufferedViaServer);
                }
            }

            if (other.gameObject.CompareTag("NPC"))
            {
                other.gameObject.GetComponent<NPCPoint>().Enter();

            }
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        
            if (photonView.IsMine)
            {
                UIController.instance.storeButton.SetActive(false);
                UIController.instance.HideCaption();
                isBuilding = false;

                if (other.gameObject.CompareTag("NPC"))
                {
                    other.gameObject.GetComponent<NPCPoint>().Exit();

                }
              }

       

    }

    [PunRPC]
    public void GetBall()
    {
        FootBallScoreManager.Instance.ball.gameObject.GetComponent<OnPlayerFoot>().GetBall(this.gameObject);

    }
}
