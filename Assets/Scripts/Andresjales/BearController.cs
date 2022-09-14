using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform path;
    List<Transform> buildings;
    int state = 0;
    int index = 0;
    Transform target;
    float timer = 0;


    private void Start()
    {
        buildings = new List<Transform>();
        for (int i = 0; i < path.childCount; i++)
        {
            buildings.Add(path.GetChild(i));
        }

        target = buildings[0];
    }

    private void Update()
    {
        if (state == 0)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if(distance < 3f)
            {
                timer += Time.deltaTime;
                AttackBuilding();

                if (timer >= 10)
                {
                    index = (index + 1) % buildings.Count;
                    target = buildings[index];
                    timer = 0;
                }
            }
        }

        Debug.Log(timer);
        agent.SetDestination(target.position);
    }

    private void AttackBuilding()
    {
        //Destruir contrucciones, por ejemplo cambiar el estado de una construcción a destruida.
    }
}
