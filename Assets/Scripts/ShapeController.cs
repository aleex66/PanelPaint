using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

internal sealed class ShapeController : MonoBehaviour
{
    private Canvas _canvas2draw;
    private Shape _myShape;

    private RectTransform _rectTransform;
    private MovementController _movementController;
    private List<float> _currentShapeSize = new List<float>();
    private Vector3 _initPosition = new Vector3(); //maybe I dont need it, just place it inside ShapeControllerConstructor

    internal List<float> CurrentSize { get => _currentShapeSize; }
    internal Vector3 InitPosition { set { _initPosition = value; } }

    internal void ShapeControllerConstructor(RectTransform rectTransform, Canvas background, Shape MyShape)
    { 
        _rectTransform = rectTransform;
        _canvas2draw = background;
        _myShape = MyShape;

        if (!_currentShapeSize.Any())
        {
            _currentShapeSize.Add(1.0f);  
            _currentShapeSize.Add(1.0f);
        }
        else
        {
            _rectTransform.localScale = new Vector3(_currentShapeSize[0], _currentShapeSize[1], 1);
            gameObject.transform.position = _initPosition;
        }
    }

    private void Start()
    {
        _movementController = GetComponent<MovementController>();
        _movementController.OnDragEvent += MoveShape;
        _movementController.OnRightClickEvent += RemoveShape;
        _movementController.OnLeftClickEvent += ChangeSize;
    }

    private void MoveShape(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas2draw.scaleFactor;
    }

    private void RemoveShape()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _movementController.OnDragEvent -= MoveShape;
        _movementController.OnRightClickEvent -= RemoveShape;
        _movementController.OnLeftClickEvent -= ChangeSize;

        SaveSystem.shapesList.Remove(_myShape);
    }

    private void ChangeSize()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _currentShapeSize[0] += 0.05f;
            _currentShapeSize[1] += 0.05f;
            _rectTransform.localScale = new Vector3(_currentShapeSize[0], _currentShapeSize[1], 1);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if ((_currentShapeSize[0] > 0.5f) && (_currentShapeSize[1] > 0.5f))
            {
                _currentShapeSize[0] -= 0.05f;
                _currentShapeSize[1] -= 0.05f;
                _rectTransform.localScale = new Vector3(_currentShapeSize[0], _currentShapeSize[1], 1);
            }
            else
                Debug.Log("Minumum size reached");
        }
    }
}
