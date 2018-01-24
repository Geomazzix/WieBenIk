using UnityEngine;
using TMPro;

public sealed class QuestionTextDisplay : MonoBehaviour 
{
    [SerializeField]
    private TextMeshProUGUI _TextMeshText;

    //Updates the questiontext.
    public void UpdateQuestionDisplay(string questionText)
    {
        _TextMeshText.text = questionText;
    }
}
