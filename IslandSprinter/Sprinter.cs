using System;
using Dalamud.Hooking;
using Dalamud.Logging;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.Game;

namespace IslandSprinter;

public unsafe class Sprinter : IDisposable
{
    private static uint SPRINT = 4;
    private static uint ISLAND_SPRINT = 31314;
    private static ushort ISLAND_TERRITORY_ID = 1055;

    private readonly IClientState clientState;

    public Sprinter(IClientState clientState, IGameInteropProvider gameInteropProvider)
    {
        this.clientState = clientState;
        useActionHook = gameInteropProvider.HookFromAddress<UseActionDelegate>((IntPtr)ActionManager.MemberFunctionPointers.UseAction, UseActionDetour);
    }

    //ActionManager*, ActionType, uint, ulong, uint, uint, uint, void*, bool
    private delegate bool UseActionDelegate(ActionManager* actionManager, ActionType actionType, uint actionID, ulong targetObjectID, uint param, uint useType, uint pvp, void* arg8);
    private bool UseActionDetour(ActionManager* actionManager, ActionType actionType, uint actionID, ulong targetObjectID, uint param, uint useType, uint pvp, void* arg8)
    {
        if(clientState.TerritoryType.Equals(ISLAND_TERRITORY_ID) && actionType.Equals(ActionType.GeneralAction) && actionID.Equals(SPRINT))
        {
            return useActionHook.Original(actionManager, ActionType.Ability, ISLAND_SPRINT, targetObjectID, param, useType, pvp, arg8);
        }
        return useActionHook.Original(actionManager, actionType, actionID, targetObjectID, param, useType, pvp, arg8);
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
