using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "NewItem")]
public class Item : ScriptableObject
{
    public GameObject itemPrefab;
    public string itemTag;
    public Sprite itemImage;
    public string itemName;
    public string itemDescription;
    public int itemUsePoints;
    public float itemTimer;
    public int itemPrice;
}