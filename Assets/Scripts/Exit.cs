using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //allows embedding of a class with sub properties in the inspector, similar to a list
public class Exit {

    public string keyString;       //identifier
    public string exitDescription; //describes the exit
    public Room valueRoom;         //

}

