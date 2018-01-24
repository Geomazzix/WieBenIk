using UnityEngine;

[CreateAssetMenu(fileName = "PaintingProperty", menuName = "PaintingProperty", order = 0)]
public sealed class PaintingProperty : ScriptableObject 
{
    [SerializeField]
    private string _PaintingPropertyName;
    public string PaintingPropertyName
    {
        get { return _PaintingPropertyName; }
    }
}
