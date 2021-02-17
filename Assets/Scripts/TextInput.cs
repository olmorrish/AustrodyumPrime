using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour {

    private GameMaster gameMaster;
    public TMPro.TMP_InputField inputField;

    private void Awake() {
        gameMaster = GetComponent<GameMaster>();
        inputField.onEndEdit.AddListener(AcceptStringInput);    //add a listener to the end-event on the input field, and call AcceptStringInput then
    }

    private void Update() {
        //if (Input.GetKeyDown) {

        //}
    }

    public void AcceptStringInput(string userInput) {
        userInput = userInput.ToLower();
        gameMaster.LogStringWithReturn(userInput);
        InputComplete();
    }

    private void InputComplete() {
        gameMaster.DisplayLoggedText();
        inputField.ActivateInputField(); //reselect
        inputField.text = null; 
    }
}
