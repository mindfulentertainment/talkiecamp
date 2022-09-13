using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnPlayerFoot : MonoBehaviour
{
    
    [SerializeField] bool isOnPlayer;
    [SerializeField] Sprite ballInteractionIcon;
    [SerializeField] Rigidbody rb;
    [SerializeField] REvents shootBall;
    [SerializeField] GameObject currentPlayer;
    [SerializeField] float shotingForce;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        shootBall.GEvent += ShootBall;
    }
    private void Update()
    {
        if (isOnPlayer)
        {
            transform.position = currentPlayer.transform.GetChild(3).position; //snapea la posicion de la bola al lugar del pie
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isOnPlayer == false)
            {
                currentPlayer = collision.gameObject; //determina cual es el jugador actual que tiene la pelota
                isOnPlayer = true;
                this.gameObject.transform.SetParent(collision.transform); //vuelve la bola en hijo al jugador
                rb.isKinematic = true;  //pone kinematico el objeto para que no lo perturben otras fuerzas
                //UIController.instance.pickBtn.GetComponent<Image>().sprite = ballInteractionIcon;    //cambia el icono de Ui de interaccion
            }
        }
    }

    void ShootBall()
    {
        if (isOnPlayer == true)
        {
            rb.isKinematic = false;
            this.gameObject.transform.SetParent(null);
            rb.AddForce((transform.position- currentPlayer.transform.position) *shotingForce);
            isOnPlayer = false;
        } 
    }
    
}
