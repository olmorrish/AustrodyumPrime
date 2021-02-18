using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItems : MonoBehaviour {

    private GameMaster gameMaster;
    public Dictionary<string, string> examineDictionary = new Dictionary<string, string>();
    public Dictionary<string, string> takeDictionary = new Dictionary<string, string>();
    [HideInInspector] public List<string> nounsInRoom = new List<string>();
    private List<string> nounsInInventory = new List<string>();

    private void Awake() {
        gameMaster = GetComponent<GameMaster>();
    }

    public string GetItemsNotInInventory(Room currentRoom, InteractableObject interactableInRoom) {
        //if the player doesn't hav the item, add the item to the items in room and return the description
        if (!nounsInInventory.Contains(interactableInRoom.noun)) {
            nounsInRoom.Add(interactableInRoom.noun);
            return interactableInRoom.description;
        }
        return null;
    }

    /// <summary>
    /// Called when going to a new room.
    /// </summary>
    public void ClearCollections() {
        examineDictionary.Clear();
        nounsInRoom.Clear();
        takeDictionary.Clear();
    }

    public Dictionary<string, string> Take(string[] separatedInputWords) {
        string noun = separatedInputWords[1];
        if (nounsInRoom.Contains(noun)) {
            nounsInInventory.Add(noun);
            nounsInRoom.Remove(noun);
            return takeDictionary;
        }
        else {
            gameMaster.LogStringWithReturn("There is no " + noun + " to take.");
            return null;
        }
    }
}
