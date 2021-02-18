using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponse/ChangeRoom")]
public class ChangeRoomResponse : ActionResponse {

    public Room roomToChangeTo;

    public override bool DoActionResponse(GameMaster gameMaster) {
        if (gameMaster.roomNavigation.currentRoom.roomName.Equals(requiredString)) {
            gameMaster.roomNavigation.currentRoom = roomToChangeTo;
            gameMaster.DisplayRoomText();
            return true;
        }
        return false;
    }
}
