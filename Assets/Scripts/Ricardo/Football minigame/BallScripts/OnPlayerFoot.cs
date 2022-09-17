using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class OnPlayerFoot : MonoBehaviourPunCallbacks
{

    [SerializeField] bool isOnPlayer;
    [SerializeField] Sprite ballInteractionIcon;
    [SerializeField] Rigidbody rb;
    [SerializeField] REvents shootBall,goalT1,goalT2;
    [SerializeField] GameObject currentPlayer;
    [SerializeField] float shotingForce;

   
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        shootBall.GEvent += ShootBall;
        goalT1.GEvent += RestartBall;
        goalT2.GEvent += RestartBall;
    }
    
    
    public void GetBall(GameObject player)
    {
        if (isOnPlayer == false)
        {
            if (currentPlayer == null)
            {
                currentPlayer = player; //determina cual es el jugador actual que tiene la pelota
                currentPlayer.GetComponent<PlayerWithBall>().haveBall = true;  //le asigna verdadero a un boleano que tenga cada jugador
                isOnPlayer = true;
                this.gameObject.transform.parent = player.transform; //vuelve la bola en hijo al jugador
                transform.position = currentPlayer.transform.GetChild(3).position; //snapea la posicion de la bola al lugar del pie
                rb.isKinematic = true;  //pone kinematico el objeto para que no lo perturben otras fuerzas
                                        //UIController.instance.pickBtn.GetComponent<Image>().sprite = ballInteractionIcon;    //cambia el icono de Ui de interaccion
            }
            else if (player != currentPlayer) //esto es para que otro jugador le pueda quitar la bola
            {
                this.gameObject.transform.SetParent(null);
                this.gameObject.transform.parent = player.transform;
                currentPlayer.GetComponent<PlayerWithBall>().haveBall = false;
                currentPlayer = player; //determina cual es el jugador actual que tiene la pelota
                currentPlayer.GetComponent<PlayerWithBall>().haveBall = true;  //le asigna verdadero a un boleano que tenga cada jugador
                isOnPlayer = true;
                this.gameObject.transform.parent = player.transform; //vuelve la bola en hijo al jugador
                transform.position = currentPlayer.transform.GetChild(3).position; //snapea la posicion de la bola al lugar del pie
                rb.isKinematic = true;
            }

        }
    }

   


    void ShootBall()
    {
        if (isOnPlayer == true )
        {
            if (currentPlayer.GetComponent<PlayerWithBall>().haveBall == true) //esto se hace para que otro jugador no pueda patear la bola
            {
                currentPlayer.GetComponent<PlayerWithBall>().haveBall = false;
                rb.isKinematic = false;
                this.gameObject.transform.SetParent(null);
                rb.AddForce(((transform.position - currentPlayer.transform.position)) * shotingForce);
                StartCoroutine(PlayerOn());
            }
            
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
}
