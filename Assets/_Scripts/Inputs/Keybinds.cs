using UnityEngine;
using UnityEngine.InputSystem;

public static class Keybinds {
   

    public static readonly Joystick Joystick = new Joystick();
    public static readonly InputActionMap Map = Joystick.PlayerOne.Get();


}