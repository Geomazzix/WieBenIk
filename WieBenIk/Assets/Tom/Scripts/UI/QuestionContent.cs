using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WieBenIk.UI
{
    public sealed class QuestionContent : MonoBehaviour
    {
        private QuestionDisplayController _QuestionDisplayController;
        private Toggle _ToggleComp;

        private DatabaseQuestion _Question;
        public DatabaseQuestion Question
        {
            get { return _Question; }
            set { _Question = value; _QuestionText.text = _Question.QuestionText;}
        }

        [SerializeField]
        private TextMeshProUGUI _QuestionText;


        //Set all the values the toggle and this class needs to function.
        private void Start()
        {
            _QuestionDisplayController = FindObjectOfType<QuestionDisplayController>();
            _ToggleComp = GetComponent<Toggle>();
            _ToggleComp.group = GetComponentInParent<ToggleGroup>();
        }


        //Update the main question display.
        public void SelectQuestion()
        {
            _QuestionDisplayController.SelectedQuestion = _Question;
        }
    }
}