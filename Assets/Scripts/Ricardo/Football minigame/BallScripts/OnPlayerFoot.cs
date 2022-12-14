using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

[RequireComponent(typeof(Rigidbody))]
public class OnPlayerFoot : MonoBehaviourPun
{

    [SerializeField] bool isOnPlayer;
    [SerializeField] Sprite ballInteractionIcon;
    [SerializeField] Rigidbody rb;
    [SerializeField] REvents shootBall,goalT1,goalT2;
    [SerializeField] GameObject currentPlayer;
    [SerializeField] float shotingForce;
    [SerializeField] ParticleSystem confetiPS;
    bool stop;
   
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        shootBall.GEvent += ShootBall;
        goalT1.GEvent += RestartBall;
        goalT2.GEvent += RestartBall;
    }
    private void OnDisable()
    {
        shootBall.GEvent -= ShootBall;
        goalT1.GEvent -= RestartBall;
        goalT2.GEvent -= RestartBall;
        shootBall.GEvent -= ShootBall;
        shootBall.GEvent -= RestartBall;
        StopAllCoroutines();

    }

    public void GetBall(GameObject player)
    {
        if (currentPlayer == player) return;

        GetComponent<PhotonRigidbodyView>().enabled = true;
        photonView.FindObservables();    //UIController.instance.pickBtn.GetComponent<Image>().sprite = ballInteractionIcon;    //cambia el icono de Ui de interaccion
        currentPlayer = player; //determina cual es el jugador actual que tiene la pelota
            isOnPlayer = true;
            this.gameObject.transform.parent = player.transform; //vuelve la bola en hijo al jugador
            transform.position = currentPlayer.transform.GetChild(3).position; //snapea la posicion de la bola al lugar del pie
            rb.constraints = RigidbodyConstraints.FreezePosition; // freeze rotation and pos
        if (player.GetComponent<PhotonView>().IsMine)
        {
            UIController.instance.SwitchPickSprite(UIController.instance.guayo);
        }

    }




    void ShootBall()
    {
        if (isOnPlayer == true)
        {
            GetComponent<PhotonRigidbodyView>().enabled = true;
            photonView.FindObservables();
            this.gameObject.transform.SetParent(null);
            rb.constraints = RigidbodyConstraints.None; // freeze rotation and pos
            UIController.instance.ResetPickSprite();

            photonView.RPC("AddForceToBall", RpcTarget.AllViaServer);
            StartCoroutine(PlayerOn());

        }
    }
    void RestartBall()
    {

        confetiPS.Play();
        TryGetComponent(out rb);
        rb.constraints = RigidbodyConstraints.None; // freeze rotation and pos

       
            rb.velocity=Vector3.zero;
            this.gameObject.transform.SetParent(null);
            StartCoroutine(PlayerOn());
        
    }


    IEnumerator PlayerOn()
    {
        yield return new WaitForSeconds(0.8f);
        currentPlayer = null;
        isOnPlayer = false;

    }
   

    [PunRPC]
    public void AddForceToBall()
    {


        GetComponent<PhotonRigidbodyView>().enabled = true;
        photonView.FindObservables();
        if (PhotonNetwork.IsMasterClient)
        {
            rb.AddForce(((transform.position - currentPlayer.transform.position)) * shotingForce);
        }
    }
}
