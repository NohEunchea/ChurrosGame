using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance { get; private set; }
    
    [Header("노트 UI")]
    [SerializeField] private GameObject notePanel;
    [SerializeField] private GameObject noteOpenCloseButton;
    
    [Header("증거 표시 UI")]
    [SerializeField] private GameObject clueDisplayPanel;
    [SerializeField] private Image clueImage;
    [SerializeField] private TextMeshProUGUI clueTitle;
    [SerializeField] private TextMeshProUGUI clueContent;
    
    [Header("네비게이션")]
    [SerializeField] private Image nextNavButton;
    
    [Header("애니메이션")]
    [SerializeField] private Animator noteAnimator;
    [SerializeField] private Animator noteBtnAnimator;
    
    [Header("설정")]
    [SerializeField] private float noteBtnAnimDuration = 0.7f;
    [SerializeField] private float navAnimDuration = 1.0f;
    
    // 내부 변수
    private List<ClueData> foundClues = new List<ClueData>();
    private int currentClueIndex = 0;
    private int totalCluesRequired = 0;
    private bool isNoteOpen = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        InitializeNote();
        SetupEventTriggers();
    }

    /// <summary>
    /// EventTrigger 설정
    /// </summary>
    private void SetupEventTriggers()
    {
        if (nextNavButton == null)
        {
            Debug.LogWarning("NoteManager: nextNavButton이 할당되지 않았습니다.");
            return;
        }
    
        if (nextNavButton.gameObject == null)
        {
            Debug.LogError("NoteManager: nextNavButton의 GameObject가 null입니다!");
            return;
        }
        
        EventTrigger nextNavButtonTrigger = nextNavButton.GetComponent<EventTrigger>();
        
        if (nextNavButtonTrigger == null)
        {
            nextNavButtonTrigger = nextNavButton.gameObject.AddComponent<EventTrigger>();
        }
            
        // 기존 이벤트 제거
        nextNavButtonTrigger.triggers.Clear();
            
        // PointerEnter
        EventTrigger.Entry enterNext = new EventTrigger.Entry();
        enterNext.eventID = EventTriggerType.PointerEnter;
        enterNext.callback.AddListener((data) => { OnNextNavImageHover(); });
        nextNavButtonTrigger.triggers.Add(enterNext);
            
        // PointerExit
        EventTrigger.Entry exitNext = new EventTrigger.Entry();
        exitNext.eventID = EventTriggerType.PointerExit;
        exitNext.callback.AddListener((data) => { OnNextNavImageHoverExit(); });
        nextNavButtonTrigger.triggers.Add(exitNext);
            
        // PointerClick
        EventTrigger.Entry clickNext = new EventTrigger.Entry();
        clickNext.eventID = EventTriggerType.PointerClick;
        clickNext.callback.AddListener((data) => { OnNextNavImageClick(); });
        nextNavButtonTrigger.triggers.Add(clickNext);
        
        Debug.Log("NoteManager: EventTrigger 설정 완료");
    }
    
    /// <summary>
    /// 노트 초기화
    /// </summary>
    private void InitializeNote()
    {
        notePanel.SetActive(false);
        noteOpenCloseButton.SetActive(true);
        clueDisplayPanel.SetActive(false);
        
        // 네비게이션 버튼 초기 상태 (증거 없으면 숨김)
        UpdateNavigationButton();
    }

    /// <summary>
    /// 필요한 증거 개수 설정 (EvidenceManager가 호출)
    /// </summary>
    public void SetTotalCluesRequired(int count)
    {
        totalCluesRequired = count;
        Debug.Log($"NoteManager: 필요한 증거 개수 = {totalCluesRequired}");
    }
    
    // ========== 노트 열기/닫기 (토글) ==========
    
    /// <summary>
    /// 노트 토글 - noteOpenCloseButton에 연결
    /// </summary>
    public void ToggleNote()
    {
        if (isNoteOpen)
        {
            CloseNote();
        }
        else
        {
            OpenNote();
        }
    }
    
    /// <summary>
    /// 노트 열기
    /// </summary>
    private void OpenNote()
    {
        notePanel.SetActive(true);
        isNoteOpen = true;
        
        Debug.Log("노트 열림");
        
        if (foundClues.Count > 0)
        {
            ShowClue(currentClueIndex);
        }
        else
        {
            ShowEmptyNote();
        }
    }

    /// <summary>
    /// 노트 닫기
    /// </summary>
    private void CloseNote()
    {
        notePanel.SetActive(false);
        isNoteOpen = false;
        
        Debug.Log("노트 닫힘");
        
        HideClue();
    }
    
    /// <summary>
    /// 증거 없을 때 표시
    /// </summary>
    private void ShowEmptyNote()
    {
        clueDisplayPanel.SetActive(true);
        clueTitle.text = "";
        clueContent.text = "증거를 찾아야겠군";
    }
    
    // ========== 증거 등록 (EvidenceManager가 호출) ==========
    
    /// <summary>
    /// 증거 발견 등록
    /// </summary>
    public void RegisterClueFound(ClueData clue)
    {
        if (clue == null)
        {
            Debug.LogError("등록하려는 ClueData가 null입니다!");
            return;
        }

        if (foundClues.Contains(clue))
        {
            Debug.LogWarning($"이미 등록된 증거: {clue.cluename}");
            return;
        }
        
        foundClues.Add(clue);
        Debug.Log($"노트에 증거 추가: {clue.cluename} ({foundClues.Count}/{totalCluesRequired})");

        // 노트 버튼 알림 애니메이션
        StartCoroutine(PlayNoteBtnAnimation());
        
        // ✅ 추가: 네비게이션 버튼 업데이트
        UpdateNavigationButton();
        
        CheckAllCluesFound();
    }

    /// <summary>
    /// 노트 버튼 알림 애니메이션
    /// </summary>
    private IEnumerator PlayNoteBtnAnimation()
    {
        if (noteBtnAnimator != null)
        {
            noteBtnAnimator.SetBool("isChange", true);
            yield return new WaitForSeconds(noteBtnAnimDuration);
            noteBtnAnimator.SetBool("isChange", false);
        }
    }
    
    /// <summary>
    /// 모든 증거 발견 체크
    /// </summary>
    private void CheckAllCluesFound()
    {
        if (totalCluesRequired > 0 && foundClues.Count >= totalCluesRequired)
        {
            Debug.Log("🎉 모든 증거를 다 찾았습니다!");
        }
    }

    // ========== 증거 표시 (노트 안) ==========
    
    /// <summary>
    /// 특정 증거 표시
    /// </summary>
    private void ShowClue(int index)
    {
        if (foundClues.Count == 0 || index < 0 || index >= foundClues.Count)
        {
            ShowEmptyNote();
            return;
        }
        
        currentClueIndex = index;
        ClueData clue = foundClues[index];
        
        clueDisplayPanel.SetActive(true);
        
        // 노트 이미지 표시
        if (clue.noteImage != null && clueImage != null)
        {
            clueImage.sprite = clue.noteImage;
            clueImage.enabled = true; // 다시 표시
        }
        else
        {
            Debug.LogWarning($"증거 '{clue.cluename}'의 noteImage가 없습니다!");
            if (clueImage != null)
            {
                clueImage.enabled = false;
            }
        }
    
        // 노트 제목 표시
        if (clueTitle != null)
        {
            clueTitle.text = !string.IsNullOrEmpty(clue.noteTitle) 
                ? clue.noteTitle 
                : clue.cluename;
        }
    
        // 노트 설명 표시
        if (clueContent != null)
        {
            clueContent.text = !string.IsNullOrEmpty(clue.noteDescription) 
                ? clue.noteDescription 
                : $"{clue.cluename}에 대한 정보";
        }
        
        Debug.Log($"증거 표시: {clue.cluename} ({currentClueIndex + 1}/{foundClues.Count})");
    }
    
    /// <summary>
    /// 증거 숨기기
    /// </summary>
    private void HideClue()
    {
        clueDisplayPanel.SetActive(false);
        clueTitle.text = "";
        clueContent.text = "";
    }

    // ========== 증거 네비게이션 ==========
    /// <summary>
    /// Next 이미지 클릭
    /// </summary>
    private void OnNextNavImageClick()
    {
        if (foundClues.Count <= 1) return;
    
        Debug.Log($"다음 증거로 이동 시작");
    
        // 애니메이션 중에는 클릭 방지
        if (nextNavButton != null)
        {
            nextNavButton.raycastTarget = false;
        }
        StartCoroutine(PlayNextClickAnimation());
    }
    
    /// <summary>
    /// Next 클릭 애니메이션
    /// </summary>
    private IEnumerator PlayNextClickAnimation()
    {
        // 1. 현재 증거 내용 숨기기
        HideClueContent();
    
        // 2. 애니메이션 시작
        if (noteAnimator != null)
        {
            noteAnimator.SetBool("isNext", true);
        }
    
        // 3. 애니메이션 재생 중 대기
        yield return new WaitForSeconds(navAnimDuration);
    
        // 4. 애니메이션 종료
        if (noteAnimator != null)
        {
            noteAnimator.SetBool("isNext", false);
        }
    
        // 5. 다음 증거로 인덱스 변경
        currentClueIndex = (currentClueIndex + 1) % foundClues.Count;
    
        Debug.Log($"다음 증거로 이동 완료: {currentClueIndex}");
    
        // 6. 새로운 증거 표시
        ShowClue(currentClueIndex);
    
        // 7. 클릭 다시 활성화
        if (nextNavButton != null)
        {
            nextNavButton.raycastTarget = true;
        }
    }

    /// <summary>
    /// 증거 내용만 숨기기 (패널은 유지)
    /// </summary>
    private void HideClueContent()
    {
        if (clueImage != null)
        {
            clueImage.enabled = false;
        }
    
        if (clueTitle != null)
        {
            clueTitle.text = "";
        }
    
        if (clueContent != null)
        {
            clueContent.text = "";
        }
    
        Debug.Log("증거 내용 숨김 (애니메이션용)");
    }

    /// <summary>
    /// Next 이미지 호버
    /// </summary>
    private void OnNextNavImageHover()
    {
        if (noteAnimator != null && foundClues.Count > 1)
        {
            noteAnimator.SetBool("isRightClick", true);
        }
    }

    /// <summary>
    /// Next 이미지 호버 종료
    /// </summary>
    private void OnNextNavImageHoverExit()
    {
        if (noteAnimator != null)
        {
            noteAnimator.SetBool("isRightClick", false);
        }
    }
    
    /// <summary>
    /// 네비게이션 버튼 업데이트
    /// </summary>
    private void UpdateNavigationButton()
    {
        if (nextNavButton != null)
        {
            // 증거가 2개 이상일 때만 표시
            bool hasMultipleClues = foundClues.Count > 1;
            nextNavButton.gameObject.SetActive(hasMultipleClues);
            
            Debug.Log($"네비게이션 버튼: {(hasMultipleClues ? "표시" : "숨김")}");
        }
    }
}
