using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{
    public Button upgradeOffice;
    void Start()
    {
        upgradeOffice.onClick.AddListener(NextScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextScene()
    {
        SceneManager.LoadScene("OfficeMap2");
    }
}
