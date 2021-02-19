using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNavigation : MonoBehaviour {

    public Room currentRoom;
    private GameMaster gameMaster;
    Dictionary<string, Room> exitDictionary = new Dictionary<string, Room>();

    private void Awake() {
        gameMaster = GetComponent<GameMaster>();
    }

    /// <summary>
    /// Goes over all exits in teh current room and gives them to the GameMaster
    /// </summary>
    public void UnpackExitsInRoom() {
        foreach(Exit exit in currentRoom.exits) {
            exitDictionary.Add(exit.keyString, exit.valueRoom); //create key value pair for exit
            gameMaster.interactionDescriptionsInRoom.Add(exit.exitDescription);
        }
    }

    public void AttemptToChangeRooms(string directionNoun) {
        if (exitDictionary.ContainsKey(directionNoun)) {
            currentRoom = exitDictionary[directionNoun];
            gameMaster.LogStringWithReturn("You go to the " + directionNoun + ".");
            gameMaster.DisplayRoomText();
        }
        else {
            gameMaster.LogStringWithReturn("There is no open path to the " + directionNoun + ".");
        }
    }

    /// <summary>
    /// Called by GameMaster
    /// </summary>
    public void ClearExits() {
        exitDictionary.Clear();
    }
}
