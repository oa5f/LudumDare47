using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class SensitivityInputField : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private TMP_InputField inputField;

    private void Start()
    {
        float sensitivity = PlayerPrefs.GetFloat("MouseSensitivity");
        if(sensitivity == 0f)
        {
            PlayerPrefs.SetFloat("MouseSensitivity", 1f);
            sensitivity = 1f;
        }
        player.MouseSensitivity = sensitivity;
        inputField.text = sensitivity.ToString();
    }

    public void OnTextChange(string newValue)
    {
        if(float.TryParse(newValue, NumberStyles.Any, CultureInfo.InvariantCulture, out float sensitivity))
        {
            PlayerPrefs.SetFloat("MouseSensitivity", sensitivity);
            player.MouseSensitivity = sensitivity;
        }
    }
}
