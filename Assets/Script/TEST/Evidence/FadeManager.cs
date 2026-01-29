using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance { get; private set; }

    [Header("UI")] 
    [SerializeField] private GameObject fadePanel;
    [SerializeField] private Image backgroundChangeImage;
    
    [Header("Settings")]
    [SerializeField] private float fadeTime = 1.0f;
    [SerializeField] private float delayBetweenFades = 0.5f;
    
    private CanvasGroup fadeCanvasGroup;
    private Coroutine currentFadeCoroutine;
    private bool isFading = false;

    private void Awake()
    {
        InitializeSingleton();
        InitializeCanvasGroup();
    }

    private void InitializeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("FadeManager 인스턴스 존재, 중복 제거");
            Destroy(gameObject);
        }
    }

    private void InitializeCanvasGroup()
    {
        if (fadePanel == null)
        {
            Debug.LogError("FadePanel이 할당되지 않았습니다");
            return;
        }
        
        fadeCanvasGroup = fadePanel.GetComponent<CanvasGroup>();
        if (fadeCanvasGroup == null)
        {
            fadeCanvasGroup = fadePanel.AddComponent<CanvasGroup>();
        }
        fadeCanvasGroup.alpha = 0.0f;
        fadeCanvasGroup.blocksRaycasts = false;
        fadePanel.SetActive(true);
    }
    
    public void ChangeBackgroundWithFade(Sprite nextSprite, float? yOffset = null)
    {
        //null체크
        if(nextSprite == null) return;
        if (backgroundChangeImage == null) return;
        
        //이미 페이드 중이라면 중단하고 새로 시작
        if (currentFadeCoroutine != null)
        {
            StartCoroutine(FadeFlowCoroutine(nextSprite));
            isFading = false;
        }
        
        Debug.Log($"페이드 시작: {nextSprite.name}");
        currentFadeCoroutine = StartCoroutine(FadeFlowCoroutine(nextSprite,  yOffset));
    }

    private IEnumerator FadeFlowCoroutine(Sprite nextSprite, float? yOffset = null)
    {
        isFading = true;
        fadeCanvasGroup.blocksRaycasts = true;
        
        // 페이드 인 (화면 어둡게)
        yield return StartCoroutine(FadeCoroutine(0f, 1f));
        
        // 배경 변경
        backgroundChangeImage.sprite = nextSprite;
        // Y 위치 조정 (옵션)
        if (yOffset.HasValue)
        {
            RectTransform rectTransform = backgroundChangeImage.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                Vector3 pos = rectTransform.anchoredPosition;
                pos.y = yOffset.Value;
                rectTransform.anchoredPosition = pos;
            }
        }
        yield return new WaitForSeconds(delayBetweenFades);
        
        // 페이드 아웃 (화면 밝게)
        yield return StartCoroutine(FadeCoroutine(1f, 0f));
        
        fadeCanvasGroup.blocksRaycasts = false;
        isFading = false;
        currentFadeCoroutine = null;
        
        Debug.Log("페이드 완료");
    }

    private IEnumerator FadeCoroutine(float start, float end)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float percent = Mathf.Clamp01(elapsedTime / fadeTime);
            
            // SmoothStep으로 부드러운 가속/감속
            float smoothPercent = Mathf.SmoothStep(0f, 1f, percent);
            fadeCanvasGroup.alpha = Mathf.Lerp(start, end, smoothPercent);
            
            yield return null;
        }
        
        // 정확한 최종 값 보장
        fadeCanvasGroup.alpha = end;
    }
}
