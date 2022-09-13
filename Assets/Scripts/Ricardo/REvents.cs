using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Events")]
public class REvents : ScriptableObject
{
    public delegate void EventInGame();
    public event EventInGame GEvent; //aca se declara el evento

    public void FireEvent() //esta funcion dispara el evento
    {
        GEvent?.Invoke();
    }
}
