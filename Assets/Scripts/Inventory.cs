using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Inventory")]
public class Inventory : InputAction {

    public override void RespondToInput(GameMaster gameMaster, string[] separatedInputWords) {
        gameMaster.interactableItems.DisplayInventory();
    }
}
