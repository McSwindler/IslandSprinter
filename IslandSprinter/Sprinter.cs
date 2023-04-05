using System;
using Dalamud.Game.ClientState;
using Dalamud.Hooking;
using Dalamud.Logging;
using FFXIVClientStructs.FFXIV.Client.Game;

namespace IslandSprinter;

public unsafe class Sprinter
{
    private static uint SPELL_TYPE = (uint) ActionType.Spell;
    private static uint GENERAL_TYPE = (uint) ActionType.General;
    private static uint SPRINT = 4;
    private static uint ISLAND_SPRINT = 31314;
    private static ushort ISLAND_TERRITORY_ID = 1055;

    private readonly ClientState clientState;

    public Sprinter(ClientState clientState)
    {
        this.clientState = clientState;
        useActionHook = Hook<UseActionDelegate>.FromAddress((IntPtr)ActionManager.MemberFunctionPointers.UseAction, UseActionDetour);
    }

    private delegate bool UseActionDelegate(ActionManager* actionManager, uint actionType, uint actionID, long targetObjectID, uint param, uint useType, int pvp, bool* isGroundTarget);
    private bool UseActionDetour(ActionManager* actionManager, uint actionType, uint actionID, long targetObjectID, uint param, uint useType, int pvp, bool* isGroundTarget)
    {
        if(clientState.TerritoryType.Equals(ISLAND_TERRITORY_ID) && actionType.Equals(GENERAL_TYPE) && actionID.Equals(SPRINT))
        {
            return useActionHook.Original(actionManager, SPELL_TYPE, ISLAND_SPRINT, targetObjectID, param, useType, pvp, isGroundTarget);
        }
        return useActionHook.Original(actionManager, actionType, actionID, targetObjectID, param, useType, pvp, isGroundTarget);
    }

    private readonly Hook<UseActionDelegate> useActionHook;

    public void Initialize()
    {
        
        useActionHook.Enable();
    }

    public void Dispose() { 
        useActionHook.Dispose();
    }
}
