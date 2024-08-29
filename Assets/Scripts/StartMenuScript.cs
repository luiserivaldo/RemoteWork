using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Start Screen Buttons")]
    public Button startGameButton;
    public Button exitButton;
    void Start()
    {
        startGameButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(QuitGame);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("OfficeMap1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
