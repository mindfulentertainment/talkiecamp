using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class BearController : MonoBehaviourPunCallbacks
{
    [SerializeField] NavMeshAgent agent;
    List<Vector3> nodes;
    int index = 0;
    Vector3 target;
    float timer = 0;
    [SerializeField] Animator animator;
    bool firstTime=true;
    private void Start()
    {
        MatchManager.instance.OnResourcesLoadGlobal.AddListener(GetBuildingsInfo);
    }
    public override void OnEnable()
    {

        Resource r= new Resource();
        if (!firstTime)
        {
            GetBuildingsInfo(r, DataManager.instance.buildings);
            StartCoroutine(Subscribe());
        }
    }
    
    IEnumerator Subscribe()
    {
        yield return new WaitForSeconds(0.3f);
        DataManager.instance.OnNewBuilding += GetBuildingsInfo;

    }
    public override void OnDisable()
    {
        DataManager.instance.OnNewBuilding -= GetBuildingsInfo;

        MatchManager.instance.OnResourcesLoadGlobal.RemoveListener(GetBuildingsInfo);
    }

    void GetBuildingsInfo(Resource resources, Buildings buildings)
    {
        if (buildings.buildings.Count>0)
        {
            nodes = new List<Vector3>();

            foreach (var item in buildings.buildings)
            {
                nodes.Add(item.GetPosition());
            }

            target = nodes[0];
            if (firstTime)
            {
                firstTime = false;
                gameObject.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(false);

        }


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
                        animator.SetBool("Attack", false);

                        timer = 0;

                    }
                }


            }
            agent.SetDestination(target);
         
      
    }


    private void AttackBuilding()
    {
        animator.SetBool("Attack", true);  
            
     }

   
}
