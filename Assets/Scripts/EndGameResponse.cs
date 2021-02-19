using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/ActionResponse/EndGame")]
public class EndGameResponse : ActionResponse {

    [TextArea] public string endText;

    public override bool DoActionResponse(GameMaster gameMaster) {
        gameMaster.DeathScene(endText);
        return true;
    }

}
