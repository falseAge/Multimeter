using System;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
public class MultimeterController : MonoBehaviour
{
    [SerializeField] private MultimeterModel model;
    [SerializeField] private MultimeterView view;

    [SerializeField] private Material _wheelMaterial;
    [SerializeField] private Material _selectedWheelMaterial;


    [SerializeField] private float _power = 400f;
    [SerializeField] private float _resistance = 1000f;

    private float _scrollAngleStep = 72f;
    private bool _isWheelSelect = false;
    public static event Action ActionTriggerActive;

    private void Start()
    {
        _scrollAngleStep = 360f / Enum.GetValues(typeof(MultimeterMode)).Length;
        gameObject.GetComponent<MeshRenderer>().material = _wheelMaterial;
        model.SetValues(_power, _resistance);
    }

    private void OnMouseEnter()
    {
        _isWheelSelect = true;
        gameObject.GetComponent<MeshRenderer>().material = _selectedWheelMaterial;
        ActionTriggerActive += UpdateDisplayValues;
    }

    private void OnMouseExit()
    {
        _isWheelSelect = false;
        gameObject.GetComponent<MeshRenderer>().material = _wheelMaterial;
        ActionTriggerActive -= UpdateDisplayValues;
    }

    private void UpdateDisplayValues()
    {
        switch (model.CurrentMode)
        {
            case MultimeterMode.Voltage:
                view.UpdateDisplayText(model.CalculateVoltage());
                break;
            case MultimeterMode.ACVoltage:
                view.UpdateDisplayText(0.01f);
                break;
            case MultimeterMode.Amperage:
                view.UpdateDisplayText(model.CalculateAmperage());
                break;
            case MultimeterMode.Resistance:
                view.UpdateDisplayText(model.Resistance);
                break;
            default:
                view.TurnOffDisplay();
                break;
        }
    }

    private void Update()
    {
        if (_isWheelSelect)
        {
            float scroll = Input.mouseScrollDelta.y;

            if (scroll > 0)
            {
                model.SetModeNext();
            }

            else if (scroll < 0)
            {
                model.SetModePrevious();
            }

            ActionTriggerActive?.Invoke();
            view.UpdateMode(model.CurrentMode);
        }

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation, 
            Quaternion.Euler(0, -90, ((float)model.CurrentMode * _scrollAngleStep) + 90), 
            0.1f);
    }
}
