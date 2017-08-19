using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class PunchingCharacterController : MonoBehaviour {
	[SerializeField] MouseLook mouseLook;
    [SerializeField] Camera mainCamera;

	// Use this for initialization
	void Start () {
		mouseLook.Init(transform, mainCamera.transform);
	}
	
	// Update is called once per frame
	void Update () {
        mouseLook.LookRotation(transform, mainCamera.transform);
	}

	private void FixedUpdate()
	{
		mouseLook.UpdateCursorLock();
	}
}
