﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Room", menuName = "TextAdventure/Room")]
public class Room : ScriptableObject {

    [TextArea]
    public string description;
    public string roomName;


}