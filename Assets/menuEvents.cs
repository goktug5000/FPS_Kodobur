using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Threading.Tasks;


public class menuEvents : MonoBehaviour
{
    public static bool isGamePaused = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void RestartSectionn()
    {
        menuEvents.RestartSection();
    }
    public static void Resume()
    {
        MouseLook_fp.showMouse(false);

        Time.timeScale = 1f;
        isGamePaused = false;


    }

    public static void Pause()
    {
        MouseLook_fp.showMouse(true);

        Time.timeScale = 0f;
        isGamePaused = true;


    }

    public static void RestartSection()
    {
        Debug.Log("Restarting");
        Resume();
        LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    static async void LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            await Task.Yield();
        }
    }
}
