using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weather : MonoBehaviour
{
    public Light sun;
    public Light moon;
    public Gradient sunColor;
    public Gradient moonColor;

    public float second, minute, hour, day, month, year;
    private float sunSeconds;
    [Tooltip("1 = 1sec")] public int timeSpeed;
    public float currentDay;
    public TMP_Text timeUi;
    public TMP_Text dateUi;


    public int temperature;
    [Tooltip("Wait x seconds for another change of the temperature")] public float temperatureChangeTimer;
    public TMP_Text temperatureUi;


    public ParticleSystem snow;
    [Tooltip("Materials with snow shader")] public Material[] staticMaterialsWithSnow;
    public float snowLevel;
    public bool isSnowing;
    [Tooltip("It's random. 0 = snowing disabled   1 = snowing enabled")] public float snowChance;
    [Tooltip("It's random. Wait x seconds for another chance of snowing")] public float snowChanceTimer;
    [Tooltip("It's random every time it starts snowing")] public float emissionRate;
    [Tooltip("It's random every time it starts snowing")] public float simulationSpeed;


    public WindZone windZone;
    [Tooltip("Materials with snow shader")] public Material[] moveableMaterialsWithSnow;
    [Tooltip("Wait x seconds for another change of the wind")] public float windChangeTimer;
    [Tooltip("It's random every time wind changes")] public float windDirection;
    [Tooltip("It's random every time wind changes")] public float windStrength;

    public Material clouds;

    public void Start()
    {
        //set current day, sun seconds to make smooth  movement of sun and invoke voids every x seconds
        currentDay = day;
        sunSeconds = hour * (3600 / 1) + minute * (60 / 1) + second;
        InvokeRepeating("Temperature", 0, temperatureChangeTimer);
        InvokeRepeating("SnowChance", 0, snowChanceTimer);
        InvokeRepeating("Wind", 0, windChangeTimer);
    }

    void FixedUpdate()
    {
        //update UI
        temperatureUi.text = "Temperature       " + temperature;
        timeUi.text = string.Format("{0:00}:{1:00}", hour, minute);
        dateUi.text = string.Format("{0:00}.{1:00}.{2:00}", day, month, year);

        //calculate time, move the sun and update Snow
        CalculateTime();
        //Lighting(hour / 24);
        Lighting(sunSeconds / 86400);
        Snow();
    }

    void CalculateTime()
    {
        //calculate time for smooth sun
        sunSeconds += Time.deltaTime * timeSpeed;

        //calculate time
        second += Time.deltaTime * timeSpeed;

        //calulate min, h, day, month and year
        if (second >= 60)
        {
            minute++;
            second = 0;
        }
        else if (minute >= 60)
        {
            sunSeconds = 0;
            sunSeconds = hour * (3600 / 1) + minute * (60 / 1) + second;
            hour++;
            minute = 0;
        }
        else if (hour >= 24)
        {
            sunSeconds = 0;
            day++;
            hour = 0;
        }
        else if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
        {
            if (day >= 32)
            {
                month++;
                day = 1;
            }
        }
        else if (month == 4 || month == 6 || month == 9 || month == 11)
        {
            if (day >= 31)
            {
                month++;
                day = 1;
            }
        }
        else if (month == 2)
        {
            if (day >= 29)
            {
                month++;
                day = 1;
            }
        }
        else if (month >= 12)
        {
            year++;
            month = 1;
        }
    }

    void Lighting(float timePercent)
    {
        //change color of the sun through time  and it's rotation
        sun.color = sunColor.Evaluate(timePercent);
        sun.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360) - 90, 200, 90));

        //change color of the moon color through time
        moon.color = moonColor.Evaluate(timePercent);

        //split whole day into day and night
        if (hour > 6 && hour < 18)
        {
            //turn on sun 
            sun.enabled = true;

            //change color of the snow  to be visible during a day
            var colorOverLifetime = snow.colorOverLifetime;
            Gradient gradient = new Gradient();
            gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(0.8f, 0.8f, 0.8f, 1), 0.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f) });
            colorOverLifetime.color = gradient;
        }
        else
        {
            //turn off sun so moon's light can work
            sun.enabled = false;

            //change color of the snow to not be so visible during a day
            var colorOverLifetime = snow.colorOverLifetime;
            Gradient gradient = new Gradient();
            gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(0.2f, 0.2f, 0.2f, 1), 0.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f) });
            colorOverLifetime.color = gradient;
        }
    }

    void Temperature()
    {
        //set random temperature  during a day and night
        if (hour > 6 && hour < 19)
        {
            temperature = Random.Range(-10, 0);
        }
        else
        {
            temperature = Random.Range(-20, -10);
        }
    }

    void Snow()
    {
        //enable emission, rate over time and simulation speed
        var emission = snow.emission;
        emission.enabled = isSnowing;

        emission.rateOverTime = emissionRate;

        var snowMain = snow.main;
        snowMain.simulationSpeed = simulationSpeed;

        //create a chance of snowing. If we "roll" 1 then it's snowing, 0 = no snow
        if (snowChance == 1)
        {
            isSnowing = true;

            //change textures colors while snowing in different speeds
            if (simulationSpeed <= 1)
            {
                SnowLevel();
                snowLevel += 0.00001f;

                if (snowLevel > 8.99)
                {
                    snowLevel = 9;
                }
            }
            else if (simulationSpeed > 1 && simulationSpeed <= 2)
            {
                SnowLevel();
                snowLevel += 0.00002f;

                if (snowLevel > 8.99)
                {
                    snowLevel = 9;
                }
            }
            else if (simulationSpeed > 2 && simulationSpeed <= 3)
            {
                SnowLevel();
                snowLevel += 0.00003f;

                if (snowLevel > 8.99)
                {
                    snowLevel = 9;
                }
            }
            else if (simulationSpeed > 3)
            {
                SnowLevel();
                snowLevel += 0.00004f;

                if (snowLevel > 8.99)
                {
                    snowLevel = 9;
                }
            }
        }
        else
        {
            isSnowing = false;

            SnowLevel();
            snowLevel -= 0.000001f;
            if (snowLevel < 0.01)
            {
                snowLevel = 0;
            }
        }
    }
    void SnowChance()
    {
        //"roll "for a chance of snowing
        //Add always one more than you need. 0-1=50% chance, 0-5=20% chance for each draw
        snowChance = Random.Range(0, 2);

        if (snowChance == 1)
        {
            emissionRate = Random.Range(10, 1000);
            simulationSpeed = Random.Range(1, 4);
        }
    }

    void SnowLevel()
    {
        //search for selected materials and change their textures
        for (int i = 0; i < staticMaterialsWithSnow.Length; i++)
        {
            staticMaterialsWithSnow[i].SetFloat("snowShaderLevel", snowLevel);
        }
        for (int i = 0; i < moveableMaterialsWithSnow.Length; i++)
        {
            moveableMaterialsWithSnow[i].SetFloat("snowShaderLevel", snowLevel);
        }
    }

    void Wind()
    {
        //set wind's strength, turbulences and direction
        windStrength = windZone.windMain;
        windZone.windMain = Random.Range(0f, 0.3f);

        windZone.windTurbulence = windZone.windMain;

        windDirection = Random.Range(0, 360);
        windZone.transform.Rotate(0, windDirection, 0);

        //pineBranch.SetFloat("SnowShaderWindPower", windStrength);
        //pineBranch.SetFloat("SnowShaderTurbulenceFrequency", windStrength);
        //pineBranch2.SetFloat("SnowShaderWindPower", windStrength);
        //pineBranch2.SetFloat("SnowShaderTurbulenceFrequency", windStrength);
        //pineBranch3.SetFloat("SnowShaderWindPower", windStrength);
        //pineBranch3.SetFloat("SnowShaderTurbulenceFrequency", windStrength);
        //treeBranch.SetFloat("SnowShaderWindPower", windStrength / 2);
        //treeBranch.SetFloat("SnowShaderTurbulenceFrequency", windStrength / 2);

        //search materials to be moved by wind
        for (int i = 0; i < moveableMaterialsWithSnow.Length; i++)
        {
            moveableMaterialsWithSnow[i].SetFloat("SnowShaderTurbulenceFrequency", windStrength);
        }

        /////////////clouds.SetFloat("snowShaderLevel", snowLevel);
    }
}
