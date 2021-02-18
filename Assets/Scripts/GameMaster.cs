using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    [HideInInspector] public RoomNavigation roomNavigation;
    [HideInInspector] public InteractableItems interactbleItems;
    List<string> actionLog = new List<string>();
    [HideInInspector] public List<string> interactionDescriptionsInRoom = new List<string>();
    public TextMeshProUGUI displayText;
    public InputAction[] inputActions; //all possible actions, all of which inherit from InputAction abstract class

    // Start is called before the first frame update
    void Awake() {
        roomNavigation = GetComponent<RoomNavigation>();
        interactbleItems = GetComponent<InteractableItems>();
    }

    private void Start() {
        DisplayRoomText();
        DisplayLoggedText();
    }

    public void DisplayLoggedText() {
        string logAsText = string.Join("\n", actionLog.ToArray());
        displayText.text = logAsText;
    }

    public void DisplayRoomText() {
        ClearCollectionsForNewRoom();
        UnpackRoom();
        string joinedInteractionDescriptions = string.Join("\n", interactionDescriptionsInRoom.ToArray());
        string combinedText = roomNavigation.currentRoom.description + "\n" + joinedInteractionDescriptions;
        LogStringWithReturn(combinedText);
    }

    private void UnpackRoom() {
        roomNavigation.UnpackExitsInRoom();
        PrepareObjectsToTakeOrExamine(roomNavigation.currentRoom);
    }

    private void PrepareObjectsToTakeOrExamine(Room currrentRoom) {
        foreach(InteractableObject interactable in currrentRoom.interactableObjectsInRoom) {
            string descriptionNotInInventory = interactbleItems.GetItemsNotInInventory(currrentRoom, interactable);
            if(descriptionNotInInventory != null) {
                interactionDescriptionsInRoom.Add(descriptionNotInInventory); //add the item decriptions to the unpacked description
            }

            foreach(Interaction interactionForInteractable in interactable.interactions) {
                if(interactionForInteractable.inputAction.keyword.Equals("examine")){
                    interactbleItems.examineDictionary.Add(interactable.noun, interactionForInteractable.textResponse);
                }
                if (interactionForInteractable.inputAction.keyword.Equals("take")) {
                    interactbleItems.takeDictionary.Add(interactable.noun, interactionForInteractable.textResponse);
                }
            }
        }

    }

    private void ClearCollectionsForNewRoom() {
        interactbleItems.ClearCollections();
        interactionDescriptionsInRoom.Clear();
        roomNavigation.ClearExits();
    }

    public string TestVerbDictionaryWithNoun(Dictionary<string, string> verbDictionary, string verb, string noun) {
        if (verbDictionary.ContainsKey(noun)) {
            return verbDictionary[noun];
        }
        else {
            return "You can't " + verb + " " + noun;
        }
    }

    public void LogStringWithReturn(string toAdd) {
        actionLog.Add(toAdd + "\n");
    }

    // Update is called once per frame
    void Update() {
        
    }
}
