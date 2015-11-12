using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Controller : MonoBehaviour {

	public Text Text;
	public Text CloneText;
	public int MaxLevels = 3;

	private Transform container;
	private GameObject template;

	private int currentLevel = 0;

	public void LoadNextLevel()
	{
		Application.LoadLevel(currentLevel+1);
		StartCoroutine(updateLevel());

		currentLevel = (currentLevel + 1) % MaxLevels;
	}

	public void Clone()
	{
		var clone = Instantiate(template) as GameObject;
		clone.transform.SetParent(container);
		clone.transform.localPosition = Vector3.zero + Vector3.forward + Vector3.left * (container.childCount % 10 - 5);
		updateCloneText();
	}

	private void Awake() 
	{
		DontDestroyOnLoad(gameObject);	

		LoadNextLevel();
	}

	private IEnumerator updateLevel()
	{
		yield return null;

		Text.text = "Current level: " + Application.loadedLevelName + ".\nClick to load next!";

		container = GameObject.Find("Container").transform;
		template = container.GetChild(0).gameObject;
		updateCloneText();
	}

	private void updateCloneText()
	{
		CloneText.text = "Objects: " + container.childCount + "\nClick to add.";
	}

}
