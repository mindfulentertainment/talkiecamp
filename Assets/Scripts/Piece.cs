using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    bool isTarget;
    bool state;
    bool isPlaying;
    private AudioSource source;
    private void Awake()
    {
        source = GetComponent<AudioSource>();   
    }
    public bool State { get => state; set => state = value; }

    public void CheckIn()
    {
        GetComponent<MeshRenderer>().material = DanceFloorGame.instance.Greenmaterial;
        State=true;
        
        
    }

    public void Sound()
    {
        if (!isPlaying)
        {
            source.Play();
        }
    }
    public void CheckOut()
    {
        if (!isTarget)
        {
            GetComponent<MeshRenderer>().material = DanceFloorGame.instance.Basematerial;


        }
        else
        {
            GetComponent<MeshRenderer>().material = DanceFloorGame.instance.TargetMaterial;

        }
        State = false;

    }
    public void ResetPiece()
    {
        isTarget = false;
        GetComponent<MeshRenderer>().material = DanceFloorGame.instance.Basematerial;
        State = false;

    }
    public void MakeTarget(Piece piece)
    {
        if(this== piece)
        {
            GetComponent<MeshRenderer>().material = DanceFloorGame.instance.SecondTargetMaterial;

        }
        else
        {
            GetComponent<MeshRenderer>().material = DanceFloorGame.instance.TargetMaterial;

        }
        isTarget = true;
    }
}
