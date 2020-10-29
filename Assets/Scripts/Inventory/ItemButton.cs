using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Interactions;

public class ItemButton : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Attachements")]
    public EventSystem eventSystem;
    public Tooltip tooltip;
    private PlayerControls playerControls;

    [Header("Item button")]
    public int buttonID;
    public GameObject button;
    private GameObject currentlySelected;
    [Space(5)]
    public Item thisItem;

    void OnEnable()
    {
        playerControls.Enable();
    }

    void Awake()
    {
        playerControls = new PlayerControls();

        playerControls.HUD.Accept.performed += ctx => Use();
        playerControls.HUD.Back.performed += ctx => ThrowOut();
    }

    void Start()
    {
        button = gameObject.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        currentlySelected = eventSystem.currentSelectedGameObject;

        if (button == currentlySelected)
        {
            //thisItem = GetThisItem();
            GetThisItem();

            if (thisItem != null)
            {
                tooltip.ShowTooltip();
                tooltip.UpdateName(thisItem.itemName);
                tooltip.UpdateDescription(thisItem.itemDescription);
                tooltip.UpdateValue(thisItem.itemPrice.ToString());
            }
            else
            {
                tooltip.HideTooltip();
                tooltip.UpdateName("");
                tooltip.UpdateDescription("");
                tooltip.UpdateValue("");
            }
        }
    }

    void OnDisable()
    {
        playerControls.Disable();
    }

    private Item GetThisItem()
    {
        for (int i = 0; i < HUDManager.instance.items.Count; i++)
        {
            if (buttonID == i)
            {
                thisItem = HUDManager.instance.items[i];
            }
        }
        return thisItem;
    }

    public void Use()
    {
        if (button == currentlySelected)
        {
            HUDManager.instance.UseItem(GetThisItem());
        }
    }

    public void ThrowOut()
    {
        if (button == currentlySelected)
        {
            HUDManager.instance.RemoveItem(GetThisItem());
        }

        //thisItem = GetThisItem();
        //if (thisItem != null)
        //{
        //    tooltip.ShowTooltip();
        //    tooltip.UpdateName(thisItem.itemName);
        //    tooltip.UpdateDescription(thisItem.itemDescription);
        //    tooltip.UpdateValue(thisItem.itemPrice.ToString());
        //}
        //else
        //{
        //    tooltip.HideTooltip();
        //    tooltip.UpdateName("");
        //    tooltip.UpdateDescription("");
        //    tooltip.UpdateValue("");
        //}

    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    GetThisItem();

    //    if (thisItem != null)
    //    {
    //        tooltip.ShowTooltip();
    //        tooltip.UpdateName(thisItem.itemName);
    //        tooltip.UpdateDescription(thisItem.itemDescription);
    //        tooltip.UpdateValue(thisItem.itemPrice.ToString());
    //    }
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    tooltip.HideTooltip();
    //    tooltip.UpdateName("");
    //    tooltip.UpdateDescription("");
    //    tooltip.UpdateValue("");
    //}
}
