using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform path;
    Transform[] nodes;
    int state = 0;
    int index = 0;
    Transform target;


    private void Start()
    {
        nodes = new Transform[path.childCount];
        for (int i = 0; i < path.childCount; i++)
        {
            nodes[i] = path.GetChild(i);
        }

        target = nodes[0];
    }

    private void Update()
    {
        if (state == 0)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if(distance < 3f)
            {
                index = (index + 1) % nodes.Length;
                target = nodes[index];
            }
        }

        agent.SetDestination(target.position);
    }

}
