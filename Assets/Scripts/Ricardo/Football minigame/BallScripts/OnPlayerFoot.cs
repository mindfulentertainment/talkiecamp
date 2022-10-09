using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class OnPlayerFoot : MonoBehaviourPun
{

    [SerializeField] bool isOnPlayer;
    [SerializeField] Sprite ballInteractionIcon;
    [SerializeField] Rigidbody rb;
    [SerializeField] REvents shootBall,goalT1,goalT2;
    [SerializeField] GameObject currentPlayer;
    [SerializeField] float shotingForce;
    bool stop;
   
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        shootBall.GEvent += ShootBall;
        goalT1.GEvent += RestartBall;
        goalT2.GEvent += RestartBall;
    }
    
    
    public void GetBall(GameObject player)
    {
        if (isOnPlayer == true) return;
        
            GetComponent<PhotonRigidbodyView>().enabled = false;                  //UIController.instance.pickBtn.GetComponent<Image>().sprite = ballInteractionIcon;    //cambia el icono de Ui de interaccion
            currentPlayer = player; //determina cual es el jugador actual que tiene la pelota
            isOnPlayer = true;
            this.gameObject.transform.parent = player.transform; //vuelve la bola en hijo al jugador
            transform.position = currentPlayer.transform.GetChild(3).position; //snapea la posicion de la bola al lugar del pie
            rb.isKinematic = true;  //pone kinematico el objeto para que no lo perturben otras fuerzas
        
    }

   


    void ShootBall()
    {
        if (isOnPlayer == true)
        {
            
            this.gameObject.transform.SetParent(null);
            photonView.RPC("AddForceToBall", RpcTarget.AllViaServer);
            StartCoroutine(PlayerOn());
        }
    }
    void RestartBall()
    {
        rb = FootBallScoreManager.Instance.ball.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        if (isOnPlayer == true)
        {
            this.gameObject.transform.SetParent(null);
            StartCoroutine(PlayerOn());
        }
        rb.isKinematic = false;
    }


    IEnumerator PlayerOn()
    {
        yield return new WaitForSeconds(0.8f);
        isOnPlayer = false;

    }
    private void OnDisable()
    {
        shootBall.GEvent -= ShootBall;
        shootBall.GEvent -= RestartBall;
        StopAllCoroutines();
    }

    [PunRPC]
    public void AddForceToBall()
    {
        rb.isKinematic = false;
        GetComponent<PhotonRigidbodyView>().enabled = true;                  
        if (PhotonNetwork.IsMasterClient)
        {
            rb.AddForce(((transform.position - currentPlayer.transform.position)) * shotingForce);
        }
    }
}
