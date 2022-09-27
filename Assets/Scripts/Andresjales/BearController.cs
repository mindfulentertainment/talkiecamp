using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class BearController : MonoBehaviourPunCallbacks
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;
    Coroutine damage;
    List<Vector3> nodes;
    int index = 0;
    int state = 0; //0 = Attacking, 1 = Trapped
    float stunTimer = 0;
    Vector3 target;
    bool firstTime=true;
    bool attacking = false;

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
        if (state == 0)
        {
            if (target != null)
            {
                agent.isStopped = false;
                float distance = Vector3.Distance(transform.position, target);
                if (distance < 5f)
                {
                    attacking = true;
                    agent.isStopped = true;

                    AttackBuilding();

                    if (attacking == false)
                    {
                        index = (index + 1) % nodes.Count;
                        target = nodes[index];
                    }
                }

                agent.SetDestination(target);
            }
        }

        if (state == 1)
        {
            stunTimer += Time.deltaTime;
            agent.isStopped = true;
            Debug.Log(stunTimer);

            if (stunTimer >= 20)
            {
                state = 0;
                stunTimer = 0;
                animator.SetBool("Trapped", false);
            }
        }
    }

    public void CaughtInTrap()
    {
        state = 1;
        animator.SetBool("Trapped", true);
        StopCoroutine(damage);
        damage = null;
    }

    private void AttackBuilding()
    {
        animator.SetBool("Attack", true);
        if (damage == null)
        {
            damage = StartCoroutine(Damage());

        }


        if (DataManager.instance.buildingsDictionary[target.ToString()].gameObject.GetComponent<Place>().health<=0) //building.life <= 0
        {

            StopCoroutine(damage);
            damage = null;
            animator.SetBool("Attack", false);
            attacking = false;
            agent.isStopped = false;

        }
    }   

    IEnumerator Damage()
    {
        while (true)
        {

            DataManager.instance.buildingsDictionary[target.ToString()].gameObject.GetComponent<Place>().DamageBuilding(10);

            yield return new WaitForSeconds(2.5f);
        }
    }
}
