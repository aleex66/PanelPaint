using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonsDisplay : MonoBehaviour
{
    
    public Buttons _button;
    public TextMeshProUGUI nameButton;
    public Image shapeImage;
    public TextMeshProUGUI buttonDescription;
    public void Start()
    {   
        nameButton.text = _button._name;
        shapeImage.sprite = _button.shape;
        buttonDescription.text = _button.shortDescription;
    }

}
