using UnityEngine;
using UnityEngine.InputSystem;

public static class Keybinds {
   

    public static readonly Joystick Joystick = new Joystick();
    public static readonly Joystick.PlayerActions PlayerActions = Joystick.Player;
    public static readonly InputActionMap PlayerMap = PlayerActions.Get();


}