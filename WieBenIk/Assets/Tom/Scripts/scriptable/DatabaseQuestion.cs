using UnityEngine;

[CreateAssetMenu(fileName = "DatabaseQuestion", menuName = "DatabaseQuestion", order = 1)]
public sealed class DatabaseQuestion : ScriptableObject 
{
    [SerializeField]
    private string _QuestionText;
    public string QuestionText
    {
        get { return _QuestionText; }
        set { _QuestionText = value; }
    }

    [SerializeField]
    private PaintingProperty _QuestionIsAbout;
    public PaintingProperty QuestionIsAbout
    {
        get { return _QuestionIsAbout; }
    }

    public int _SelectedPopupOption;
}
