using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItems : MonoBehaviour {

    private GameMaster gameMaster;
    public Dictionary<string, string> examineDictionary = new Dictionary<string, string>();
    public Dictionary<string, string> takeDictionary = new Dictionary<string, string>();
    public Dictionary<string, string> lickDictionary = new Dictionary<string, string>();
    [HideInInspector] public List<string> nounsInRoom = new List<string>();
    private List<string> nounsInInventory = new List<string>();

    public List<InteractableObject> useableItemList;
    Dictionary<string, ActionResponse> useDictionary = new Dictionary<string, ActionResponse>();

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

    //Called whenever you take an item, so we have a list of actions that can be taken when it is used
    public void AddActionResponsesToUseDictionary() {
        foreach (string noun in nounsInInventory) {
            //get an interactable object associated with the noun, then for each associated interaction, store all actions in the use dictionary
            InteractableObject interactableInInventory = GetInteractableObjectFromUsablesList(noun);
            if(interactableInInventory == null) {
                continue;
            }

            foreach (Interaction interaction in interactableInInventory.interactions) {
                if(interaction.actionResponse == null) {
                    continue;
                }
                if (!useDictionary.ContainsKey(noun)) {
                    useDictionary.Add(noun, interaction.actionResponse);
                }
            }
        }
    }

    private InteractableObject GetInteractableObjectFromUsablesList(string noun) {
        foreach(InteractableObject interactableObject in useableItemList) {
            if (interactableObject.noun.Equals(noun)) {
                return interactableObject;
            }
        }
        return null;
    }

    public void DisplayInventory() {
        gameMaster.LogStringWithReturn("Inside your backpack, you have: ");
        if(nounsInInventory.Count == 0) {
            gameMaster.LogStringWithReturn("Nothing!");
        }
        else {
            foreach(string noun in nounsInInventory) {
                gameMaster.LogStringWithReturn(noun);
            }
        }
    }

    /// <summary>
    /// Called when going to a new room.
    /// </summary>
    public void ClearCollections() {
        examineDictionary.Clear();
        lickDictionary.Clear();
        nounsInRoom.Clear();
        takeDictionary.Clear();
    }

    public Dictionary<string, string> Take(string[] separatedInputWords) {
        string noun = separatedInputWords[1];
        if (nounsInRoom.Contains(noun)) {
            nounsInInventory.Add(noun);
            AddActionResponsesToUseDictionary();
            nounsInRoom.Remove(noun);
            return takeDictionary;
        }
        else {
            gameMaster.LogStringWithReturn("There is no " + noun + " to take.");
            return null;
        }
    }

    public void UseItem(string[] separatedInput) {
        string noun = separatedInput[1];
        if (nounsInInventory.Contains(noun)) {
            if (useDictionary.ContainsKey(noun)) {
                bool actionResult = useDictionary[noun].DoActionResponse(gameMaster); //activate the action response
                if (!actionResult) {
                    gameMaster.LogStringWithReturn("Nothing happens.");
                }

            }
            else {
                gameMaster.LogStringWithReturn("You're not sure how to use the " + noun + ".");
            }
        }
        else {
            gameMaster.LogStringWithReturn("There is no " + noun + " in your inventory.");
        }
    }

    public void LickItem(string[] separatedInput) {
        string noun = separatedInput[1];
        if (lickDictionary.ContainsKey(noun)) {
            gameMaster.LogStringWithReturn(lickDictionary[noun]);
            bool actionResult = useDictionary[noun].DoActionResponse(gameMaster); //activate the action response
        }
        else {
            gameMaster.LogStringWithReturn("There is no " + noun + " in your inventory.");
        }
    }
}
