using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public enum SceneType
{
    GameScene,
    MenuScene,
    UiScene
}

[CreateAssetMenu(menuName = "Scene/SceneData")]
public class SceneData : ScriptableObject
{
    [SerializeField]
    private string m_sceneName;
    [Tooltip("you need to desable player input?")]
    [SerializeField]
    private bool m_disableInput;
    [Header("Action callled at the end of scene loaded")]
    [SerializeField]
    private UnityEvent m_OnSceneLoaded;
    [Header("Load Type of scene")]
    [SerializeField]
    private LoadSceneMode m_loadType = LoadSceneMode.Single;
    [SerializeField]
    private SceneType m_sceneType = SceneType.GameScene;

    public string SceneName { get { return m_sceneName; }set { m_sceneName = value; } }
    public bool DisableInput { get { return m_disableInput; } }
    public UnityEvent OnSceneLoaded { get { return m_OnSceneLoaded; } }
    public LoadSceneMode LoadType { get { return m_loadType; } }
    public SceneType SceneType { get { return m_sceneType; } }
}
