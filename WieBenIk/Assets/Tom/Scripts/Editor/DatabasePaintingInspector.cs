using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DatabasePainting))]
public class DatabasePaintingInspector : Editor
{
    private SerializedProperty _PaintingSprite;
    private SerializedProperty _PaintingProperties;
    private SerializedProperty _PaintingArtDirection;
    private DatabasePainting[] _Paintings;

    
    //Contains all the interaction with the inspector.
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //Wrapper
        GUILayout.BeginVertical("box");


        //name of script.
        GUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("Database Painting");
        GUILayout.EndVertical();


        //Draw the default inspector, only this time inside of a box.
        GUILayout.BeginVertical("Box");
        EditorGUILayout.PropertyField(_PaintingArtDirection, false);
        EditorGUILayout.PropertyField(_PaintingSprite, false);
        EditorGUILayout.PropertyField(_PaintingProperties, true);
        GUILayout.EndVertical();

        //Draw the updatebutton.
        GUILayout.BeginVertical("Box");
        DrawUpdatePaintingButton();
        GUILayout.EndVertical();

        GUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }


    //Draws the update button in the inspector.
    private void DrawUpdatePaintingButton()
    {
        _Paintings = Resources.LoadAll<DatabasePainting>("Paintings");
        int paintingsLength = _Paintings.Length;

        //When the button is pressed, update ALL questions.
        if (GUILayout.Button("Update paintings."))
        {
            //Set the array length.
            for (int i = 0; i < paintingsLength; i++)
            {
                _Paintings[i].UpdateProperties();
            };
        }
    }


    //Sets all the property fields when selected.
    public void OnEnable()
    {
        _PaintingArtDirection = serializedObject.FindProperty("_ArtDirection");
        _PaintingSprite = serializedObject.FindProperty("_PaintingSprite");
        _PaintingProperties = serializedObject.FindProperty("_PaintingCharacteristics");
    }
}
