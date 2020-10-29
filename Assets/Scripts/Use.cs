using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class Use : MonoBehaviour
{
    [Header("Attachements")]
    private GameObject player;
    private PlayerControls playerControls;

    public Canvas floatingText;
    public Image buttonClick;
    private bool isPressFeadback;
    [Space(10)]

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
            Feadback();
        }
        else
        {
            floatingText.gameObject.SetActive(false);
            isPressFeadback = false;
        }

        PressChecker();
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
                isPressFeadback = true;
            }
        };
    }

    void PressChecker()
    {
        if (isPressFeadback == true)
        {
            gameObject.GetComponent<Animator>().Play("ButtonPressFloatingUI");
            isPressFeadback = false;
        }
        else
        {
            return;
        }
    }
}
