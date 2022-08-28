using UnityEngine;

public interface IPickable
{
    GameObject gameObject { get; }
    public void Pick();
    public void Drop();
}
