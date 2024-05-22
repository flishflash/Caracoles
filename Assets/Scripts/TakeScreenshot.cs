using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeScreenshot : MonoBehaviour 
{
	GameObject canvas;

    private void Start()
    {
		canvas = GameObject.Find("Canvas");
    }

    public void TakeAShot()
	{
		StartCoroutine(CaptureIt());
	}

	IEnumerator CaptureIt()
	{
		canvas.SetActive(false);
		yield return new WaitForEndOfFrame();
		string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
		string fileName = "Screenshot" + timeStamp + ".png";
		string pathToSave = fileName;
		ScreenCapture.CaptureScreenshot(pathToSave);
		yield return new WaitForEndOfFrame();
		canvas.SetActive(true);
	}
}
