using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Examine")]
public class Examine : InputAction {

    public override void RespondToInput(GameMaster gameMaster, string[] separatedInputWords) {
        //try to log the text response of the lookup, if it works
        gameMaster.LogStringWithReturn(gameMaster.TestVerbDictionaryWithNoun(gameMaster.interactbleItems.examineDictionary, separatedInputWords[0], separatedInputWords[1]));
    }

}
