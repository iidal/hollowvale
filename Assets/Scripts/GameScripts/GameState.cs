
using System;
using UnityEngine;

public static class GameState
{
    public enum ActionType {Move = 0, Ability=1, EndTurn=2, Invalid = 999};
    public enum BoardObjType {Playable = 0, Enemy = 1, Breakable= 2, Unbreakable = 3, Invalid = 999};
    public static bool something = true;

    public static ActionType IntToGameStateEnum(int index)
    {
        if(Enum.IsDefined(typeof(ActionType), index))
        {
            return (ActionType)index;
        }
        Debug.LogError("Trying to get ActionType enum for invalid index=" + index);
        return ActionType.Invalid;
    }


}