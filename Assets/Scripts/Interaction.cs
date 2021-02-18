using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //data class: can see in inspector
public class Interaction {

    public InputAction inputAction;
    [TextArea] public string textResponse;
}
