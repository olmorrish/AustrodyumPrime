using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour {

    private GameMaster gameMaster;
    public TextMeshProUGUI inputFieldText;
    public TMPro.TMP_InputField inputField;

    private void Awake() {
        gameMaster = GetComponent<GameMaster>();
        inputField.onEndEdit.AddListener(AcceptStringInput);    //add a listener to the end-event on the input field, and call AcceptStringInput then
    }

    private void Start() {
        inputFieldText.text = "";
    }

    public void AcceptStringInput(string userInput) {
        userInput = userInput.ToLower();
        gameMaster.LogStringWithReturn(userInput);

        //process input and act if the first is a valid input action
        char[] delimiterChars = {' '};
        string[] separatedInput = userInput.Split(delimiterChars);

        foreach (InputAction inputAction in gameMaster.inputActions) {
            if (separatedInput[0].Equals(inputAction.keyword)) {
                inputAction.RespondToInput(gameMaster, separatedInput);
            }
        }

        InputComplete();
    }

    private void InputComplete() {
        gameMaster.DisplayLoggedText();
        inputField.ActivateInputField(); //reselect
        inputField.text = null; 
    }
}
