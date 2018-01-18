using UnityEngine;
using System.Collections.Generic;
using WieBenIk.Data;
using System.Linq;

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
    public List<PaintingCharacteristic> _PaintingCharacteristics = new List<PaintingCharacteristic>();


    //Gets called from editorscript.
    public void UpdateProperties()
    {
        //Load the all the properties in
        PaintingProperty[] paintingProperties = Resources.LoadAll<PaintingProperty>("Properties");
        int paintingPropertiesCount = paintingProperties.Length;

        //Load the current properties in.
        List<PaintingProperty> currentPaintingProperties = new List<PaintingProperty>();
        int paintingCharacteristicsCount = _PaintingCharacteristics.Count;
        if (paintingCharacteristicsCount > 0)
        {
            for (int i = 0; i < paintingCharacteristicsCount; i++)
            {
                currentPaintingProperties.Add(_PaintingCharacteristics[i]._PaintingProperty);
            }
        }
        else
        {
            _PaintingCharacteristics = new List<PaintingCharacteristic>(paintingPropertiesCount);
        }


        //Add properties to the characteristics array.
        if (paintingCharacteristicsCount < paintingPropertiesCount)
        {
            //Loop through the paintingproperties to check seek for the missing property.
            for (int i = 0; i < paintingPropertiesCount; i++)
            {
                if(!currentPaintingProperties.Contains<PaintingProperty>(paintingProperties[i]))
                {
                    currentPaintingProperties.Add(paintingProperties[i]);
                }
            }
        }

        
        //Remove properties from the characteristics array.
        if (paintingCharacteristicsCount > paintingPropertiesCount)
        {
            //Loop through the paintingcharacteristicsarray to look for properties that are not supposed to be there.
            for (int i = 0; i < paintingCharacteristicsCount; i++)
            {
                if(!paintingProperties.Contains(currentPaintingProperties[i]))
                {
                    currentPaintingProperties.Remove(currentPaintingProperties[i]);
                }

                if (_PaintingCharacteristics[i]._PaintingProperty == null)
                {
                    _PaintingCharacteristics.Remove(_PaintingCharacteristics[i]);
                }
            }
        }


        //Reassign the array to the inspector.
        for (int i = 0; i < paintingPropertiesCount; i++)
        {
            //When the lenght doesnt match the listcount make sure to expand it.
            if(i >= _PaintingCharacteristics.Count)
            {
                PaintingCharacteristic newPaintingCharacteristic = new PaintingCharacteristic();
                newPaintingCharacteristic._PaintingProperty = currentPaintingProperties[i];
                _PaintingCharacteristics.Add(newPaintingCharacteristic);
                
            }
            else
            {
                PaintingCharacteristic currCharacteristic = _PaintingCharacteristics[i];
                currCharacteristic._PaintingProperty = currentPaintingProperties[i];
                _PaintingCharacteristics[i] = currCharacteristic;
            }
        }
    }
}


//One of the multiple paintingcharacteristics of the painting.
[System.Serializable]
public struct PaintingCharacteristic
{
    public PaintingProperty _PaintingProperty;
    public bool _DoesContain;
}
