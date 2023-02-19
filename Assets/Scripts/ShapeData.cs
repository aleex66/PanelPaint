using UnityEngine;

[System.Serializable]
internal sealed class ShapeData
{
    private string _shapeType;
    private float[] _shapeCurrSize;
    private float[] _currPosition;

    internal string ShapeType { get => _shapeType; }
    internal float[] CurrentSize { get => _shapeCurrSize; }
    internal float[] CurrentPos { get => _currPosition; }

    public ShapeData(Shape shape)
    {
        ShapeController shapeControll = shape.ThisShape.GetComponent<ShapeController>();

        _shapeType = shape.ThisShape.name;

        _shapeCurrSize = new float[]
        {
            shapeControll.CurrentSize[0],
            shapeControll.CurrentSize[1]
        };

        Vector3 shapePos = shape.ImageRectTransform.transform.position;
        _currPosition = new float[]
        {
            shapePos.x, 
            shapePos.y, 
            shapePos.z
        };
    }
}
