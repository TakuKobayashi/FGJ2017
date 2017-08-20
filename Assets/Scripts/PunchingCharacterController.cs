using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class PunchingCharacterController : MonoBehaviour {
	[SerializeField] MouseLook mouseLook;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject characterObject;
    [SerializeField] HandController handController;
    [SerializeField] bool mouseLookEnable;

    private Vector3 prevCharacterPosition;

	// Use this for initialization
	void Start () {
		mouseLook.Init(transform, mainCamera.transform);
        prevCharacterPosition = characterObject.transform.position;
        OnPunchHit();
	}
	
	// Update is called once per frame
	void Update () {
        if(mouseLookEnable){
			mouseLook.LookRotation(transform, mainCamera.transform);
        }
        handController.transform.position = handController.transform.position + DiffCharacterObjectPosition();
		mainCamera.transform.position = mainCamera.transform.position + DiffCharacterObjectPosition();
		prevCharacterPosition = characterObject.transform.position;
	}

	void FixedUpdate()
	{
        if (mouseLookEnable)
        {
            mouseLook.UpdateCursorLock();
        }
	}


    void OnPunchHit(){
        ShootCharacter();
    }

    private void ShootCharacter(){
		Rigidbody rigidbody = characterObject.AddComponent<Rigidbody>();
		Vector3 shootVector = Util.ShootVectorFromSpeed(characterObject.gameObject.transform.position, new Vector3(10, 0, -20), 3.0f);
		// 速さベクトルのままAddForce()を渡してはいけないぞ。力(速さ×重さ)に変換するんだ
		Vector3 force = shootVector * rigidbody.mass;
		rigidbody.AddForce(force, ForceMode.Impulse);
    }

    private Vector3 DiffCharacterObjectPosition(){
        return characterObject.transform.position - prevCharacterPosition;
    }

}
