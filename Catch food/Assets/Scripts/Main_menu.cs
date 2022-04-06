using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_menu : MonoBehaviour
{
    public GameObject panel;

    //Загрузка сцен для кнопки Start, Menu и Restart
    public void PlayScene(int _sceneNumber)
    {
        SceneManager.LoadScene(_sceneNumber);
    }

    public void ExitPause()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
    }
 
}
