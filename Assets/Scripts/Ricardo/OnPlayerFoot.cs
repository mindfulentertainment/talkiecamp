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

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit player");

        }
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
            rb.AddForce((transform.position - currentPlayer.transform.position) * shotingForce);
            StartCoroutine(PlayerOn());
        }
    }


    IEnumerator PlayerOn()
    {
        yield return new WaitForSeconds(2);
        isOnPlayer = false;

    }
    private void OnDestroy()
    {
        shootBall.GEvent -= ShootBall;
    }
}
