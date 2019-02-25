using UnityEngine;
using System.Collections;

public class ApplicationManager : MonoBehaviour {
	
    public void Continue ()
    {
        Debug.Log("CLICKED CONTINUE YAY");

    }//Stub for Continue.

    //Functionality added via Inspector for object
        // > On Click()
            // + Button
                // Add Application Manager from Scene
                // Editor and Runtime
                // ApplicationManager.Continue
                // SOMETHING LIKE THIS!
                // Now just add read and write to file.
                // Can be ported over to the main project
                // rather than demo scene for menu. Cool!
                //  - E.H.
    public void Quit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
