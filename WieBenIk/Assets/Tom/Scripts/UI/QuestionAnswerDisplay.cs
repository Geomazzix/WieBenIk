using UnityEngine;
using TMPro;

public sealed class QuestionAnswerDisplay : MonoBehaviour 
{
    [SerializeField]
    private string _TrueText, _FalseText;

    [SerializeField]
    private Color _TrueColor, _FalseColor;

    [SerializeField]
    private TextMeshProUGUI _AnswerText;


    //Gets called when the player receives an answer of his question.
    public void DisplayQuestionAnswer(bool answerIsTrue)
    {
        if (answerIsTrue)
        {
            _AnswerText.color = _TrueColor;
            _AnswerText.text = _TrueText;
        }
        else
        {
            _AnswerText.color = _FalseColor;
            _AnswerText.text = _FalseText;
        }
    }
}
