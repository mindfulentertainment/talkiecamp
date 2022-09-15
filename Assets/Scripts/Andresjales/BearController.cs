using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    List<Vector3> nodes;
    int index = 0;
    Vector3 target;
    float timer = 0;


    private void Start()
    {
        StateManager.Instance.OnResourcesLoad.AddListener(GetBuildingsInfo);
    }
    private void OnDisable()
    {
        StateManager.Instance.OnResourcesLoad.RemoveListener(GetBuildingsInfo);
    }

    void GetBuildingsInfo(Resource resources, Buildings buildings)
    {
        nodes = new List<Vector3>();

        foreach (var item in buildings.buildings)
        {
            nodes.Add(item.GetPosition());
        }

        target = nodes[0];
    }

    private void Update()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target);
            if (distance < 5f)
            {
                timer += Time.deltaTime;
                AttackBuilding();

                if (timer >= 10)
                {
                    index = (index + 1) % nodes.Count;
                    target = nodes[index];
                    timer = 0;
                }
            }

            agent.SetDestination(target);
        }
    }

    private void AttackBuilding()
    {
        //Destruir contrucciones, por ejemplo cambiar el estado de una construcción a destruida.
    }
}
