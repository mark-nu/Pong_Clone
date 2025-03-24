using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject singlesMenu;
    [SerializeField] private GameObject doublesMenu;
    [SerializeField] private GameObject gameLengthMenu;
    [SerializeField] private TMP_Text modeText;
    [SerializeField] private TMP_Text difficultyText;

    private void Awake()
    {
        startMenu.SetActive(true);
        singlesMenu.SetActive(false);
        doublesMenu.SetActive(false);
        gameLengthMenu.SetActive(false);
    }

    public void OnSinglesClick()
    {
        startMenu.SetActive(false);
        singlesMenu.SetActive(true);
    }

    public void OnDoublesClick()
    {
        startMenu.SetActive(false);
        doublesMenu.SetActive(true);
    }

    public void OnDifficultyClicked(string mode, string difficulty)
    {
        modeText.text = mode;
        difficultyText.text = difficulty;

        startMenu.SetActive(false);
        singlesMenu.SetActive(false);
        doublesMenu.SetActive(false);
        gameLengthMenu.SetActive(true);
    }

    public void OnBackClicked(string menu)
    {

    }

    public void OnGameStart(GameManager.GameMode mode, GameManager.GameDifficulty difficulty, GameManager.GameLength length)
    {
        SceneManager.LoadScene("Scene");
    }
}
