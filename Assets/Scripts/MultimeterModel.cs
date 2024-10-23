using System;
using UnityEngine;

public enum MultimeterMode
{
    Neutral,
    Voltage,
    ACVoltage,
    Amperage,
    Resistance
}

public class MultimeterModel : MonoBehaviour
{
    public MultimeterMode CurrentMode { get; private set; } = MultimeterMode.Neutral;

    public float Power { get; private set; } = 0f;
    public float Resistance { get; private set; } = 0f;

    public void SetModeNext()
    {
        if (CurrentMode != MultimeterMode.Resistance)
        {
            CurrentMode = (MultimeterMode)((int)CurrentMode + 1);
        }
        else
            CurrentMode = MultimeterMode.Neutral;
    }

    public void SetModePrevious()
    {
        if (CurrentMode != MultimeterMode.Neutral)
        {
            CurrentMode = (MultimeterMode)((int)CurrentMode - 1);
        }
        else
            CurrentMode = MultimeterMode.Resistance;
    }


    public float CalculateAmperage()
    {
        return (float)Math.Round(Math.Sqrt(Power / Resistance), 2);
    }

    public float CalculateVoltage()
    {
        return (float)Math.Round(Power / CalculateAmperage(), 2);
    }

    
    public void SetValues(float power, float resistance)
    {
        Power = power;
        Resistance = resistance;
    }
}
