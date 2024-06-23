using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuSceneLimitation: MonoBehaviour
{
    public GameObject gameMenu;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        CheckAndToggleGameMenu(SceneManager.GetActiveScene());
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckAndToggleGameMenu(scene);
    }

    void CheckAndToggleGameMenu(Scene scene)
    {
        if (scene.name == "OutsideTheCabin" || scene.name == "Cabin")
        {
            gameMenu.SetActive(true);
        }
        else
        {
            gameMenu.SetActive(false);
        }
    }
}
