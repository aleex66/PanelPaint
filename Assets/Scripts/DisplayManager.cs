using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal sealed class Shape
{
    private GameObject _thisShape;
    internal GameObject ThisShape { get => _thisShape; }

    private RectTransform _imageRectTransform;
    internal RectTransform ImageRectTransform { get => _imageRectTransform; }

    public Shape(GameObject parent, string nameOfShape, int id, Sprite whichShape)
    {
        _thisShape = new GameObject(nameOfShape + id);
        _thisShape.transform.SetParent(parent.transform);

        _imageRectTransform = _thisShape.AddComponent<RectTransform>();
        _imageRectTransform.localScale = Vector2.one;
        _imageRectTransform.position = new Vector3(0f, 0f, 90f);
        _imageRectTransform.anchoredPosition = new Vector2(0f, 0f);
        _imageRectTransform.sizeDelta = new Vector2(50, 50);

        Image imageOnGameObject = _thisShape.AddComponent<Image>();
        imageOnGameObject.sprite = whichShape;

        _thisShape.AddComponent<MovementController>();
        ShapeController shapeController = _thisShape.AddComponent<ShapeController>();
        shapeController.ShapeControllerConstructor(_imageRectTransform, parent.GetComponent<Canvas>(), this);

        SaveSystem.shapesList.Add(this);
    }

    public Shape(GameObject parent, string nameOfShape, Vector3 currPos, Vector2 currSize, Sprite whichShape)
    {
        _thisShape = new GameObject(nameOfShape);
        _thisShape.transform.SetParent(parent.transform);

        _imageRectTransform = _thisShape.AddComponent<RectTransform>();
        _imageRectTransform.localScale = Vector2.one;
        _imageRectTransform.position = new Vector3(0f, 0f, 90f); ;
        _imageRectTransform.anchoredPosition = new Vector2(0f, 0f);
        _imageRectTransform.sizeDelta = new Vector2(50, 50);

        Image imageOnGameObject = _thisShape.AddComponent<Image>();
        imageOnGameObject.sprite = whichShape; 

        _thisShape.AddComponent<MovementController>();
        ShapeController shapeController = _thisShape.AddComponent<ShapeController>();

        shapeController.CurrentSize.Add(currSize.x);
        shapeController.CurrentSize.Add(currSize.y);

        shapeController.InitPosition = currPos;

        shapeController.ShapeControllerConstructor(_imageRectTransform, parent.GetComponent<Canvas>(), this);
        SaveSystem.shapesList.Add(this);
    }
}

internal sealed class DisplayManager : MonoBehaviour
{
    [SerializeField] private Canvas _background;
    [SerializeField] private GameObject _saveLoadMenu;
    [SerializeField] internal Sprite _rectangleShape;
    [SerializeField] internal Sprite _circleShape;

    internal Sprite RectangleShape { get => _rectangleShape; }
    internal Sprite CircleShape { get => _circleShape; }

    private DispalyEventManager _displayEventManager;

    private Dictionary<string, int> _shapesStats = new Dictionary<string, int>()
    {
        {"rectangles", 0},
        {"circles", 0}
    };

    private bool _showSaveLoadMenu = false;

    void Start()
    {
        _displayEventManager = GetComponent<DispalyEventManager>();
        _displayEventManager.On_R_clicked += DrawRectangle; 
        _displayEventManager.On_C_clicked += DrawCircle;
        _displayEventManager.On_ESC_clicked += ShowSaveLoadMenu;

        _saveLoadMenu.SetActive(_showSaveLoadMenu);
    }

    private void DrawRectangle()
    {
        Shape Rectangle = new Shape(gameObject, "rectangle", _shapesStats["rectangles"], _rectangleShape);
        updateShapesStats("rectangles");
    }

    private void DrawCircle()
    {
        Shape Circle = new Shape(gameObject, "circle", _shapesStats["circles"], _circleShape);
        updateShapesStats("circles");
    }

    private void updateShapesStats(string shapeName)
    {
        _shapesStats[shapeName] = _shapesStats[shapeName] + 1;
    }

    private void ShowSaveLoadMenu()
    {
        _showSaveLoadMenu = !_showSaveLoadMenu;
        _saveLoadMenu.SetActive(_showSaveLoadMenu);
    }

    private void OnDisable()
    {
        _displayEventManager.On_R_clicked -= DrawRectangle;
        _displayEventManager.On_C_clicked -= DrawCircle;
        _displayEventManager.On_ESC_clicked -= ShowSaveLoadMenu;
    }
}
