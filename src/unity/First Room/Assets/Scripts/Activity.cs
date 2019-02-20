using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activity : MonoBehaviour
{
    GameObject cat;
    GameObject location;
    aStar goal;
    BasicWalk trigger;
    // Start is called before the first frame update
    void Start()
    {
        cat = GameObject.Find("Cat Lite");
        goal = cat.GetComponent<aStar>();
        trigger = cat.GetComponent<BasicWalk>();
    }

    public void feed(){
        location = GameObject.Find("bowl (1)");
        goal.destination = location.transform;
        trigger.isEnabled = false;
        return;
    }
}
