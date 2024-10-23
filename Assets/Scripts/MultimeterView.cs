using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MultimeterView : MonoBehaviour
{
    [SerializeField] private TextMeshPro _displayText;

    [SerializeField] private TextMeshProUGUI _voltageValueText;
    [SerializeField] private TextMeshProUGUI _amperageValueText;
    [SerializeField] private TextMeshProUGUI _aCVoltageValueText;
    [SerializeField] private TextMeshProUGUI _resistanceValueText;

    public void UpdateMode(MultimeterMode mode)
    {
        _voltageValueText.text = "V =";
        _amperageValueText.text = "V =";
        _aCVoltageValueText.text = "V =";
        _resistanceValueText.text = "V =";

        switch (mode)
        {
            case MultimeterMode.Voltage:
                _voltageValueText.text = $"V = {_displayText.text}";
                break;

            case MultimeterMode.Amperage:
                _amperageValueText.text = $"A = {_displayText.text}";
                break;

            case MultimeterMode.ACVoltage:
                _aCVoltageValueText.text = $"~V = {_displayText.text}";
                break;

            case MultimeterMode.Resistance:
                _resistanceValueText.text = $"Ω = {_displayText.text}";
                break;

            default:
                break;
        }
    }

    public void UpdateDisplayText(float value)
    {
        _displayText.text = value.ToString("F2");
    }

    public void TurnOffDisplay()
    {
        _displayText.text = "";
    }
}
