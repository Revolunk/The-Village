using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Fire : MonoBehaviour
{
    [Header("Attachements")]
    private GameObject player;
    private Player playerScript;
    private InteriorsTemperatureHandler interiorsTemperatureHandler;
    public ParticleSystem fire;
    public ParticleSystem embers;
    public ParticleSystem smoke;
    public Light lightSource;

    [Header("Behaviour")]
    public bool fireIsOn;
    public float duration;
    [Space(5)]
    public Gradient lightColor;
    private float lightTime;
    public float noiseSpeed;
    [Space(5)]
    public bool isOutdoor;
    public bool hasEffectOnPlayer;
    private float playerDistance;
    public bool hasEffectOnInterior;
    private bool externalForcesEnabled;

    [Header("Atributes")]
    public int feelsLikeTemperature;

    void Start()
    {
        //find player object and it's script
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();

        //reset fire
        externalForcesEnabled = true;
        fireIsOn = false;
        hasEffectOnPlayer = false;
        hasEffectOnInterior = false;
    }

    void Update()
    {
        var fireEmision = fire.emission;
        var embersEmision = embers.emission;
        var smokeEmision = smoke.emission;

        //check for player's distance
        playerDistance = Vector3.Distance(transform.position, player.transform.position);

        if (fireIsOn)
        {
            //turn on the light, make it's duration lower
            duration -= Time.deltaTime;

            fireEmision.enabled = true;
            embersEmision.enabled = true;
            smokeEmision.enabled = true;

            // smoothly turn on and set light source to be flickering
            lightSource.enabled = true;
            lightSource.color = lightColor.Evaluate(lightTime) * Flicker();
            lightTime += Time.deltaTime;
            if (lightTime > 1)
            {
                lightTime = 1;
            }

            if (playerDistance <= 1)
            {
                if (hasEffectOnInterior == false)
                {
                    if (hasEffectOnPlayer == false)
                    {
                        //if outside and in range, add fire's temperature to temperature of player
                        playerScript.additionalTemperatures += feelsLikeTemperature;
                        hasEffectOnPlayer = true;
                    }
                }
            }
            else
            {
                if (hasEffectOnPlayer)
                {
                    //if not in range, remove temperate added to player
                    playerScript.additionalTemperatures -= feelsLikeTemperature;
                    hasEffectOnPlayer = false;
                }
            }
        }
        else
        {
            fireEmision.enabled = false;
            embersEmision.enabled = false;
            smokeEmision.enabled = false;

            //if turned off, smoothly turn off the light source
            lightSource.enabled = true;
            lightSource.color = lightColor.Evaluate(lightTime) * Flicker();
            lightTime -= Time.deltaTime;
            if (lightTime < 0)
            {
                lightTime = 0;
            }
            //removes effects from player
            if (hasEffectOnPlayer)
            {
                playerScript.additionalTemperatures -= feelsLikeTemperature;
                hasEffectOnPlayer = false;
            }

            //removes effects from interior
            if (hasEffectOnInterior)
            {
                interiorsTemperatureHandler.additionalTemperatures -= feelsLikeTemperature;

                hasEffectOnInterior = false;
            }
        }

        //if duration < 0, turn of the fire
        if (duration < 0)
        {
            fireIsOn = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Interior"))
        {
            //if fire touches interior, turn off the forces of the particles
            var forceOnFire = fire.externalForces;
            forceOnFire.enabled = externalForcesEnabled;

            var forceOnEmbers = embers.externalForces;
            forceOnEmbers.enabled = externalForcesEnabled;


            var forceOnSmoke = smoke.externalForces;
            forceOnSmoke.enabled = externalForcesEnabled;

            externalForcesEnabled = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Interior"))
        {
            //if fire is in the interior, take it's temperate script
            interiorsTemperatureHandler = other.GetComponent<InteriorsTemperatureHandler>();

            if (isOutdoor)
            {
                hasEffectOnInterior = false;
            }
            else
            {
                if (hasEffectOnInterior == false && fireIsOn)
                {
                    //add temperature to the interior
                    interiorsTemperatureHandler.additionalTemperatures += feelsLikeTemperature;
                    hasEffectOnInterior = true;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Interior"))
        {
            //if fire exits the interior, tun on it's forces and removed effects from interior
            var forceOnFire = fire.externalForces;
            forceOnFire.enabled = externalForcesEnabled;

            var forceOnEmbers = embers.externalForces;
            forceOnEmbers.enabled = externalForcesEnabled;


            var forceOnSmoke = smoke.externalForces;
            forceOnSmoke.enabled = externalForcesEnabled;

            externalForcesEnabled = true;

            if (hasEffectOnInterior == true)
            {
                interiorsTemperatureHandler.additionalTemperatures -= feelsLikeTemperature;
                hasEffectOnInterior = false;
            }
            interiorsTemperatureHandler = null;
        }
    }

    float Flicker()
    {
        //create flickierng
        float y;

        y = 1 - (Random.value * noiseSpeed);
        return y;
    }

    void PickUpUse()
    {
        //turn on/off using key press
        if (fireIsOn)
        {
            fireIsOn = false;
        }
        else
        {
            fireIsOn = true;
        }
    }
}