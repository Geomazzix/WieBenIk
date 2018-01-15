using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DatabaseQuestion))]
public class DatabaseQuestionInspector : Editor
{
    private SerializedProperty _QuestionPaintingProperty;
    private SerializedProperty _QuestionText;
    private SerializedProperty _QuestionIndex;

    private string[] _PaintingPropertyNames;
    private string _PaintingPropertyArrayLabel = "QuestionType";


    public override void OnInspectorGUI()
    {
        //Update the values.
        serializedObject.Update();

        GUILayout.BeginVertical("box");

        GUILayout.BeginVertical("Box");

        EditorGUILayout.LabelField("DatabaseQuestion");
        GUILayout.EndVertical();


        //Wrap the inspector around this box.
        GUILayout.BeginVertical("Box");


        //The text of the question.
        EditorGUILayout.PropertyField(_QuestionText);


        //Get the painting properties.
        PaintingProperty[] paintingProperties = Resources.LoadAll<PaintingProperty>("Properties");
        int propertiesLength = paintingProperties.Length;
        _PaintingPropertyNames = new string[propertiesLength];
        for (int i = 0; i < propertiesLength; i++)
        {
            _PaintingPropertyNames[i] = paintingProperties[i].PaintingPropertyName;
        }

        //Make sure the user can't make a question without a property.
        if (_PaintingPropertyNames != null)
        {
            int lastSelectedIndex = _QuestionIndex.intValue;

            //Create the popup menu.
            _QuestionIndex.intValue = EditorGUILayout.Popup(_PaintingPropertyArrayLabel, _QuestionIndex.intValue, _PaintingPropertyNames);

            if(_QuestionIndex.intValue == lastSelectedIndex)
            {
                _QuestionPaintingProperty.objectReferenceValue = paintingProperties[_QuestionIndex.intValue];
            }
        }
        GUILayout.EndVertical();
        GUILayout.EndVertical();

        //Make sure to apply the value changes.
        serializedObject.ApplyModifiedProperties();
    }


    //When the object gets selected.
    public void OnEnable()
    {
        _QuestionText = serializedObject.FindProperty("_QuestionText");
        _QuestionPaintingProperty = serializedObject.FindProperty("_QuestionIsAbout");
        _QuestionIndex = serializedObject.FindProperty("_SelectedPopupOption");
    }
}
