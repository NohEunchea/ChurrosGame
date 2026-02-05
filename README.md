# ChurrosGame
명지전문대 졸작 

## 20260129
### 증거/노트 시스템

### 구조
- **EvidenceManager**: 증거 발견 및 목표 관리
- **NoteManager**: 노트 UI 및 증거 표시
- **FadeManager**: 배경 전환 효과

### 주요 기능
- 증거 발견 시 팝업 표시
- 노트에 증거 자동 등록
- 증거 순환 네비게이션
- 스테이지별 증거 그룹 관리
- 부드러운 페이드 효과

### 설정 방법
1. ClueData에 팝업/노트 이미지 및 텍스트 설정
2. StageGoal에 증거 인덱스 등록
3. 증거 그룹 오브젝트 할당
4. Animator 파라미터 설정 (isRightClick, isChange)

## 20260205
용량이 커서 Git LFS를 이용해서 올렸다.
푸시 후 로컬에 이미지, 오디오 파일이 포인터로만 남아있어서 
다시 유니티를 열었을 때 파일을 인식 못하는 현상 발생.
git lfs pull로 실제 파일을 다운받아 해결!


---

증거찾는 Scene 하이어라키 구성
![alt text](image.png)
