using Assets.Scripts.GameConfig;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        if (selectedGame.GameLength == GameLength.SHORT)
        {
            if (playerPoints == 11)
            {
                NewGame();
                SceneManager.LoadScene("MainMenu");
            }
        }
        else
        {
            if (playerPoints == 21)
            {
                NewGame();
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    public void ScorePointOpponent()
    {
        opponentPoints++;

        if (selectedGame.GameLength == GameLength.SHORT)
        {
            if (opponentPoints == 11)
            {
                NewGame();
                SceneManager.LoadScene("MainMenu");
            }
        }
        else
        {
            if (opponentPoints == 21)
            {
                NewGame();
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
