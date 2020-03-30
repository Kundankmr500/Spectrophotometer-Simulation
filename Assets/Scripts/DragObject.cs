using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class DragObject : MonoBehaviour
{

	private Vector3 mOffset;

	private float mZcoord;
	bool canRotate = true;
	bool isDragging = false;

	Ray ray;
	RaycastHit hit;


	void OnMouseDown()
	{
		SceneManager.instance.SelectedObject = gameObject;
		SceneManager.instance.tubeDefaultPosition = transform.position;
		mZcoord = Camera.main.WorldToScreenPoint (transform.position).z;
		SceneManager.instance.TubsColliderOff ();
		mOffset = transform.position - GetMouseWorldPos ();
		isDragging = true;
//		print ("On Mouse Down Called");
	}


	void OnMouseUp()
	{
		isDragging = false;
//		print ("Up Called");
		CheckingForTargetCollider ();

	}


	Vector3 GetMouseWorldPos()
	{
		Vector3 mousePoint = Input.mousePosition;

		mousePoint.z = mZcoord;

		return Camera.main.ScreenToWorldPoint (mousePoint);
	}

    
	void OnMouseDrag()
	{
		if (canRotate) {
			transform.position = GetMouseWorldPos () + mOffset;
		}
//		print ("On Mouse Drag Called");
	}



	void OnMouseOver () {
		if (Input.GetMouseButtonDown(1) && canRotate && !isDragging)
		{
			canRotate = false;
			transform.DOLocalRotate(new Vector3 (0,transform.localEulerAngles.y + 90,0), 1f).OnComplete(TestTubeCanRotate);
		}
	}

	void TestTubeCanRotate()
	{
		canRotate = true;
//		print ("transform.localEulerAngles.y " + Math.Round(transform.localEulerAngles.y));
	}


	void CheckingForTargetCollider()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit))
		{
//			Debug.Log(hit.collider.name);
			if (hit.collider.name == "TestTubeTargetPosition") {
				StartCoroutine (MoveToTargetPosition ());
			} 
			else 
			{
				StartCoroutine (MoveToDefaultPosition ());
			}
		}
	}


	IEnumerator MoveToTargetPosition()
	{
		transform.DOMove (SceneManager.instance.testTubeTarget1.position,.5f);
		yield return new WaitForSeconds (.52f);
		transform.DOMove (SceneManager.instance.testTubeTarget2.position,1.5f);
		yield return new WaitForSeconds (1.5f);
		CheckForRightPlace ();
	}


	void CheckForRightPlace()
	{
		SceneManager.instance.ObjectPlaced = true;
		if (Math.Round(transform.localEulerAngles.y) % 20 != 0) {
			SceneManager.instance.isRightPlaced = true;
//			print ("isRightPlaced =  true " + Math.Round(transform.localEulerAngles.y) % 20);
		} else {
			SceneManager.instance.isRightPlaced = false;
//			print ("isRightPlaced =  false " + Math.Round(transform.localEulerAngles.y) % 20);
		}
		SceneManager.instance.CheckForResatButton ();
	}


	IEnumerator MoveToDefaultPosition()
	{
		if (transform.position != SceneManager.instance.tubeDefaultPosition) 
		{
			transform.DOMove (SceneManager.instance.tubeDefaultPosition,1.5f);
			yield return new WaitForSeconds (1.5f);
		}

		SceneManager.instance.TubesColliderOn ();
	}

}
