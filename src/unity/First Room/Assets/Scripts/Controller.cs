using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Controller : MonoBehaviour
{
    GameObject panel;
    public SteamVR_Action_Vector2 joystickAction;
    void Start() {
        
    }
    void Update() {
        if(SteamVR_Input._default.inActions.OpenMenu.GetStateDown(SteamVR_Input_Sources.Any)) {
            
        }
        Vector2 stick = joystickAction.GetAxis(SteamVR_Input_Sources.Any);
        if(stick != Vector2.zero){
            if(stick == Vector2.up){

            }
            else if(stick == Vector2.down){

            }
        }
        if(SteamVR_Input._default.inActions.InteractUI.GetStateDown(SteamVR_Input_Sources.Any)) {

        }
    }
}
