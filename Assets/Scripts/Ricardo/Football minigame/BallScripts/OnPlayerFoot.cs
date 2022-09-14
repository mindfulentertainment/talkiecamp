using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnPlayerFoot : MonoBehaviour
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
            currentPlayer = player; //determina cual es el jugador actual que tiene la pelota
            isOnPlayer = true;
            this.gameObject.transform.parent = player.transform; //vuelve la bola en hijo al jugador
            transform.position = currentPlayer.transform.GetChild(3).position; //snapea la posicion de la bola al lugar del pie
            rb.isKinematic = true;  //pone kinematico el objeto para que no lo perturben otras fuerzas
                                    //UIController.instance.pickBtn.GetComponent<Image>().sprite = ballInteractionIcon;    //cambia el icono de Ui de interaccion
        }
    }
    void ShootBall()
    {
        if (isOnPlayer == true)
        {
            rb.isKinematic = false;
            this.gameObject.transform.SetParent(null);
            rb.AddForce(((transform.position - currentPlayer.transform.position).normalized) * shotingForce);
            StartCoroutine(PlayerOn());
        }
    }
    void RestartBall()
    {
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
        yield return new WaitForSeconds(1f);
        isOnPlayer = false;

    }
    private void OnDestroy()
    {
        shootBall.GEvent -= ShootBall;
    }
}
