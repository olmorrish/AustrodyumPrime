using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Lick")]
public class Lick : InputAction {

    public override void RespondToInput(GameMaster gameMaster, string[] separatedInputWords) {
        if(separatedInputWords.Length >= 2) {
            gameMaster.LogStringWithReturn(gameMaster.TestVerbDictionaryWithNoun(gameMaster.interactbleItems.lickDictionary, separatedInputWords[0], separatedInputWords[1]));
        }
        else {
            gameMaster.LogStringWithReturn("Lick what?");
        }
    }
}
