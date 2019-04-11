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
        location.transform.position = new Vector3(35f,35f,35f);
        goal.destination = temp.transform;
        trigger.isEnabled = false;
        while(!trigger.isEnabled){
            yield return null;
        }
        //And return it
        //location.SetActive(true);
        //location.transform.
        location.transform.localRotation = new Quaternion(0,0,0,0);
        location.transform.localPosition = new Vector3(0.114f, -.16653f, -8.496f);
        //location.transform.localPosition = new Vector3(0f, 1f, 0f);//
    }
}
