using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovePlayerToScene : MonoBehaviour
{
    public int levelIndex;
    public GameObject Canvas;
    public GameObject Controller;
    public GameObject Camera;
    public GameObject m_MyGameObject;


    void OnTriggerEnter() {
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Single);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene
        SceneManager.MoveGameObjectToScene(m_MyGameObject, SceneManager.GetSceneByBuildIndex(levelIndex));
        SceneManager.MoveGameObjectToScene(Canvas, SceneManager.GetSceneByBuildIndex(levelIndex));
        SceneManager.MoveGameObjectToScene(Controller, SceneManager.GetSceneByBuildIndex(levelIndex));
        Camera.GetComponent<CameraScript>().ChangeCenter(Vector3.zero);
        SceneManager.MoveGameObjectToScene(Camera, SceneManager.GetSceneByBuildIndex(levelIndex));
        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
