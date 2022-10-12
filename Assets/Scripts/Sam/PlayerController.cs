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
    public bool isRunning;
    public  bool isBuilding;
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
        //Debug.Log(isGrounded);
        isGrounded = Physics.Raycast(groundCheckPoint.position, Vector3.down, .25f, groundLayers);

        direction = new Vector3(horizontal, direction.y, vertical).normalized;
        direction.y += Physics.gravity.y * Time.deltaTime * gravityMod;

        Vector3 vector3 = new Vector3(direction.x, 0, direction.z);
        if (vector3.magnitude >= 0.1f)
        {
            animator?.SetBool("isRunning", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothTurnVelocity, smoothTurnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            isRunning = true;
        }
        else {

            animator?.SetBool("isRunning", false);
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
                Pick(x, y, z);
            }

        }
    }

    void Pick(float x, float y, float z)
    {
        SnapZone snapZone = pickAndDropNetWork.CurrentSnapZone;

        // empty hands, try to pick
        if (_currentPickable == null)
        {
            if (snapZone == null) return;
            if (snapZone.GetComponent<Token_Pick>() != null)
            {
                if (!snapZone.GetComponent<Token_Pick>().isAvailable) return;

            }
            _currentPickable = snapZone as IPickable;
          

            if (_currentPickable != null)
            {
                if (_currentPickable.gameObject.GetComponent<Token_Pick>().isAvailable)
                {
                    int key = _currentPickable.gameObject.GetComponent<Token_Pick>().key;
                    photonView.RPC("HandlePickUp", RpcTarget.AllViaServer, key);
                    pickAndDropNetWork.Remove(_currentPickable as SnapZone);
                    return;

                }


            }

            // SnapZone only (not a IPickable)




            int keySnap = snapZone.gameObject.GetComponent<Token_snapZone>().key;

            photonView.RPC("HandlePickUpSlot", RpcTarget.AllViaServer, keySnap);

            return;
        }

        // we carry a pickable, let's try to drop it

        // no snap zone in range or at most a Pickable in range (we ignore it)
        if (snapZone == null || snapZone is IPickable)
        {
            if (_currentPickable != null)
            {
                if (!_currentPickable.gameObject.GetComponent<Token_Pick>().isAvailable)
                {
                    int key = _currentPickable.gameObject.GetComponent<Token_Pick>().key;
                    photonView.RPC("HandleDrop", RpcTarget.AllViaServer, key, x, y, z);
                    _currentPickable = null;
                    return;
                }
         
            }

        }

       
           
            


        if (_currentPickable != null)
        {
            int key = _currentPickable.gameObject.GetComponent<Token_Pick>().key;
            photonView.RPC("HandleState", RpcTarget.AllViaServer, key);

        }

       


        if (_currentPickable != null)
        {
            if (snapZone != null)
            {
                int snap = snapZone.gameObject.GetComponent<Token_snapZone>().key;
                int pickkey = _currentPickable.gameObject.GetComponent<Token_Pick>().key;
                photonView.RPC("TryDrop", RpcTarget.AllViaServer, snap, pickkey);
               

            }
        }


        _currentPickable = null;

    }

    [PunRPC]
    public virtual void HandlePickUp(int key)
    {
        IPickable item = Token_Manager.DefaultInstance.pickables_tokens[key];

        if (!item.gameObject.GetComponent<Token_Pick>().isAvailable) return;

        item.gameObject.GetComponent<Token_Pick>().isAvailable = false ;
        item.Pick();
        animator?.SetBool("isLifting", true);
        item.gameObject.transform.SetPositionAndRotation(slot.transform.position, Quaternion.identity);
        item.gameObject.transform.SetParent(slot);

    }
    [PunRPC]
    public virtual void HandleDrop(int key,float x, float y, float z)
    {
        IPickable item = Token_Manager.DefaultInstance.pickables_tokens[key];

        if (item.gameObject.GetComponent<Token_Pick>().isAvailable) return;
        item.gameObject.GetComponent<Token_Pick>().isAvailable = true;

        animator?.SetBool("isLifting", false);
        Vector3 pos = new Vector3(x, y, z);

        item.Drop(pos);


    }

    [PunRPC]
    public virtual void HandlePickUpSlot(int keySnapzone)
    {

        SnapZone snapZone = Token_Manager.DefaultInstance.snapzones_tokens[keySnapzone];

        animator?.SetBool("isLifting", true);

        _currentPickable = snapZone?.TryToPickUpFromSlot(_currentPickable);

        StartCoroutine(GetPickUp(_currentPickable));





    }

    IEnumerator GetPickUp(IPickable p)
    {
        yield return new WaitForEndOfFrame();
        int key = p.gameObject.GetComponent<Token_Pick>().key;
        photonView.RPC("HandlePickUp", RpcTarget.AllViaServer, key);
    }
    [PunRPC]
    public virtual void HandleState(int keyPickable)
    {

        IPickable item = Token_Manager.DefaultInstance.pickables_tokens[keyPickable];
        if (!item.gameObject.GetComponent<Token_Pick>().isAvailable) return;
        item?.gameObject.transform.SetPositionAndRotation(slot.position, Quaternion.identity);
        item?.gameObject.transform.SetParent(slot);

    }

    [PunRPC]

    public virtual void TryDrop(int keySnapzone, int keyPickable)
    {
        IPickable item = Token_Manager.DefaultInstance.pickables_tokens[keyPickable];
        if (item.gameObject.GetComponent<Token_Pick>().isAvailable) return;

        SnapZone snapZone = Token_Manager.DefaultInstance.snapzones_tokens[keySnapzone];


        item.gameObject.GetComponent<Token_Pick>().isAvailable = true;
       
        animator?.SetBool("isLifting", false);

         snapZone.TryToDropIntoSlot(item);
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
