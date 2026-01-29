using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class StageGoal
{
    [Header("목표 정보")]
    public string goalName;
    
    [Header("활성화할 오브젝트")]
    public GameObject targetObject;

    [Header("배경")]
    public Sprite currentBackgroundSprite;
    public Sprite nextBackgroundSprite;
    
    [Header("증거 그룹")]
    [Tooltip("이 스테이지의 증거들을 담은 부모 오브젝트")]
    public GameObject clueGroupObject;
    
    [Header("필요한 증거")]
    public List<int> requiredClueIndexes = new List<int>();
    
    [HideInInspector]
    public bool isCompleted = false;

    /// <summary>
    /// 목표 달성에 필요한 모든 증거가 발견되었는지 확인
    /// </summary>
    public bool CheckRequirements(List<ClueData> clueList)
    {
        foreach (int index in requiredClueIndexes)
        {
            // 유효하지 않은 인덱스
            if (index < 0 || index >= clueList.Count)
            {
                Debug.LogWarning($"목표 '{goalName}'에 유효하지 않은 증거 인덱스가 있습니다: {index}");
                return false;
            }

            // 아직 발견되지 않은 증거
            if (!clueList[index].isFound)
            {
                return false;
            }
        }

        return true;
    }
}
public class EvidenceManager : MonoBehaviour
{
    [Header("증거 관리")]
    [SerializeField] private List<ClueData> clueList = new List<ClueData>();
    
    [Header("목표 관리")]
    [SerializeField] private List<StageGoal> stageGoalList = new List<StageGoal>();

    [Header("UI References")]
    [SerializeField] private GameObject popUp;
    [SerializeField] private Image popUpImage;
    [SerializeField] private Image backgroundImage;
    
    [Header("Settings")]
    [SerializeField] private float popUpDisplayDuration = 1.0f;
    [SerializeField] private float yOffset = 0f;
    
    private Coroutine currentPopUpCoroutine;
    private int completedGoalsCount = 0;
    
    void Start()
    {
        InitializeBackground();
        InitializeClueButtons();
        
        // 첫 번째 스테이지의 증거 그룹만 활성화
        for (int i = 0; i < stageGoalList.Count; i++)
        {
            if (stageGoalList[i].clueGroupObject != null)
            {
                stageGoalList[i].clueGroupObject.SetActive(i == 0);
            }
        }
        
        // NoteManager에 총 증거 개수 알려주기
        if (NoteManager.Instance != null)
        {
            NoteManager.Instance.SetTotalCluesRequired(clueList.Count);
            Debug.Log($"EvidenceManager: NoteManager에 증거 개수 전달 ({clueList.Count}개)");
        }
        else
        {
            Debug.LogWarning("NoteManager 인스턴스를 찾을 수 없습니다!");
        }
        
        ValidateSetup();
    }

    /// <summary>
    /// 초기 배경 설정
    /// </summary>
    private void InitializeBackground()
    {
        if (backgroundImage == null)
        {
            Debug.LogError("backgroundImage가 할당되지 않았습니다!");
            return;
        }

        if (stageGoalList.Count > 0 && stageGoalList[0].currentBackgroundSprite != null)
        {
            // 첫 번째 목표의 nextBackgroundSprite를 초기 배경으로 사용
            backgroundImage.sprite = stageGoalList[0].currentBackgroundSprite;
        }
        else
        {
            Debug.LogWarning("초기 배경 스프라이트가 설정되지 않았습니다.");
        }
    }

    /// <summary>
    /// 증거 버튼에 리스너 등록
    /// </summary>
    private void InitializeClueButtons()
    {
        for (int i = 0; i < clueList.Count; i++)
        {
            ClueData clue = clueList[i];
            
            if (clue.clueButton == null)
            {
                Debug.LogWarning($"증거 '{clue.cluename}'의 버튼이 할당되지 않았습니다.");
                continue;
            }

            // 기존 리스너 제거 후 등록 (중복 방지)
            clue.clueButton.onClick.RemoveAllListeners();
            
            // 클로저 문제 해결을 위해 로컬 변수 사용
            int index = i;
            clue.clueButton.onClick.AddListener(() => OnClickClue(index));
        }
    }

    /// <summary>
    /// 설정 검증 (디버그용)
    /// </summary>
    private void ValidateSetup()
    {
        // 증거 리스트 검증
        for (int i = 0; i < clueList.Count; i++)
        {
            if (clueList[i] == null)
            {
                Debug.LogError($"clueList[{i}]이 null입니다!");
            }
        }

        // 목표 리스트 검증
        foreach (var goal in stageGoalList)
        {
            foreach (int index in goal.requiredClueIndexes)
            {
                if (index < 0 || index >= clueList.Count)
                {
                    Debug.LogError($"목표 '{goal.goalName}'에 범위를 벗어난 증거 인덱스가 있습니다: {index}");
                }
            }
        }
    }
    /// <summary>
    /// 증거 버튼 클릭 처리
    /// </summary>
    private void OnClickClue(int clueIndex)
    {
        if (clueIndex < 0 || clueIndex >= clueList.Count)
        {
            Debug.LogError($"유효하지 않은 증거 인덱스: {clueIndex}");
            return;
        }

        ClueData clue = clueList[clueIndex];
        
        // 이미 발견한 증거는 무시
        if (clue.isFound)
        {
            return;
        }

        // 증거 발견 처리
        clue.isFound = true;
        clue.clueButton.interactable = false;
        Debug.Log($"증거 발견: {clue.cluename}");
        
        // 팝업 표시 후 목표 확인
        if (clue.popupImage != null)
        {
            StartCoroutine(ShowPopUpThenCheckGoals(clue.popupImage, clue));
        }
        else
        {
            // 팝업이 없으면 바로 목표 확인
            if (NoteManager.Instance != null)
            {
                NoteManager.Instance.RegisterClueFound(clue);
            }
            
            CheckGoals();
        }
    }
    
    /// <summary>
    /// 팝업 표시 후 목표 확인
    /// </summary>
    private IEnumerator ShowPopUpThenCheckGoals(Sprite popupSprite, ClueData clue)
    {
        // 팝업 표시
        yield return StartCoroutine(ShowPopUpCoroutine(popupSprite));
    
        if (NoteManager.Instance != null)
        {
            NoteManager.Instance.RegisterClueFound(clue);
            Debug.Log($"EvidenceManager: NoteManager에 증거 발견 알림 ({clue.cluename})");
        }
        else
        {
            Debug.LogWarning("NoteManager 인스턴스를 찾을 수 없습니다!");
        }
        
        // 팝업이 완전히 종료된 후 목표 확인
        CheckGoals();
    }
    
    /// <summary>
    /// 팝업 표시 코루틴
    /// </summary>
    private IEnumerator ShowPopUpCoroutine(Sprite popupSprite)
    {
        if (popUp == null || popUpImage == null)
        {
            Debug.LogWarning("팝업 UI가 설정되지 않았습니다.");
            yield break;
        }

        // 이전 팝업 코루틴 중단
        if (currentPopUpCoroutine != null)
        {
            StopCoroutine(currentPopUpCoroutine);
        }

        popUpImage.sprite = popupSprite;
        popUp.SetActive(true);
    
        yield return new WaitForSeconds(popUpDisplayDuration);
    
        popUp.SetActive(false);
        currentPopUpCoroutine = null;
    }
    
    /// <summary>
    /// 모든 목표 달성 여부 확인
    /// </summary>
    private void CheckGoals()
    {
        // 모든 목표가 완료되었으면 체크 생략
        if (completedGoalsCount >= stageGoalList.Count)
        {
            return;
        }

        foreach (var goal in stageGoalList)
        {
            // 이미 완료된 목표는 건너뛰기
            if (goal.isCompleted)
            {
                continue;
            }

            // 목표 달성 조건 확인
            if (goal.CheckRequirements(clueList))
            {
                CompleteGoal(goal);
            }
        }
    }
    
    /// <summary>
    /// 목표 달성 처리
    /// </summary>
    private void CompleteGoal(StageGoal goal)
    {
        goal.isCompleted = true;
        completedGoalsCount++;
        
        Debug.Log($"목표 달성: {goal.goalName}");

        if (goal.targetObject != null)
        {
            goal.targetObject.SetActive(true);
        }

        if (goal.nextBackgroundSprite != null)
        {
            ChangeBackground(goal.nextBackgroundSprite);
        
            // 현재 스테이지 증거 그룹 비활성화
            if (goal.clueGroupObject != null)
            {
                goal.clueGroupObject.SetActive(false);
            }
        
            // 다음 스테이지 증거 그룹 활성화
            int nextGoalIndex = stageGoalList.IndexOf(goal) + 1;
            if (nextGoalIndex < stageGoalList.Count)
            {
                var nextGoal = stageGoalList[nextGoalIndex];
                if (nextGoal.clueGroupObject != null)
                {
                    nextGoal.clueGroupObject.SetActive(true);
                }
            }
        }

        if (completedGoalsCount >= stageGoalList.Count)
        {
            OnAllGoalsCompleted();
        }
    }
    
    /// <summary>
    /// 배경 변경 처리
    /// </summary>
    private void ChangeBackground(Sprite nextSprite)
    {
        if (FadeManager.Instance != null)
        {
            FadeManager.Instance.ChangeBackgroundWithFade(nextSprite, yOffset);
            
            // backgroundImage도 동기화 (FadeManager와 같은 이미지를 사용하는 경우)
            if (backgroundImage != null)
            {
                // FadeManager의 페이드 시간만큼 대기 후 변경하거나
                // FadeManager에서 콜백을 받아서 변경하는 것이 더 정확함
                StartCoroutine(UpdateBackgroundAfterFade(nextSprite));
            }
        }
        else
        {
            Debug.LogError("FadeManager 인스턴스를 찾을 수 없습니다!");
            // FadeManager 없이도 동작하도록 폴백
            if (backgroundImage != null)
            {
                backgroundImage.sprite = nextSprite;
            }
        }
    }

    /// <summary>
    /// 페이드 완료 후 배경 이미지 업데이트
    /// </summary>
    private IEnumerator UpdateBackgroundAfterFade(Sprite nextSprite)
    {
        // FadeManager의 페이드 시간 + 약간의 여유
        yield return new WaitForSeconds(2.5f);
        
        if (backgroundImage != null)
        {
            backgroundImage.sprite = nextSprite;
        }
    }

    /// <summary>
    /// 팝업 표시
    /// </summary>
    private void ShowPopUp(Sprite popupSprite)
    {
        if (popUp == null || popUpImage == null)
        {
            Debug.LogWarning("팝업 UI가 설정되지 않았습니다.");
            return;
        }

        // 이전 팝업 코루틴 중단
        if (currentPopUpCoroutine != null)
        {
            StopCoroutine(currentPopUpCoroutine);
        }

        popUpImage.sprite = popupSprite;
        currentPopUpCoroutine = StartCoroutine(ShowPopUpCoroutine());
    }

    /// <summary>
    /// 팝업 표시 코루틴
    /// </summary>
    private IEnumerator ShowPopUpCoroutine()
    {
        popUp.SetActive(true);
        yield return new WaitForSeconds(popUpDisplayDuration);
        popUp.SetActive(false);
        
        currentPopUpCoroutine = null;
    }

    /// <summary>
    /// 모든 목표 완료 시 호출
    /// </summary>
    private void OnAllGoalsCompleted()
    {
        Debug.Log("모든 목표를 달성했습니다!");
        // 여기에 엔딩 처리, 다음 스테이지 이동 등 추가 가능
    }
}
