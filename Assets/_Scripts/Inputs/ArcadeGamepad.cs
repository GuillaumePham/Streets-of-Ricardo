// using System.Linq;

// using UnityEditor;

// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.InputSystem.Controls;
// using UnityEngine.InputSystem.Layouts;
// using UnityEngine.InputSystem.LowLevel;
// using UnityEngine.InputSystem.Utilities;

// [InputControlLayout(stateType = typeof(ArcadeGamepadState))]
// [InitializeOnLoad]
// public class ArcadeGamepad : InputDevice {

//     public ButtonControl button1 { get; private set; }

//     public ButtonControl button2 { get; private set; }

//     public StickControl stick { get; private set; }


//     static ArcadeGamepad() {
//         // InputSystem.RegisterLayout<DualShock4GamepadHID>(
//         //     new InputDeviceMatcher()
//         //         .WithInterface("HID")
//         //         .WithManufacturer("Sony.+Entertainment")
//         //         .WithProduct("Wireless Controller"));

//         InputSystem.RegisterLayout<ArcadeGamepad>(
//             matches: new InputDeviceMatcher()
//                 .WithInterface("HID")
//                 .WithCapability("vendorId", 0x79)
//                 .WithCapability("productId", 0x6));
//         // InputSystem.RegisterLayout<ArcadeGamepad>();


//         // if ( !InputSystem.devices.Any(x => x is ArcadeGamepad) ) {
//         //     InputSystem.AddDevice<ArcadeGamepad>();
//         // }
//     }

//     protected override void FinishSetup() {
//         base.FinishSetup();

//         button1 = GetChildControl<ButtonControl>("button2");
//         button2 = GetChildControl<ButtonControl>("button3");

//         stick = GetChildControl<StickControl>("stick");
//     }

//     [MenuItem("Arcade/Add Gamepad")]
//     [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
//     public static void Initialize() {
//         InputSystem.RegisterLayout<ArcadeGamepad>();
//         InputSystem.AddDevice<ArcadeGamepad>();
//     }

// }

// public struct ArcadeGamepadState : IInputStateTypeInfo {
//     public FourCC format => new FourCC('H', 'I', 'D');


//     [InputControl(name = "button2", displayName = "Button 1", layout = "Button", bit = 0)]
//     [InputControl(name = "button3", displayName = "Button 2", layout = "Button", bit = 1)]
//     public int buttons;

//     [InputControl(displayName = "Stick", layout = "Stick", type = "StickControl", processors = "stickDeadzone", format = "VEC2", offset = 1, sizeInBits = 16)]
//     public Vector2 stick;


// }





// // {
// //     "name": "ArcadeGamepad",
// //     "extend": "",
// //     "extendMultiple": [],
// //     "format": "MYDV",
// //     "beforeRender": "",
// //     "runInBackground": "",
// //     "commonUsages": [],
// //     "displayName": "",
// //     "description": "",
// //     "type": "ArcadeGamepad, Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null",
// //     "variant": "",
// //     "isGenericTypeOfDevice": false,
// //     "hideInUI": false,
// //     "controls": [
// //         {
// //             "name": "button1",
// //             "layout": "Button",
// //             "variants": "",
// //             "usage": "",
// //             "alias": "",
// //             "useStateFrom": "",
// //             "offset": 0,
// //             "bit": 0,
// //             "sizeInBits": 0,
// //             "format": "",
// //             "arraySize": 0,
// //             "usages": [],
// //             "aliases": [],
// //             "parameters": "",
// //             "processors": "",
// //             "displayName": "Button 1",
// //             "shortDisplayName": "",
// //             "noisy": false,
// //             "dontReset": false,
// //             "synthetic": false,
// //             "defaultState": "",
// //             "minValue": "",
// //             "maxValue": ""
// //         },
// //         {
// //             "name": "button2",
// //             "layout": "Button",
// //             "variants": "",
// //             "usage": "",
// //             "alias": "",
// //             "useStateFrom": "",
// //             "offset": 0,
// //             "bit": 1,
// //             "sizeInBits": 0,
// //             "format": "",
// //             "arraySize": 0,
// //             "usages": [],
// //             "aliases": [],
// //             "parameters": "",
// //             "processors": "",
// //             "displayName": "Button 2",
// //             "shortDisplayName": "",
// //             "noisy": false,
// //             "dontReset": false,
// //             "synthetic": false,
// //             "defaultState": "",
// //             "minValue": "",
// //             "maxValue": ""
// //         },
// //         {
// //             "name": "stick",
// //             "layout": "Stick",
// //             "variants": "",
// //             "usage": "",
// //             "alias": "",
// //             "useStateFrom": "",
// //             "offset": 1,
// //             "bit": 4294967295,
// //             "sizeInBits": 0,
// //             "format": "VEC2",
// //             "arraySize": 0,
// //             "usages": [
// //                 "Secondary2DMotion"
// //             ],
// //             "aliases": [],
// //             "parameters": "",
// //             "processors": "stickDeadzone",
// //             "displayName": "Stick",
// //             "shortDisplayName": "",
// //             "noisy": false,
// //             "dontReset": false,
// //             "synthetic": false,
// //             "defaultState": "",
// //             "minValue": "",
// //             "maxValue": ""
// //         }
// //     ]
// // }