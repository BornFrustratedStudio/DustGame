using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ScreenFader : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup m_faderCanvasGroump;
    private bool        m_IsFading;
    public float        fadeDuration = 1f;

    public static ScreenFader Instance = null;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else if(Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void FadeIn(System.Action OnFadeEnd)
    {
        if(m_IsFading)
            return;

        if(!m_faderCanvasGroump.gameObject.activeSelf)
        {
            m_faderCanvasGroump.alpha = 0;
            m_faderCanvasGroump.gameObject.SetActive(true);
        }

        StartCoroutine(Fade(1, m_faderCanvasGroump, OnFadeEnd));
    }   

    public void FadeIn()
    {
        FadeIn(null);
    }   

    public void FadeOut(System.Action OnFadeEnd)
    {
        if(m_IsFading)
            return;
        
        if(!m_faderCanvasGroump.gameObject.activeSelf)
        {
            m_faderCanvasGroump.alpha = 1;
            m_faderCanvasGroump.gameObject.SetActive(true);
        }

        StartCoroutine(Fade(0, m_faderCanvasGroump, OnFadeEnd));
    }   
    
    public void FadeOut()
    {
        FadeOut(null);
    }   

    protected IEnumerator Fade(float finalAlpha, CanvasGroup canvasGroup, System.Action OnFadeEnd)
    {
        m_IsFading = true;
        canvasGroup.blocksRaycasts = true;
        float fadeSpeed = Mathf.Abs(canvasGroup.alpha - finalAlpha) / fadeDuration;
        while (!Mathf.Approximately(canvasGroup.alpha, finalAlpha))
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, finalAlpha,
                fadeSpeed * Time.deltaTime);
            yield return null;
        }
        canvasGroup.alpha = finalAlpha;
        m_IsFading = false;
        canvasGroup.blocksRaycasts = false;

        if(OnFadeEnd != null)
            OnFadeEnd();
    }
}
