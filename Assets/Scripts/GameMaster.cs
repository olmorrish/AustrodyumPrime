using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    [HideInInspector] public RoomNavigation roomNavigation;
    [HideInInspector] public InteractableItems interactableItems;
    List<string> actionLog = new List<string>();
    [HideInInspector] public List<string> interactionDescriptionsInRoom = new List<string>();
    public TextMeshProUGUI displayText;
    public InputAction[] inputActions; //all possible actions, all of which inherit from InputAction abstract class
    public string cachedRoomOutput; //saves the room text when we want to look around again

    // Start is called before the first frame update
    void Awake() {
        roomNavigation = GetComponent<RoomNavigation>();
        interactableItems = GetComponent<InteractableItems>();
    }

    private void Start() {
        DisplayRoomText();
        DisplayLoggedText();
    }

    public void DisplayLoggedText() {
        string logAsText = string.Join("\n", actionLog.ToArray());
        displayText.text = logAsText;
    }

    public void LookAround() {
        LogStringWithReturn(cachedRoomOutput);
    }

    public void DisplayRoomText() {
        ClearCollectionsForNewRoom();
        UnpackRoom();
        string joinedInteractionDescriptions = string.Join("\n", interactionDescriptionsInRoom.ToArray());
        string combinedText = roomNavigation.currentRoom.description + "\n" + joinedInteractionDescriptions;
        cachedRoomOutput = combinedText;
        LogStringWithReturn(combinedText);
    }

    private void UnpackRoom() {
        roomNavigation.UnpackExitsInRoom();
        PrepareObjectsToTakeOrExamine(roomNavigation.currentRoom);
    }

    private void PrepareObjectsToTakeOrExamine(Room currrentRoom) {
        if (currrentRoom.interactableObjectsInRoom == null)
            return;

        //create list of interactions based on current room
        foreach (InteractableObject interactable in currrentRoom.interactableObjectsInRoom) {

            //add any items in the room that aren't in inventory to the description of the room
            string descriptionNotInInventory = interactableItems.GetItemsNotInInventory(currrentRoom, interactable);
            if (descriptionNotInInventory != null) {
                interactionDescriptionsInRoom.Add(descriptionNotInInventory); //add the item decriptions to the unpacked description
            }

            //add interactions for the objects in the room to the dictionaries for those actions
            foreach (Interaction interactionForInteractable in interactable.interactions) {
                if (interactionForInteractable.inputAction.keyword.Equals("examine") && !interactableItems.examineDictionary.ContainsKey(interactable.noun)) {
                    interactableItems.examineDictionary.Add(interactable.noun, interactionForInteractable.textResponse);
                }
                if (interactionForInteractable.inputAction.keyword.Equals("take") && !interactableItems.takeDictionary.ContainsKey(interactable.noun)) {
                    interactableItems.takeDictionary.Add(interactable.noun, interactionForInteractable.textResponse);
                }
                if (interactionForInteractable.inputAction.keyword.Equals("lick") && !interactableItems.lickDictionary.ContainsKey(interactable.noun)) {
                    interactableItems.lickDictionary.Add(interactable.noun, interactionForInteractable.textResponse);
                }
            }
        }

        //get all interactables in inventory and add them to the dictionaries as well
        InteractableObject[] inventoryInteractables = interactableItems.GetInteractableItemsInInventory();
        foreach (InteractableObject interactable in inventoryInteractables) {
            foreach(Interaction interactionForInteractable in interactable.interactions) {
                if (interactionForInteractable.inputAction.keyword.Equals("examine") && !interactableItems.examineDictionary.ContainsKey(interactable.noun)) {
                    interactableItems.examineDictionary.Add(interactable.noun, interactionForInteractable.textResponse);
                }
                if (interactionForInteractable.inputAction.keyword.Equals("take") && !interactableItems.takeDictionary.ContainsKey(interactable.noun)) {
                    interactableItems.takeDictionary.Add(interactable.noun, interactionForInteractable.textResponse);
                }
                if (interactionForInteractable.inputAction.keyword.Equals("lick") && !interactableItems.lickDictionary.ContainsKey(interactable.noun)) {
                    interactableItems.lickDictionary.Add(interactable.noun, interactionForInteractable.textResponse);
                }
            }

        }
    }

    private void ClearCollectionsForNewRoom() {
        interactableItems.ClearCollections();
        interactionDescriptionsInRoom.Clear();
        roomNavigation.ClearExits();
    }

    public string TestVerbDictionaryWithNoun(Dictionary<string, string> verbDictionary, string verb, string noun) {
        if (verbDictionary.ContainsKey(noun)) {
            return verbDictionary[noun];
        }
        else {
            return "You can't think of a way to " + verb + " " + noun + ".";
        }
    }

    public void LogStringWithReturn(string toAdd) {
        actionLog.Add(toAdd + "\n");
    }

    public void DeathScene(string textToDisplay) {
        Debug.Log("End game says: " + textToDisplay);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
