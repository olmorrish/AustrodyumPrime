using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    [HideInInspector] public RoomNavigation roomNavigation;
    List<string> actionLog = new List<string>();
    [HideInInspector] public List<string> interactionDescriptionsInRoom = new List<string>();
    public TextMeshProUGUI displayText;

    // Start is called before the first frame update
    void Awake() {
        roomNavigation = GetComponent<RoomNavigation>();
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
        UnpackRoom();
        string joinedInteractionDescriptions = string.Join("\n", interactionDescriptionsInRoom.ToArray());
        string combinedText = roomNavigation.currentRoom.description + "\n" + joinedInteractionDescriptions;
        LogStringWithReturn(combinedText);
    }

    private void UnpackRoom() {
        roomNavigation.UnpackExitsInRoom();
    }

    public void LogStringWithReturn(string toAdd) {
        actionLog.Add(toAdd + "\n");
    }

    // Update is called once per frame
    void Update() {
        
    }
}
