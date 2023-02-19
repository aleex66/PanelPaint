using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;


internal sealed class SaveSystem : MonoBehaviour
{
    private bool _isAlreadyLoaded = false;
    internal static List<Shape> shapesList = new List<Shape>();

    const string DRAWING_PATH = "/drawing";
    const string COUNT_PATH = "/drawing.count";

    public void SaveDrawing()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + DRAWING_PATH + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + COUNT_PATH + SceneManager.GetActiveScene().buildIndex;

        FileStream countStream = new FileStream(countPath, FileMode.Create);
        formatter.Serialize(countStream, shapesList.Count);
        countStream.Close();

        for (int i = 0; i < shapesList.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            ShapeData data2save = new ShapeData(shapesList[i]);

            formatter.Serialize(stream, data2save);
            stream.Close();
        }
        Debug.Log("Current drawing has been saved!");
    }

    public void LoadDrawing() //needs refactoring in terms of multiple loads in one try
    {
        if (!_isAlreadyLoaded)
        {
            DisplayManager dispManager = GetComponent<DisplayManager>();

            const string rectangleStringPattern = @"\W*((?i)rectangle(?-i))\W*";
            const string circleStringPattern = @"\W*((?i)circle(?-i))\W*";

            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + DRAWING_PATH + SceneManager.GetActiveScene().buildIndex;
            string countPath = Application.persistentDataPath + COUNT_PATH + SceneManager.GetActiveScene().buildIndex;
            int shapesCount = 0;

            if (File.Exists(countPath))
            {
                FileStream countStream = new FileStream(countPath, FileMode.Open);
                shapesCount = (int)formatter.Deserialize(countStream);
                countStream.Close();

                for (int i = 0; i < shapesCount; i++)
                {
                    if (File.Exists(path + i))
                    {
                        FileStream stream = new FileStream(path + i, FileMode.Open);
                        ShapeData data2load = (ShapeData)formatter.Deserialize(stream);
                        stream.Close();

                        string nameOfShape = data2load.ShapeType;
                        Vector3 positionOfShape = new Vector3(data2load.CurrentPos[0], data2load.CurrentPos[1], data2load.CurrentPos[2]);
                        Vector2 sizeOfShape = new Vector3(data2load.CurrentSize[0], data2load.CurrentSize[1]);

                        if (Regex.IsMatch(nameOfShape, rectangleStringPattern))
                        {
                            Shape reloadedShape = new Shape(gameObject, nameOfShape, positionOfShape, sizeOfShape, dispManager.RectangleShape);
                        }
                        else if (Regex.IsMatch(nameOfShape, circleStringPattern))
                        {
                            Shape reloadedShape = new Shape(gameObject, nameOfShape, positionOfShape, sizeOfShape, dispManager.CircleShape);
                        }
                    }
                    else
                        Debug.Log("Cannot load from " + path + i);
                }
            }
            else
            {
                Debug.Log("Path for shapes count not found " + countPath);
                Debug.Log("Cannot load the drawing");
            }
        }
        else
            Debug.Log("The drawing is already loaded, exit and enter application once again to load!");

        _isAlreadyLoaded = true;
    }
}
