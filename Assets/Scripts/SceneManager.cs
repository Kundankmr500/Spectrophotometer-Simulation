using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneManager : MonoBehaviour
{
	public static SceneManager instance;
	public Animator mainAnimatior;
	public Transform testTubeTarget1;
	public Transform testTubeTarget2;
	[HideInInspector]
	public Vector3 tubeDefaultPosition;
	[HideInInspector]
	public GameObject SelectedObject;
	public List<GameObject> TestTubes;
	public GameObject ResatButton;
	public TextMeshPro ScreenText;

	public bool isRightPlaced;
	public bool ObjectPlaced;

	void Awake()
	{
		instance = this;
	}


	void Start()
	{
		ResatButton.SetActive (false);
	}


	public void CloseApplication()
	{
		Application.Quit ();
	}


	public void ResatPosition()
	{
		ObjectPlaced = false;
		ResatButton.SetActive (false);
		SelectedObject.transform.DOMove(testTubeTarget1.position, .5f);
		StartCoroutine (MoveToDefaultPosition ());
	}


	IEnumerator MoveToDefaultPosition()
	{
		yield return new WaitForSeconds (.52f);
		SelectedObject.transform.DOMove (tubeDefaultPosition,1.5f);
		yield return new WaitForSeconds (1.51f);
		TubesColliderOn ();
	}


	public void TubesColliderOn()
	{
		for (int i = 0; i < TestTubes.Count; i++) {
			TestTubes [i].GetComponent<BoxCollider> ().enabled = true;
		}
	}


	public void TubsColliderOff()
	{
		for (int i = 0; i < TestTubes.Count; i++) {
			TestTubes [i].GetComponent<BoxCollider> ().enabled = false;
		}
	}

	/// <summary>
	/// This function is responsible for open close functionality.
	/// </summary>
	public void Lid_Open_Close()
	{
		if (mainAnimatior.GetBool ("Lid_Open")) {
			mainAnimatior.SetBool ("Lid_Open", false);
			ResatButton.SetActive (false);
		} 
		else 
		{
			mainAnimatior.SetBool ("Lid_Open", true);
			CheckForResatButton ();
		}
	}


	public void CheckForResatButton()
	{
		if (ObjectPlaced && mainAnimatior.GetBool ("Lid_Open")) 
		{
			ResatButton.SetActive (true);
		}
	}

	/// <summary>
	/// Pressed the screen Button.
	/// </summary>
	public void ScreenButtonPressed()
	{
		if (mainAnimatior.GetBool ("Lid_Open") || SelectedObject == null || !ObjectPlaced) {
			ScreenText.text = "Error";
			print (" 1 ");
		}
		else if(!isRightPlaced)
		{
			ScreenText.text = Random.Range(5.820f,6.180f).ToString();
			print (" 2 ");
		}
		else if(SelectedObject.name == "TestTubeSample_1")
		{
			ScreenText.text = Random.Range(0.000f,0.003f).ToString();
			print (" 3 ");
		}
		else if(SelectedObject.name == "TestTubeSample_2")
		{
			ScreenText.text = Random.Range(0.097f,0.103f).ToString();
			print (" 4 ");
		}
		else if(SelectedObject.name == "TestTubeSample_3")
		{
			ScreenText.text = Random.Range(0.194f,0.206f).ToString();
			print (" 5 ");
		}
		else if(SelectedObject.name == "TestTubeSample_4")
		{
			ScreenText.text = Random.Range(0.291f,0.309f).ToString();
			print (" 6 ");
		}
		else if(SelectedObject.name == "TestTubeSample_5")
		{
			ScreenText.text = Random.Range(0.422f,0.448f).ToString();
			print (" 7 ");
		}
	}

}
