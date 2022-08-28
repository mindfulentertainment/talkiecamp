using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Builder : PlayerController
{
    [PunRPC]
    public override void HandlePickUp()
    {
        base.HandlePickUp();    
    }

}
