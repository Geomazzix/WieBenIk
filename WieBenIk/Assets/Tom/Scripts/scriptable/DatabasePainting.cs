using UnityEngine;
using WieBenIk.Data;

[CreateAssetMenu(fileName = "DatabasePainting",  menuName = "DatabasePainting", order = 2)]
public class DatabasePainting : ScriptableObject
{
    [SerializeField]
    private Sprite _PaintingSprite;
    public Sprite PaintingSprite
    {
        get { return _PaintingSprite; }
        set { _PaintingSprite = value; }
    }


    [SerializeField]
    private EArtDirections _ArtDirection;
    public EArtDirections ArtDirection
    {
        get { return _ArtDirection; }
    }


    [SerializeField]
    public PaintingCharacteristic[] _PaintingCharacteristics;


    //Gets called from editorscript.
    public void UpdateProperties()
    {
        PaintingProperty[] _PaintingProperties = Resources.LoadAll<PaintingProperty>("Properties");
        int length = _PaintingProperties.Length;
        _PaintingCharacteristics = new PaintingCharacteristic[length];
        for (int i = 0; i < length; i++)
        {
           _PaintingCharacteristics[i]._PaintingProperty = _PaintingProperties[i];
        }
    }
}


[System.Serializable]
public struct PaintingCharacteristic
{
    public PaintingProperty _PaintingProperty;
    public bool _DoesContain;
}
