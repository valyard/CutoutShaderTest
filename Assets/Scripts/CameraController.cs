using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	void Awake() {
		GetComponent<Camera>().orthographicSize = Screen.height / 2;
	}
}
