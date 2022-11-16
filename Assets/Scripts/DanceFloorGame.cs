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
    float time = 5;
    int level=0;
    bool isPlaying;
    Piece firstPiece;
    Piece secondPiece;
    float currenTime=20;

    public List<FloorPattern> floorPatterns;
    public Image guide;
    public Transform startPos;
    public Transform endPos;
    public Image patternSprite;

    float timeToLerp = 3; //lerp for two seconds.




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
        Element element = new Element(Element.ElementType.connection, 3);
        DataManager.instance.IncreaseElement(element);
        Handheld.Vibrate();
        CameraShake.Shake(0.25f, 0.5f);
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
        
       
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].State != floorPatterns[random].targetPiece[i])
            {
                level = 0;
                timeToLerp = 3;
                isPlaying = false;

                return false;
            }
        }

        timeToLerp -= 0.34f;
        return true;
    }
    IEnumerator CheckPattern(int random)
    {
        yield return new WaitUntil(() => patternSprite.transform.position == endPos.position);

        if (CheckWin(random))
        {
            if (PhotonNetwork.IsMasterClient)
            {
                StartTheGame();

            }
        }
        else
        {

            PlayerSpawner.instance.player.GetComponent<PlayerCallBacks>().StopDanceFloor();
            
        }
    }

    public void Lose()
    {
        patternSprite.gameObject.SetActive(false);
        guide.gameObject.SetActive(false);
        startGame.gameObject.SetActive(false);
    }
}
