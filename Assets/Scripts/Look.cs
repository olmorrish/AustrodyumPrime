using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Look")]
public class Look : InputAction {

    public override void RespondToInput(GameMaster gameMaster, string[] separatedInputWords) {
        gameMaster.DisplayRoomText();
    }

}
