using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;
using TMPro;

public class DoorOpener : MonoBehaviour
{
    [Header("Attachements")]
    public Canvas floatingText;
    public TMP_Text openCloseText;
    private GameObject player;
    private PlayerControls playerControls;

    [Header("States")]
    public bool isOpen;

    private bool canUse;
    private Transform hittedObject;
    private float playerDistance;

    void OnEnable()
    {
        playerControls.Enable();
    }

    void Awake()
    {
        playerControls = new PlayerControls();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        transform.localRotation = Quaternion.Euler(0, 0, 0);
        isOpen = false;
        openCloseText.text = "Open";
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            hittedObject = hit.transform;
        }

        playerDistance = Vector3.Distance(transform.position, player.transform.position);

        if (gameObject.transform == hittedObject && playerDistance < 2)
        {
            floatingText.gameObject.SetActive(true);
            canUse = true;
            Feadback();
        }
        else
        {
            floatingText.gameObject.SetActive(false);
            canUse = false;
        }

        if (isOpen)
        {
            openCloseText.text = "Close";
        }
        else
        {
            openCloseText.text = "Open";
        }
    }

    void OnDisable()
    {
        playerControls.Disable();
    }

    void Feadback()
    {
        playerControls.Actions.Use.performed += ctx =>
        {
            if (ctx.interaction is PressInteraction)
            {
                OpenClose();
            }
        };
    }

    void OpenClose()
    {
        if (canUse)
        {
            if (isOpen)
            {
                gameObject.GetComponent<Animator>().Play("CloseDoor");
            }
            else
            {
                gameObject.GetComponent<Animator>().Play("OpenDoor");
            }
        }
        else
        {
            return;
        }
    }

    void IsOpen()
    {
        isOpen = true;
    }

    void IsColsed()
    {
        isOpen = false;
    }
}
