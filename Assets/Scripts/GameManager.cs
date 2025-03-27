using Assets.Scripts.GameConfig;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public ISelectedGame selectedGame;
    [SerializeField] private TMP_Text playerScore;
    [SerializeField] private TMP_Text opponentScore;
    private int playerPoints = 0;
    private int opponentPoints = 0;
    public bool start = false;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void LateUpdate()
    {
        playerScore.text = playerPoints.ToString();
        opponentScore.text = opponentPoints.ToString();
    }
    public void NewGame()
    {
        start = false;
        playerPoints = 0;
        opponentPoints = 0;
    }

    public void Reset()
    {
        start = false;
    }

    public void ScorePointPlayer()
    {
        playerPoints++;
    }

    public void ScorePointOpponent()
    {
        opponentPoints++;
    }
}
