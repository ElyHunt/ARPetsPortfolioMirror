using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHover : MonoBehaviour
{
    public float sightlength = 100f;
    public GameObject selectedObj;
    public float hoverForwardDistance = .5f;
    void FixedUpdate() {
        RaycastHit seen;
        Ray raydirection = new Ray(transform.position, transform.forward);
        if(Physics.Raycast(raydirection, out seen, sightlength)) {
            if(seen.collider.tag == "Button") {
                if(selectedObj != null && selectedObj != seen.transform.gameObject) {
                    GameObject hitObject = seen.transform.gameObject;
                    MoveMenuButton(hitObject);
                }
                selectedObj = seen.transform.gameObject;
            }
        }
    }

    private void MoveMenuButton(GameObject hitObject) {
        Vector3 newZ = hitObject.transform.position;
        newZ.z -= hoverForwardDistance;
        selectedObj.transform.position = newZ;

        newZ.z += hoverForwardDistance * 2;
        hitObject.transform.position = newZ;
    }
}
