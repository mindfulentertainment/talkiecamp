using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class ShootObject : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Transform movingDir,posIni;
    [SerializeField] float force;
    [SerializeField] REvents thing,dissapear;
   
    Vector3 initialSize;
    void Start()
    {
        transform.position = posIni.position;
        initialSize = transform.localScale;
        rb = GetComponent<Rigidbody>();
        thing.GEvent += Shoot;
        dissapear.GEvent += Disappear;
        transform.localScale = Vector3.zero;
        rb.isKinematic = true;
    }

    void Shoot()
    {
        transform.localScale = initialSize;
        rb.isKinematic = false;
        if (!PhotonNetwork.IsMasterClient)
        {
            GetComponent<PhotonRigidbodyView>().enabled = true;
            GetComponent<PhotonView>().FindObservables();
        }
        else
        {
            rb.AddForce((movingDir.transform.position - transform.position) * force * Time.deltaTime);
            GetComponent<PhotonRigidbodyView>().enabled = true;
            GetComponent<PhotonView>().FindObservables();
        }

    }
    public void Disappear()
    {
        rb.isKinematic=true;
        var player = GetComponentInParent<PlayerController>();
        if (player != null)
        {
            if (player.photonView.IsMine)
            {
                int key = GetComponent<Token_Pick>().key;
                player.photonView.RPC("HandleDrop", RpcTarget.AllViaServer, key, posIni.position.x, posIni.position.y, posIni.position.z);
                player._currentPickable = null;
            }
           
        }
        transform.position = posIni.position;
        transform.localScale = Vector3.zero;
    }
    private void OnDestroy()
    {
        thing.GEvent -= Shoot;
        dissapear.GEvent -= Disappear;
    }

}
