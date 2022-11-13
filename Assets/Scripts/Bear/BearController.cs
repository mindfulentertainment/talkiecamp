using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class BearController : MonoBehaviourPun
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] clips;
    Coroutine damage;
    List<Vector3> nodes;
    int index = 0;
    int state = 0; //0 = Attacking, 1 = Trapped
    float stunTimer = 0;
    Vector3 target=Vector3.zero;
    bool firstTime=true;
    bool rest=false;
    bool attacking = false;
    public BearManager bM;
    bool buildings = false;
    bool subscribe = false;
    private void Start()
    {
        MatchManager.instance.OnResourcesLoadGlobal.AddListener(GetBuildingsInfo);
    }
    public  void OnEnable()
    {

        Resource r= new Resource();
        if (!firstTime)
        {
            GetBuildingsInfo(r, DataManager.instance.buildings);
            if (!gameObject.activeSelf) return;
            StartCoroutine(Subscribe());
        }

        if (firstTime)
        {
            firstTime = false;
            gameObject?.SetActive(false);
        }
    }
    
    IEnumerator Subscribe()
    {
        yield return new WaitForSeconds(0.3f);
        DataManager.instance.OnNewBuilding += GetBuildingsInfo;
        subscribe = true;
    }
    public  void OnDisable()
    {
        if (subscribe)
        {
            DataManager.instance.OnNewBuilding -= GetBuildingsInfo;
            subscribe=false;
        }
        target = Vector3.zero;
        MatchManager.instance?.OnResourcesLoadGlobal.RemoveListener(GetBuildingsInfo);
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

           
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {



        if(nodes.Count<=0) return;
        if (state == 0)
        {
            if (target != Vector3.zero)
            {
                if (rest == true)
                {
                    target = BearManager.instance.restPos.position;
                }
                agent.isStopped = false;
                float distance = Vector3.Distance(transform.position, target);
                if (distance < 5f && rest==false)
                {
                    attacking = true;
                    agent.isStopped = true;

                    AttackBuilding();

                 
                }
                if (rest == true)
                {
                    target = BearManager.instance.restPos.position;
                    distance = Vector3.Distance(transform.position, BearManager.instance.restPos.position);

                    if (distance <= 4)
                    {
                        
                            agent.isStopped=true;
                        bM.ReactivateBear();
                        rest= false;
                        this.gameObject.SetActive(false);

                        return;
                    }
                }

            }
            else if(!rest)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    audioSource.PlayOneShot(clips[2]);
                    index = Random.Range(0, nodes.Count);
                    photonView.RPC("SetDestination", RpcTarget.AllViaServer, index);
                }
             
            }
        }

        if (state == 1)
        {
            stunTimer += Time.deltaTime;
            agent.isStopped = true;

            if (stunTimer >= 10)
            {
                state = 0;
                stunTimer = 0;
                animator.SetBool("Trapped", false);
                animator.SetBool("Attack", false);

                target = BearManager.instance.restPos.position;
                agent.SetDestination(target);

            }
        }
    }

    [PunRPC]
    void SetDestination(int i)
    {
        if (rest) return;
        if (nodes.Count <= 0) return;
        target = nodes[i];
        agent.SetDestination(target);

    }


    public void CaughtInTrap()
    {
        if (!rest)
        {
            photonView.RPC("FreezeBear", RpcTarget.AllViaServer);

        }
    }
    [PunRPC]
    public void FreezeBear()
    {
        state = 1;
        animator.SetBool("Trapped", true);
        audioSource.PlayOneShot(clips[0]);

        if (damage != null)
        {
            StopCoroutine(damage);

        }
        damage = null;
        rest = true;
    }
    private void AttackBuilding()
    {
        animator.SetBool("Attack", true);
        if (damage == null)
        {
            damage = StartCoroutine(Damage());

        }


        if (DataManager.instance.buildingsDictionary[target.ToString()].gameObject.GetComponent<Place>().buildingHistory.health<=0) //building.life <= 0
        {
            rest=true;
            nodes.Remove(target);
            StopCoroutine(damage);
            damage = null;
            animator.SetBool("Attack", false);
            attacking = false;
            agent.isStopped = false;
            target = BearManager.instance.restPos.position;
            agent.SetDestination(target);


        }
    }   

    IEnumerator Damage()
    {
        while (true)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                string t=target.ToString();
                photonView.RPC("TryDamage", RpcTarget.AllViaServer, t);

            }

            yield return new WaitForSeconds(2.5f);
        }
    } 

    [PunRPC]

    void TryDamage(string target)
    {
        if (DataManager.instance.buildingsDictionary[target] != null)
        {
            DataManager.instance.buildingsDictionary[target].gameObject.GetComponent<Place>().DamageBuilding(15);
            audioSource.PlayOneShot(clips[1]);

            //audioSource.PlayDelayed(1f);

        }
    }
}
