using UnityEngine;
using System.Collections.Generic;
using System.Linq;
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
        //Get all the properties.
        List<PaintingProperty> paintingProperties = Resources.LoadAll<PaintingProperty>("Properties").ToList();
        List<PaintingProperty> paintingCharacteristics = new List<PaintingProperty>();

        //get the current characteristics.
        int length = _PaintingCharacteristics.Length;
        for (int i = 0; i < length; i++)
        {
            paintingCharacteristics.Add(_PaintingCharacteristics[i]._PaintingProperty);
        }


        //Check if the list contains the new properties if not add them.
        int count = paintingProperties.Count;
        for (int i = 0; i < count; i++)
        {
            if(!paintingCharacteristics.Contains<PaintingProperty>(paintingProperties[i]))
            {
                paintingCharacteristics.Add(paintingProperties[i]);
            }
        }


        //Check if the list contians any unnecessary properties, if so remove them.
        int count2 = paintingCharacteristics.Count;
        for (int i = 0; i < count2; i++)
        {
            if(!paintingProperties.Contains<PaintingProperty>(paintingCharacteristics[i]))
            {
                paintingCharacteristics.Remove(paintingCharacteristics[i]);
            }
        }


        //Reassign the new array.
        for (int i = 0; i < length; i++)
        {
            _PaintingCharacteristics[0]._PaintingProperty = paintingCharacteristics[i];
        }
    }
}


[System.Serializable]
public struct PaintingCharacteristic
{
    public PaintingProperty _PaintingProperty;
    public bool _DoesContain;
}
