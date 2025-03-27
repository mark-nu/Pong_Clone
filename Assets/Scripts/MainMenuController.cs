using System.Collections.Generic;
using Assets.Scripts.GameConfig;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject difficultyMenu;
    [SerializeField] private Button easyButton;
    [SerializeField] private Button mediumButton;
    [SerializeField] private Button hardButton;
    [SerializeField] private GameObject gameLengthMenu;
    [SerializeField] private Button shortGameButton;
    [SerializeField] private Button longGameButton;
    [SerializeField] private TMP_Text modeText1;
    [SerializeField] private TMP_Text modeText2;
    [SerializeField] private TMP_Text difficultyText;
    private readonly Stack<GameObject> menuPath = new();

    private void Awake()
    {
        startMenu.SetActive(true);
        difficultyMenu.SetActive(false);
        gameLengthMenu.SetActive(false);
    }

    private void OnEnable()
    {
        easyButton.onClick.AddListener(() => OnDifficultyClicked(modeText1.text, "Easy"));
        mediumButton.onClick.AddListener(() => OnDifficultyClicked(modeText1.text, "Medium"));
        hardButton.onClick.AddListener(() => OnDifficultyClicked(modeText1.text, "Hard"));

        shortGameButton.onClick.AddListener(() =>
        {
            GameMode gameMode;
            if (modeText1.text.Contains("1P"))
            {
                gameMode = GameMode.ONE_PLAYER;
            }
            else
            {
                gameMode = GameMode.TWO_PLAYER;
            }

            GameDifficulty gameDifficulty;
            if (difficultyText.text.Contains("E"))
            {
                gameDifficulty = GameDifficulty.EASY;
            }
            else if (difficultyText.text.Contains("M"))
            {
                gameDifficulty = GameDifficulty.MEDIUM;
            }
            else
            {
                gameDifficulty = GameDifficulty.HARD;
            }

            OnGameStart(gameMode, gameDifficulty, GameLength.SHORT);
        });

        shortGameButton.onClick.AddListener(() =>
        {
            GameMode gameMode;
            if (modeText1.text.Contains("1P"))
            {
                gameMode = GameMode.ONE_PLAYER;
            }
            else
            {
                gameMode = GameMode.TWO_PLAYER;
            }

            GameDifficulty gameDifficulty;
            if (difficultyText.text.Contains("E"))
            {
                gameDifficulty = GameDifficulty.EASY;
            }
            else if (difficultyText.text.Contains("M"))
            {
                gameDifficulty = GameDifficulty.MEDIUM;
            }
            else
            {
                gameDifficulty = GameDifficulty.HARD;
            }

            OnGameStart(gameMode, gameDifficulty, GameLength.LONG);
        });
    }

    public void OnModeClicked(string mode)
    {
        modeText1.text = mode;
        startMenu.SetActive(false);
        difficultyMenu.SetActive(true);
        menuPath.Push(startMenu);
    }

    public void OnDifficultyClicked(string mode, string difficulty)
    {
        modeText2.text = mode;
        difficultyText.text = difficulty;

        startMenu.SetActive(false);
        difficultyMenu.SetActive(false);
        gameLengthMenu.SetActive(true);

        menuPath.Push(difficultyMenu);
    }

    public void OnBackClicked()
    {
        startMenu.SetActive(false);
        difficultyMenu.SetActive(false);
        gameLengthMenu.SetActive(false);

        GameObject lastMenu = menuPath.Pop();
        lastMenu.SetActive(true);

        Debug.Log(lastMenu.name);
    }

    public void OnGameStart(GameMode mode, GameDifficulty difficulty, GameLength length)
    {
        GameManager.Instance.selectedGame = new SelectedGameData
        {
            GameMode = mode,
            GameDifficulty = difficulty,
            GameLength = length
        };
        SceneManager.LoadScene("Scene");
    }
}
