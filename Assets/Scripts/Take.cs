using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Take")]
public class Take : InputAction {

    public override void RespondToInput(GameMaster gameMaster, string[] separatedInputWords) {
        Dictionary<string, string> takeDictionary = gameMaster.interactableItems.Take(separatedInputWords); //try to take something
        if(takeDictionary != null) {
            gameMaster.LogStringWithReturn(gameMaster.TestVerbDictionaryWithNoun(takeDictionary, separatedInputWords[0], separatedInputWords[1]));
        }
    }

}
