using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    #region Private Field
    private bool m_Transitioning;

    
    /////Players Input useful to disable on scene transitions
    //private List<InputComponenet> m_playerInputs = new List<InputComponenet>();
    

    ///Scene Controller Instance
    private static SceneController instance;
    #endregion

    /// Constructor
    public static SceneController Instance {
        get {
            if (instance != null)
                return instance;
            instance = FindObjectOfType<SceneController>();
            if (instance != null)
                return instance;
            Create();
            return instance;
        }
    }
    public static SceneController Create()
    {
        GameObject sceneControllerGameObject = new GameObject("SceneController");
        instance = sceneControllerGameObject.AddComponent<SceneController>();
        return instance;
    }

    #region Unity-Method
    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

    }
    #endregion

    #region Public-Method
    /// <summary>
    /// Call Load Scene
    /// </summary>
    /// <param name="_targetScene">scene data container</param>
    public void TransitionToScene(SceneData _targetScene)
    {
        Instance.StartCoroutine(Transition(_targetScene));
    }
    #endregion

    #region Private-Method
    /// <summary>
    /// Called to start the loading async of scene
    /// </summary>
    /// <param name="_sceneData"></param>
    /// <returns></returns>
    protected IEnumerator Transition(SceneData _sceneData)
    {
        m_Transitioning = true;
        ///Start load Async
        yield return SceneManager.LoadSceneAsync(_sceneData.SceneName, _sceneData.LoadType);
        m_Transitioning = false;
    }
    #endregion
}