using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorsTemperatureHandler : MonoBehaviour
{
    [Header("Attachements")]
    private Player playerScript;
    //public DoorOpener door;

    [Header("Atributes")]
    public int additionalTemperatures;
    private int lastAdditionalTemperatures;
    private int difference;

    [Header("States")]
    public bool hasEffectOnPlayer;

    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        lastAdditionalTemperatures = additionalTemperatures;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (hasEffectOnPlayer == false)
            {
                playerScript.additionalTemperatures += additionalTemperatures;
                lastAdditionalTemperatures = additionalTemperatures;
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            hasEffectOnPlayer = true;

            if (hasEffectOnPlayer && lastAdditionalTemperatures < additionalTemperatures)
            {
                playerScript.additionalTemperatures -= lastAdditionalTemperatures;
                playerScript.additionalTemperatures += additionalTemperatures;
                lastAdditionalTemperatures = additionalTemperatures;
            }
            if (hasEffectOnPlayer && lastAdditionalTemperatures > additionalTemperatures)
            {
                difference = lastAdditionalTemperatures - additionalTemperatures;
                playerScript.additionalTemperatures -= difference;
                lastAdditionalTemperatures = additionalTemperatures;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (hasEffectOnPlayer == true)
            {
                playerScript.additionalTemperatures -= additionalTemperatures;
                hasEffectOnPlayer = false;
            }
        }
    }
}
