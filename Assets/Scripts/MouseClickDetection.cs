using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickDetection : MonoBehaviour
{


	public void OnMouseDown()
	{
		if (gameObject.name == "Lid_Ctrl") {
			SceneManager.instance.Lid_Open_Close ();
		}
		else if (gameObject.name == "ScreenButtons") {
			SceneManager.instance.ScreenButtonPressed ();
		}

	}



}
