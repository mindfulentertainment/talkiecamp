using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTP : MonoBehaviour
{
     public Transform losingPos;
    
    [SerializeField] bool outGame;
    //[SerializeField] ParticleSystem splash;    //efecto de splash de cuando impacta

    public void TPOut()
    {

        outGame = true;
        
    }
    private void FixedUpdate()
    {
        if (losingPos == null) return;

        if (outGame == true)
        {
            transform.position = losingPos.position;
            outGame = false;
        }
    }

}
