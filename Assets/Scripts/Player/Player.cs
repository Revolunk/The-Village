using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Attachements")]
    private Weather weather;
    public TMP_Text feelsLikeTemperatureUi;
    private PlayerControls playerControls;

    //MOVEMENT\\
    [Header("Movement")]
    private Transform mainCamera;
    private CharacterController characterController;
    [Space(10)]

    public Transform groundChecker;
    private float groundDistance = 0.1f;
    public LayerMask groundMask;
    public bool isGrounded;
    [Space(10)]

    private Vector2 movementInput;

    public bool isWalking;
    public float walkSpeed;
    [Space(5)]
    public bool isRunning;
    public float runSpeed;
    [Space(5)]
    public bool isSprinting;
    public float sprintSpeed;
    [Space(5)]
    public float turnSmoothTime;
    private float turnSmoothVelocity;
    [Space(10)]

    private Vector3 jumpVelocity;
    public float jumpHeight;
    [Space(10)]

    private Vector3 lastPosition;
    private float travelledDistanceFloat;
    public int travelledDistance;
    private float travelledThousand = 1000;

    //STATS\\
    [Header("Statistics")]
    public int health = 100;
    public float healthFloat;
    public int maxHealth = 100;
    public Slider healthSlider;
    [Space(5)]
    //public GameObject healthCircle;
    public int stamina = 100;
    public float staminaFloat;
    private float StaminaTemperatureMultiplier;
    public int maxStamina = 100;
    public Slider staminaSlider;
    [Space(5)]
    //public GameObject staminaCircle;
    public int mana = 100;
    public int maxMana = 100;
    public Slider manaSlider;
    [Space(5)]
    //public GameObject manaCircle;
    public int temporaryDrinked;
    [Space(10)]

    public int oneHandedWeapons = 0;
    private int maxOneHandedWeapons = 100;
    public int twoHandedWeapons = 0;
    private int maxTwoHandedWeapons = 100;
    public int axes = 0;
    private int maxAxes = 100;
    public int bows = 0;
    private int maxBows = 100;
    public int crossbows = 0;
    private int maxCrossbows = 100;
    public int shields = 0;
    private int maxShields = 100;
    [Space(5)]
    public int money;

    [Space(10)]
    public int feelsLikeTemperature;
    public int additionalTemperatures;

    void Awake()
    {
        playerControls = new PlayerControls();

        playerControls.Movement.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();

        playerControls.Movement.WalkRunSprint.performed += ctx =>
        {
            if (ctx.interaction is HoldInteraction)
            {
                isSprinting = true;
            }

            if (ctx.interaction is PressInteraction)
            {
                if (isWalking)
                {
                    isWalking = false;
                    isRunning = true;
                }
                else if (isRunning)
                {
                    isRunning = false;
                    isWalking = true;
                }
            }
        };

        playerControls.Movement.WalkRunSprint.canceled += ctx =>
        {
            if (ctx.interaction is HoldInteraction)
            {
                isSprinting = false;
            }
        };

        playerControls.HUD.Menu.performed += ctx =>
          {
              if (HUDManager.instance.isHalfPaused)
              {
                  HUDManager.instance.Resume();
              }
              else
              {
                  HUDManager.instance.HalfPause();
              }
          };
    }

    void OnEnable()
    {
        playerControls.Enable();
    }

    void Start()
    {
        weather = GameObject.Find("Weather").GetComponent<Weather>();
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main.transform;
        isWalking = true;
        healthFloat = health;
        staminaFloat = maxStamina;
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        Movement();
        TravelledDistance();
        Statistics();
    }

    void OnDisable()
    {
        playerControls.Disable();
    }

    void Movement()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);

        //MOVE\\
        if (HUDManager.instance.isHalfPaused)
        {
            isGrounded = false;

            jumpVelocity.y += Physics.gravity.y * Time.deltaTime;
            characterController.Move(jumpVelocity * Time.deltaTime);
            if (jumpVelocity.y < 0)
            {
                jumpVelocity.y = -2;
            }

            staminaFloat += 7f * Time.deltaTime * StaminaTemperatureMultiplier;
            stamina = (int)Mathf.Floor(staminaFloat);
            if (stamina == maxStamina)
            {
                staminaFloat = maxStamina;
            }
        }
        else
        {
            float horizontal = movementInput.x;
            float vertical = movementInput.y;

            Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

            if (isSprinting)
            {
                if (direction.magnitude >= 0.1)
                {
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;

                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                    transform.rotation = Quaternion.Euler(0, angle, 0);

                    Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                    characterController.Move(moveDirection.normalized * sprintSpeed * Time.deltaTime);

                    staminaFloat -= 7f * Time.deltaTime;
                    if (stamina == 0)
                    {
                        isSprinting = false;
                    }
                }
                else
                {
                    staminaFloat += 7f * Time.deltaTime * StaminaTemperatureMultiplier;
                }
                stamina = (int)Mathf.Floor(staminaFloat);
                if (stamina == maxStamina)
                {
                    staminaFloat = maxStamina;
                }
            }
            else
            {
                if (isWalking)
                {
                    if (direction.magnitude >= 0.1)
                    {
                        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;

                        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                        transform.rotation = Quaternion.Euler(0, angle, 0);

                        Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                        characterController.Move(moveDirection.normalized * walkSpeed * Time.deltaTime);
                    }

                    isRunning = false;
                    staminaFloat += 7f * Time.deltaTime * StaminaTemperatureMultiplier;
                }
                else if (isRunning)
                {
                    if (direction.magnitude >= 0.1)
                    {
                        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;

                        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                        transform.rotation = Quaternion.Euler(0, angle, 0);

                        Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                        characterController.Move(moveDirection.normalized * runSpeed * Time.deltaTime);

                        staminaFloat += 3f * Time.deltaTime * StaminaTemperatureMultiplier;
                    }
                    else
                    {
                        staminaFloat += 7f * Time.deltaTime * StaminaTemperatureMultiplier;
                    }
                    isWalking = false;
                }

                stamina = (int)Mathf.Floor(staminaFloat);
                if (stamina == maxStamina)
                {
                    staminaFloat = maxStamina;
                }
            }

            //JUMP\\
            jumpVelocity.y += Physics.gravity.y * Time.deltaTime;
            characterController.Move(jumpVelocity * Time.deltaTime);

            if (isGrounded && jumpVelocity.y < 0)
            {
                jumpVelocity.y = -2;
            }

            playerControls.Movement.Jump.performed += ctx =>
            {
                if (isGrounded && ctx.interaction is HoldInteraction)
                {
                    jumpVelocity.y = Mathf.Sqrt(jumpHeight * -2 * Physics.gravity.y * 2f);
                }
                else if (isGrounded && ctx.interaction is PressInteraction)
                {
                    jumpVelocity.y = Mathf.Sqrt(jumpHeight * -2 * Physics.gravity.y);
                }
            };
        }
    }

    void TravelledDistance()
    {
        if (isGrounded == true)
        {
            travelledDistanceFloat += Vector3.Distance(transform.position, lastPosition);
            travelledThousand -= Vector3.Distance(transform.position, lastPosition);
            lastPosition = transform.position;
        }
        else
        {
            lastPosition = transform.position;
        }
        travelledDistance = (int)Mathf.Floor(travelledDistanceFloat);
    }

    public void Statistics()
    {
        //HEALTH\\
        healthSlider.value = health;
        healthSlider.maxValue = maxHealth;
        //float healthPercent;
        //healthPercent = (float)health / 100;
        //healthCircle.GetComponent<Image>().fillAmount = healthPercent;
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        //STAMINA\\
        staminaSlider.value = stamina;
        staminaSlider.maxValue = maxStamina;
        //float staminaPercent;
        //staminaPercent = (float)stamina / 100;
        //staminaCircle.GetComponent<Image>().fillAmount = staminaPercent;
        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }

        if (travelledThousand < 0)
        {
            maxStamina++;
            travelledThousand = 1000;
        }

        //MANA\\
        manaSlider.value = mana;
        manaSlider.maxValue = maxMana;
        //float manaPercent;
        //manaPercent = (float)mana / 100;
        //manaCircle.GetComponent<Image>().fillAmount = manaPercent;
        if (mana > maxMana)
        {
            mana = maxMana;
        }

        //TEMPORARY\\
        if (weather.day > weather.currentDay)
        {
            temporaryDrinked = 0;
            weather.currentDay = weather.day;
        }
        if (temporaryDrinked == 6)
        {
            healthFloat = 10;
            staminaFloat = 10;
            HUDManager.instance.currentStaminaTime = 0;
            HUDManager.instance.currentManaTime = 0;
            mana = 10;
        }

        //TEMPERATURE\\
        feelsLikeTemperatureUi.text = "Feels Like       " + feelsLikeTemperature;
        feelsLikeTemperature = weather.temperature + additionalTemperatures;

        health = (int)Mathf.Floor(healthFloat);
        if (healthFloat > maxHealth)
        {
            healthFloat = maxHealth;
        }

        if (feelsLikeTemperature >= 15)
        {
            StaminaTemperatureMultiplier = 1;
        }
        if (feelsLikeTemperature >= 5 && feelsLikeTemperature <= 14)
        {
            StaminaTemperatureMultiplier = 0.7f;
        }
        if (feelsLikeTemperature >= 0 && feelsLikeTemperature <= 4)
        {
            StaminaTemperatureMultiplier = 0.3f;

            healthFloat -= 0.1f * Time.deltaTime;
        }
        if (feelsLikeTemperature < 0)
        {
            StaminaTemperatureMultiplier = 0.1f;

            healthFloat -= 0.5f * Time.deltaTime;
        }
    }
}
