using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysSelected : MonoBehaviour {

    private TMPro.TMP_InputField inputField;

    private void Start() {
        inputField = GetComponent<TMPro.TMP_InputField>();
    }

    void Update() {
        //ensure the field is always the focus
        inputField.Select();
        inputField.ActivateInputField();
    }

}
