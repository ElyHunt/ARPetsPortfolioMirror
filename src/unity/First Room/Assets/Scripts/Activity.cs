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

    public void feed()
    {
        location = GameObject.Find("bowl (1)");
        goal.destination = location.transform;
        trigger.isEnabled = false;
        return;
    }

    public void fetch(){
        StartCoroutine(fetchWait());
    }

    public IEnumerator fetchWait()
    {
        location = GameObject.Find("Sphere");
        goal.destination = location.transform;
        trigger.isEnabled = false;
        while (!trigger.isEnabled)
        {
            yield return null;
        }
        GameObject temp = GameObject.Find("Room/Player/SteamVRObjects/VRCamera");
        //Just trying to get rid of obj to random place...
        location.transform.position = new Vector3(35,35,35);
        goal.destination = temp.transform;
        trigger.isEnabled = false;
        while(!trigger.isEnabled){
            yield return null;
        }
        //And return it
        //location.SetActive(true);
        Vector3 local = new Vector3(0.114f, -.26653f, -8.496f);
        location.transform.position = local;
    }
}
