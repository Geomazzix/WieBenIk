using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PaintingProperty))]
public class DatabasePaintingProperty : Editor
{
    private SerializedProperty _TextProperty;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //Wrapper.
        GUILayout.BeginVertical("box");

        //Namebox.
        GUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("DatabaseProperty");
        GUILayout.EndVertical();

        //Properties
        GUILayout.BeginVertical("box");
        EditorGUILayout.PropertyField(_TextProperty);
        GUILayout.EndVertical();

        GUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }


    //Get all the properties.
    public void OnEnable()
    {
        _TextProperty = serializedObject.FindProperty("_PaintingPropertyName");
    }
}
