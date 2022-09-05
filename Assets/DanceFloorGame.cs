using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class DanceFloorGame : MonoBehaviour
{
    public static DanceFloorGame instance;
    public Material Greenmaterial;
    public Material Basematerial;
    public Material TargetMaterial;
    public Material SecondTargetMaterial;
    public Button startGame;
    public int maxPlayers=2;
    int players;
    int secondandomPiece;
    int randomPiece;
    public TMP_Text info;
    public List<Piece> pieces;
    float time = 8;
    int level=0;
    bool isPlaying;
    Piece firstPiece;
    Piece secondPiece;
    float currenTime=20;

    public List<FloorPattern> floorPatterns;
    public Image guide;
    public Transform startPos;
    public Transform endPos;
    public float speed;
    public Image patternSprite;

    float timeToLerp = 6; //lerp for two seconds.




    private void OnEnable()
    {
        startGame.onClick.AddListener(StartTheGame);
    }
    private void OnDisable()
    {
        startGame.onClick.RemoveListener(StartTheGame);

    }
    void StartTheGame()
    {


        PlayerSpawner.instance.player.GetComponent<PhotonView>().RPC("StartDanceFloor", RpcTarget.AllBuffered, (int)Random.Range(0, floorPatterns.Count));

    }
    private void OnTriggerStay(Collider other)
    {
        if (!isPlaying)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (players >= maxPlayers)
                {
                    startGame.gameObject.SetActive(true);
                    info.gameObject.SetActive(false);
                }
                else
                {
                    info.gameObject.SetActive(true);
                    info.text = "Se requiere de " +maxPlayers+" jugadores";
                }
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isPlaying)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                players++;



            }
        }
     

    }
    private void OnTriggerExit(Collider other)
    {
        if (!isPlaying)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                players--;
                if (players >= maxPlayers)
                {
                    startGame.gameObject.SetActive(true);

                }
            }
            


        }
        info.gameObject.SetActive(false);
        startGame.gameObject.SetActive(false);
    }





    float timeLerped = 0.0f;
    private void Awake()
    {
        instance = this;
        patternSprite.gameObject.SetActive(false);
        guide.gameObject.SetActive(false);
    }
   
    private void Update()
    {
        if (isPlaying)
        {
            timeLerped += Time.deltaTime;
            patternSprite.transform.position = Vector3.Lerp(startPos.position, endPos.position, timeLerped / timeToLerp);
        }
        

    
    }

    public void ResetGame(int random)
    {
        info.gameObject.SetActive(false);
        startGame.gameObject.SetActive(false);
        isPlaying = true;
        patternSprite.gameObject.SetActive(true);
        guide.gameObject.SetActive(true);
        StopAllCoroutines();
        patternSprite.sprite = floorPatterns[random].pattern;
        guide.sprite = floorPatterns[random].pattern;
        patternSprite.transform.position = startPos.position;
        StartCoroutine(CheckPattern(random));
        timeLerped = 0f;
    }
    bool CheckWin(int random)
    {
        List<int> vs = new List<int>();
        for (int i = 0; i < floorPatterns[random].targetPiece.Length; i++)
        {
            if (floorPatterns[random].targetPiece[i])
            {
                vs.Add(i);
            }
        }
        for (int i = 0; i < vs.Count; i++)
        {
            if (!pieces[vs[i]].State)
            {
                vs.Clear();
                level = 0;
                timeToLerp = 6;
                isPlaying = false;

                return false;
            }
        }
        vs.Clear();

        timeToLerp -= 0.14f;
        return true;
    }
    IEnumerator CheckPattern(int random)
    {
        yield return new WaitUntil(() => patternSprite.transform.position == endPos.position);

        if (CheckWin(random))
        {
            Element element = new Element(Element.ElementType.connection, (int)(5/timeToLerp));
            DataManager.instance.IncreaseElement(element);
            StartTheGame();
        }
        else
        {
            patternSprite.gameObject.SetActive(false);
            guide.gameObject.SetActive(false);
        }
    }
}
