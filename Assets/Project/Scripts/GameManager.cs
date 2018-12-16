using UnityEngine;
using System.Collections.Generic;

public enum GameState
{
    Running,
    Stoped,
    Win
}

public class GameManager : MonoBehaviour
{
    #region Private-Field
    private GameState   m_gameState;
    private GameManager m_instance;
    private int         m_currentStage;
    [SerializeField]
    private bool        m_godMode;

    [SerializeField] 
    private GameMode    m_gameMode;

    [SerializeField]
    private SceneData   m_endGameScene;

    private float       m_score;
    #endregion

    #region Public-Field
    public GameState    GameState 
    {
        get { return m_gameState; }
        set { m_gameState = value; }
    }
    public static GameManager  Instance = null;
    #endregion

    #region Unity-Method
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }else if(Instance != this)
        {
            Destroy(this.gameObject);
        }
        
        if (m_gameMode != null)
        {
            m_gameMode = Instantiate (m_gameMode);
        }


    }

    private void OnEnable()
    {

        if (m_gameMode != null)
        {
            m_gameMode.Enable ();
        }
    }

    private void OnDisable()
    {
        
        if(m_gameMode != null)
        {
            m_gameMode.Disable();
        }
    }

    private void Start()
    {
        m_gameState = GameState.Stoped;


    }

    private void Update()
    {
        if (m_gameState != GameState.Running)
            return;

        if (m_gameMode != null && m_godMode != true)
            CheckEndGame (ref m_gameMode);
    }
    #endregion

    #region Pivate-Method
    private void OnStartGame()
    {
        m_gameState = GameState.Running;
    }

    private void OnGameWin()
    {
        m_gameState = GameState.Win;
    }

    private void OnGameOver()
    {
        m_gameState = GameState.Stoped;
    }
    #endregion

    #region Public-Method
    public void TogglePause ()
    {
        if (m_gameState == GameState.Stoped)
        {
            m_gameState = GameState.Running;
            return;
        }
        else
        {
            m_gameState = GameState.Stoped;
        }
    }

    public void ChangeGameMode (GameMode gameMode)
    {
        if (m_gameMode != null)
        {
            m_gameMode.Disable ();
        }
        m_gameMode = Instantiate (gameMode);
        m_gameMode.Enable ();
    }

    public void ThrowEndGame()
    {
        SceneController.Instance.TransitionToScene(m_endGameScene);
    }

    public void CheckEndGame(ref GameMode gameMode)
    {
        switch(gameMode.CheckCondiction())
        {
            case GameModeCondiction.GameWin:
                OnGameWin ();
                break;
            case GameModeCondiction.GameOver:
                OnGameOver ();
                break;
            case GameModeCondiction.Idle:
            default:
                break;
        }

    }
    #endregion
}

