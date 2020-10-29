using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class HUDManager : MonoBehaviour
{
    [Header("Attachements")]
    public static HUDManager instance;
    public GameObject inventoryMenu;
    public Player player;
    public CinemachineFreeLook cinemachineFreeLook;

    [Header("States of the game")]
    public bool isHalfPaused;

    //INVENTORY\\
    [Header("Inventory")]
    public List<Item> items = new List<Item>();
    public List<int> itemAmount = new List<int>();
    public GameObject[] slots;
    [Space(10)]

    public ItemButton thisButton;
    public ItemButton[] itemButtons;
    public GameObject firstItemSelected;

    //STATS\\
    [Header("Statistics")]
    public TMP_Text healthPoints;
    public TMP_Text staminaPoints;
    public TMP_Text manaPoints;
    public TMP_Text oneHandedWeaponsPercent;
    public TMP_Text twoHandedWeaponsPercent;
    public TMP_Text axesPercent;
    public TMP_Text bowsPercent;
    public TMP_Text crossbowsPercent;
    public TMP_Text shieldsPercent;
    [Space(10)]

    public Slider temporaryStaminaSlider;
    public Slider temporaryManaSlider;
    public Slider temporaryFocusSlider;
    [Space(5)]
    public float currentStaminaTime;
    public bool staminaTimerIsCountingDown;
    private int standardStaminaPoints;
    [Space(5)]
    public float currentManaTime;
    public bool manaTimerIsCountingDown;
    private int standardManaPoints;
    [Space(5)]
    public float currentFocusTime;
    public bool focusTimerIsCountingDown;
    private int temporaryFocusPoints;
    [Space(5)]
    public float currentNorthearthBoozeTime;
    public bool NorthearthBoozeTimerIsCountingDown;
    [Space(5)]
    private bool hasDrinkedTemporaryStamina;
    private bool hasDrinkedTemporaryMana;
    private bool hasDrinkedTemporaryFocus;
    private bool hasDrinkedNorthearthBooze;

    void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        //Screen.SetResolution(1920, 1080, true);

        cinemachineFreeLook.m_XAxis.m_MaxSpeed = 200;
        cinemachineFreeLook.m_YAxis.m_MaxSpeed = 1;
        //cinemachineFreeLook.m_YAxisRecentering.m_enabled = true;
        //cinemachineFreeLook.m_RecenterToTargetHeading.m_enabled = true;
        inventoryMenu.gameObject.SetActive(false);

        DisplayItems();
    }

    void Update()
    {
        if (isHalfPaused == true)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        StatisticsControl();
        Timers();
    }

    public void Resume()
    {
        isHalfPaused = false;
        //cinemachineFreeLook.m_XAxis.m_MaxSpeed = 200;
        //cinemachineFreeLook.m_YAxis.m_MaxSpeed = 1;
        //cinemachineFreeLook.m_YAxisRecentering.m_enabled = true;
        //cinemachineFreeLook.m_RecenterToTargetHeading.m_enabled = true;
        inventoryMenu.gameObject.SetActive(false);
        //Time.timeScale = 1;
    }

    public GameObject cinemachine;

    public void HalfPause()
    {
        isHalfPaused = true;
        //cinemachineFreeLook.m_XAxis.m_MaxSpeed = 0;
        //cinemachineFreeLook.m_YAxis.m_MaxSpeed = 0;
        //cinemachineFreeLook.m_YAxisRecentering.m_enabled = false;
        //cinemachineFreeLook.m_RecenterToTargetHeading.m_enabled = false;
        inventoryMenu.gameObject.SetActive(true);
        //Time.timeScale = 0;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstItemSelected);
    }

    void DisplayItems()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
            {
                slots[i].transform.GetChild(0).gameObject.SetActive(true);
                slots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 1);
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].itemImage;

                slots[i].transform.GetChild(1).GetComponent<TMP_Text>().color = new Color(1, 1, 1, 1);
                slots[i].transform.GetChild(1).GetComponent<TMP_Text>().text = itemAmount[i].ToString();
            }
            else
            {
                slots[i].transform.GetChild(0).gameObject.SetActive(false);
                slots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, 0);
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;

                slots[i].transform.GetChild(1).GetComponent<TMP_Text>().color = new Color(1, 1, 1, 0);
                slots[i].transform.GetChild(1).GetComponent<TMP_Text>().text = null;
            }
        }
    }

    public void AddItem(Item _item)
    {
        if (!items.Contains(_item))
        {
            items.Add(_item);
            itemAmount.Add(1);
            if (_item.itemTag == "Money")
            {
                player.money++;
            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (_item == items[i])
                {
                    itemAmount[i]++;
                    if (_item.itemTag == "Money")
                    {
                        player.money++;
                    }
                }
            }
        }
        DisplayItems();
    }

    public void RemoveItem(Item _item)
    {
        Vector3 dropPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        if (items.Contains(_item))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (_item == items[i])
                {
                    Instantiate(_item.itemPrefab, dropPosition + player.transform.forward * 0.5f, Quaternion.identity);
                    itemAmount[i]--;
                    if (_item.itemTag == "Money")
                    {
                        player.money--;
                    }
                    if (itemAmount[i] == 0)
                    {
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(firstItemSelected);

                        items.Remove(_item);
                        itemAmount.Remove(itemAmount[i]);
                    }
                }
            }
        }
        DisplayItems();
        ResetButtonItems();
    }

    public void UseItem(Item _item)
    {
        if (items.Contains(_item))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (_item == items[i])
                {
                    if (_item.itemTag == "HealthPercent")
                    {
                        itemAmount[i]--;
                        int percent;
                        percent = (player.maxHealth * _item.itemUsePoints) / 100;
                        player.healthFloat += percent;
                        player.healthSlider.value = player.health;
                        if (player.health > player.maxHealth)
                        {
                            player.healthFloat = player.maxHealth;
                        }
                    }

                    if (_item.itemTag == "ManaPercent")
                    {
                        itemAmount[i]--;
                        int percent;
                        percent = (player.maxMana * _item.itemUsePoints) / 100;
                        player.mana += percent;

                        if (player.mana > player.maxMana)
                        {
                            player.mana = player.maxMana;
                        }
                    }

                    if (_item.itemTag == "StaminaPercent")
                    {
                        itemAmount[i]--;
                        int percent;
                        percent = (player.maxStamina * _item.itemUsePoints) / 100;
                        player.staminaFloat += percent;

                        if (player.stamina > player.maxStamina)
                        {
                            player.staminaFloat = player.maxStamina;
                        }
                    }

                    if (_item.itemTag == "MiracleMana")
                    {
                        itemAmount[i]--;

                        player.maxMana += _item.itemUsePoints;
                        player.mana = player.maxMana;

                    }

                    if (_item.itemTag == "MiracleStamina")
                    {
                        itemAmount[i]--;

                        player.maxStamina += _item.itemUsePoints;
                        player.staminaFloat = player.maxStamina;

                    }

                    if (_item.itemTag == "TemporaryStamina")
                    {
                        itemAmount[i]--;
                        player.temporaryDrinked++;

                        if (hasDrinkedTemporaryStamina == false)
                        {
                            standardStaminaPoints = player.maxStamina;
                            player.maxStamina += _item.itemUsePoints;
                            player.stamina = player.maxStamina;
                            currentStaminaTime = _item.itemTimer;
                            temporaryStaminaSlider.maxValue = _item.itemTimer;
                            temporaryStaminaSlider.value = temporaryStaminaSlider.maxValue;
                            staminaTimerIsCountingDown = true;
                        }
                        else
                        {
                            player.maxStamina = standardStaminaPoints;
                            player.maxStamina += _item.itemUsePoints;
                            player.stamina = player.maxStamina;
                            currentStaminaTime = _item.itemTimer;
                            temporaryStaminaSlider.maxValue = _item.itemTimer;
                            temporaryStaminaSlider.value = temporaryStaminaSlider.maxValue;
                            staminaTimerIsCountingDown = true;
                        }
                    }

                    if (_item.itemTag == "TemporaryMana")
                    {
                        itemAmount[i]--;
                        player.temporaryDrinked++;

                        if (hasDrinkedTemporaryMana == false)
                        {
                            standardManaPoints = player.maxMana;
                            player.maxMana += _item.itemUsePoints;
                            player.mana = player.maxMana;
                            currentManaTime = _item.itemTimer;
                            temporaryManaSlider.maxValue = _item.itemTimer;
                            temporaryManaSlider.value = temporaryManaSlider.maxValue;
                            manaTimerIsCountingDown = true;
                        }
                        else
                        {
                            player.maxMana = standardManaPoints;
                            player.maxMana += _item.itemUsePoints;
                            player.mana = player.maxMana;
                            currentManaTime = _item.itemTimer;
                            temporaryManaSlider.maxValue = _item.itemTimer;
                            temporaryManaSlider.value = temporaryManaSlider.maxValue;
                            manaTimerIsCountingDown = true;
                        }
                    }

                    if (_item.itemTag == "TemporaryFocus")
                    {
                        itemAmount[i]--;
                        player.temporaryDrinked++;

                        if (hasDrinkedTemporaryFocus == false)
                        {
                            temporaryFocusPoints = _item.itemUsePoints;
                            player.oneHandedWeapons += _item.itemUsePoints;
                            player.twoHandedWeapons += _item.itemUsePoints;
                            player.axes += _item.itemUsePoints;
                            player.bows += _item.itemUsePoints;
                            player.crossbows += _item.itemUsePoints;
                            player.shields += _item.itemUsePoints;
                            currentFocusTime = _item.itemTimer;
                            temporaryFocusSlider.maxValue = _item.itemTimer;
                            temporaryFocusSlider.value = temporaryFocusSlider.maxValue;
                            focusTimerIsCountingDown = true;
                        }
                        else
                        {
                            player.oneHandedWeapons -= temporaryFocusPoints;
                            player.oneHandedWeapons += _item.itemUsePoints;
                            player.twoHandedWeapons -= temporaryFocusPoints;
                            player.twoHandedWeapons += _item.itemUsePoints;
                            player.axes -= temporaryFocusPoints;
                            player.axes += _item.itemUsePoints;
                            player.bows -= temporaryFocusPoints;
                            player.bows += _item.itemUsePoints;
                            player.crossbows -= temporaryFocusPoints;
                            player.crossbows += _item.itemUsePoints;
                            player.shields -= temporaryFocusPoints;
                            player.shields += _item.itemUsePoints;
                            currentFocusTime = _item.itemTimer;
                            temporaryFocusSlider.maxValue = _item.itemTimer;
                            temporaryFocusSlider.value = temporaryFocusSlider.maxValue;
                            focusTimerIsCountingDown = true;
                        }
                    }

                    if (_item.itemTag == "NorthearthBooze")
                    {
                        itemAmount[i]--;

                        if (hasDrinkedNorthearthBooze == false)
                        {
                            player.oneHandedWeapons += _item.itemUsePoints;
                            player.twoHandedWeapons += _item.itemUsePoints;
                            player.axes += _item.itemUsePoints;
                            player.bows += _item.itemUsePoints;
                            player.crossbows += _item.itemUsePoints;
                            player.shields += _item.itemUsePoints;
                            currentNorthearthBoozeTime = _item.itemTimer;////////////////////////
                            temporaryFocusSlider.maxValue = _item.itemTimer;
                            temporaryFocusSlider.value = temporaryFocusSlider.maxValue;
                            focusTimerIsCountingDown = true;
                        }
                        else
                        {
                            player.oneHandedWeapons -= temporaryFocusPoints;
                            player.oneHandedWeapons += _item.itemUsePoints;
                            player.twoHandedWeapons -= temporaryFocusPoints;
                            player.twoHandedWeapons += _item.itemUsePoints;
                            player.axes -= temporaryFocusPoints;
                            player.axes += _item.itemUsePoints;
                            player.bows -= temporaryFocusPoints;
                            player.bows += _item.itemUsePoints;
                            player.crossbows -= temporaryFocusPoints;
                            player.crossbows += _item.itemUsePoints;
                            player.shields -= temporaryFocusPoints;
                            player.shields += _item.itemUsePoints;
                            currentFocusTime = _item.itemTimer;
                            temporaryFocusSlider.maxValue = _item.itemTimer;
                            temporaryFocusSlider.value = temporaryFocusSlider.maxValue;
                            focusTimerIsCountingDown = true;
                        }
                    }

                    if (itemAmount[i] == 0)
                    {
                        EventSystem.current.SetSelectedGameObject(null);
                        EventSystem.current.SetSelectedGameObject(firstItemSelected);

                        items.Remove(_item);
                        itemAmount.Remove(itemAmount[i]);
                    }
                }
            }
        }
        DisplayItems();
        ResetButtonItems();
    }

    void Timers()
    {
        if (staminaTimerIsCountingDown)
        {
            hasDrinkedTemporaryStamina = true;
            temporaryStaminaSlider.gameObject.SetActive(true);
            currentStaminaTime -= Time.deltaTime;
            temporaryStaminaSlider.value = currentStaminaTime;

            if (currentStaminaTime <= 0)
            {
                hasDrinkedTemporaryStamina = false;
                player.maxStamina = standardStaminaPoints;
                player.staminaSlider.value = player.stamina;
                staminaTimerIsCountingDown = false;
                temporaryStaminaSlider.gameObject.SetActive(false);
            }
        }

        if (manaTimerIsCountingDown)
        {
            hasDrinkedTemporaryMana = true;
            temporaryManaSlider.gameObject.SetActive(true);
            currentManaTime -= Time.deltaTime;
            temporaryManaSlider.value = currentManaTime;

            if (currentManaTime <= 0)
            {
                hasDrinkedTemporaryMana = false;
                player.maxMana = standardManaPoints;
                player.manaSlider.value = player.mana;
                manaTimerIsCountingDown = false;
                temporaryManaSlider.gameObject.SetActive(false);
            }
        }

        if (focusTimerIsCountingDown)
        {
            hasDrinkedTemporaryFocus = true;
            temporaryFocusSlider.gameObject.SetActive(true);
            currentFocusTime -= Time.deltaTime;
            temporaryFocusSlider.value = currentFocusTime;

            if (currentFocusTime <= 0)
            {
                hasDrinkedTemporaryFocus = false;
                player.oneHandedWeapons -= temporaryFocusPoints;
                player.twoHandedWeapons -= temporaryFocusPoints;
                player.axes -= temporaryFocusPoints;
                player.bows -= temporaryFocusPoints;
                player.crossbows -= temporaryFocusPoints;
                player.shields -= temporaryFocusPoints;
                focusTimerIsCountingDown = false;
                temporaryFocusSlider.gameObject.SetActive(false);
            }
        }
    }

    void ResetButtonItems()
    {
        for (int i = 0; i < itemButtons.Length; i++)
        {
            if (i < items.Count)
            {
                itemButtons[i].thisItem = items[i];
            }
            else
            {
                itemButtons[i].thisItem = null;
            }
        }
    }

    void StatisticsControl()
    {
        healthPoints.text = player.health + "/" + player.maxHealth;
        staminaPoints.text = player.stamina + "/" + player.maxStamina;
        manaPoints.text = player.mana + "/" + player.maxMana;
        oneHandedWeaponsPercent.text = player.oneHandedWeapons + "%";
        twoHandedWeaponsPercent.text = player.twoHandedWeapons + "%";
        axesPercent.text = player.axes + "%";
        bowsPercent.text = player.bows + "%";
        crossbowsPercent.text = player.crossbows + "%";
        shieldsPercent.text = player.shields + "%";
    }
}
