using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FootBallScoreManager : MonoBehaviour
{
    [SerializeField] REvents team1G, team2G,winTeam1,winTeam2; 
    [SerializeField] int score1, score2,scoreGoal;
    [SerializeField] TextMeshProUGUI scoreT1, scoreT2;
    public GameObject ball;
    public static FootBallScoreManager Instance;

    void Start()
    {
        team1G.GEvent += ScoreTeam1;
        team2G.GEvent += ScoreTeam2;
        scoreT1.text="00";
        scoreT2.text = "00";
    }
    private void Awake()
    {
        Instance=this;
    }
    void ScoreTeam1()
    {
        score1 += 1;
        UpdateTeamScore(score1, scoreT1);
        if (score1 >= scoreGoal)
        {
            winTeam1.FireEvent();
        }
    }
    void ScoreTeam2()
    {
        score2 += 1;
        UpdateTeamScore(score2, scoreT2);
        if (score2 >= scoreGoal)
        {
            winTeam2.FireEvent();
        }
    }
    void UpdateTeamScore(int scoreTeam,TextMeshProUGUI scoreUI)
    {
        if (scoreTeam <= 9)
        {
            scoreUI.text = "0" + scoreTeam.ToString();
        }
        else
        {
            scoreUI.text = scoreTeam.ToString();
        }
        
    }
    private void OnDestroy()
    {
        team1G.GEvent -= ScoreTeam1;
        team2G.GEvent -= ScoreTeam2;
    }
    private void OnDisable()
    {
        team1G.GEvent -= ScoreTeam1;
        team2G.GEvent -= ScoreTeam2;
    }
}
