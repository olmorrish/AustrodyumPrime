﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputAction : ScriptableObject {

    public string keyword;

    public abstract void RespondToInput(GameMaster gameMaster, string[] separatedInputWords);
}
