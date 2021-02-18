using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InteractableObject")]
public class InteractableObject : ScriptableObject {

    public string noun = "name";
    [TextArea] public string description = "description in room";
    //[TextArea] public string lickDescription = "description when licked";
    public Interaction[] interactions; //each has an input action and response; they are serializable and so appear in the inspector
    
}
