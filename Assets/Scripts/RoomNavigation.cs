using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour {

    public Room currentRoom;
    private GameMaster gameMaster;

    private void Awake() {
        gameMaster = GetComponent<GameMaster>();
    }

    /// <summary>
    /// Goes over all exits in teh current room and gives them to the GameMaster
    /// </summary>
    public void UnpackExitsInRoom() {
        foreach(Exit exit in currentRoom.exits) {
            gameMaster.interactionDescriptionsInRoom.Add(exit.exitDescription);
        }
    }


}
