using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
    public TMP_Text itemName;
    public TMP_Text itemDesciption;
    public TMP_Text itemValue;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowTooltip()
    {
        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public void UpdateName(string _itemName)
    {
        itemName.text = _itemName;
    }
    public void UpdateDescription(string _itemDescription)
    {
        itemDesciption.text = _itemDescription;
    }
    public void UpdateValue(string _itemValue)
    {
        itemValue.text = "Worth " + _itemValue + " coins";
    }
}
