using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class ClueData
{
    public string cluename;
    public Button clueButton;
    public Sprite popupImage;
    
    public Sprite noteImage;
    public string noteTitle;
    [TextArea(3, 10)]
    public string noteDescription;
    
    [HideInInspector] public bool isFound = false;
    
    public Sprite GetNoteImage()
    {
        return noteImage != null ? noteImage : popupImage;
    }
    
    public string GetNoteTitle()
    {
        return !string.IsNullOrEmpty(noteTitle) ? noteTitle : cluename;
    }
    
    public string GetNoteDescription()
    {
        if (!string.IsNullOrEmpty(noteDescription))
        {
            return noteDescription;
        }
        
        // 기본 텍스트
        return $"{cluename}에 대한 정보입니다.";
    }
}
