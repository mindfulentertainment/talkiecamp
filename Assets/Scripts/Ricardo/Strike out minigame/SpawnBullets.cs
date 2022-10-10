using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawnBullets : MonoBehaviourPun
{
    [SerializeField] REvents shootBullet;
    [SerializeField] Transform shootPoint;
    
    void Start()
    {
        shootBullet.GEvent += AppearBullet;
    }

    void AppearBullet()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("ShootBullet", RpcTarget.AllViaServer);
        }
    }

    [PunRPC]
    void ShootBullet()
    {
        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = shootPoint.transform.position;
            bullet.transform.rotation = shootPoint.transform.rotation;
            bullet.SetActive(true);
        }
    }


    private void OnDestroy()
    {
        shootBullet.GEvent -= AppearBullet;
    }
}
