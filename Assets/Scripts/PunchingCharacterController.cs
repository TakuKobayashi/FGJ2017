using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;
using System.Collections;

public class PunchingCharacterController : MonoBehaviour {
	[SerializeField] MouseLook mouseLook;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject characterObject;
    [SerializeField] HandController handController;
    [SerializeField] GameObject bodyHitSoundObj;
    [SerializeField] bool mouseLookEnable;

    private bool isMove = false;
    private Vector3 prevCharacterPosition;

	// Use this for initialization
	void Start () {
		mouseLook.Init(transform, mainCamera.transform);
        prevCharacterPosition = characterObject.transform.position;
	}

    private IEnumerator PlayHitSoundCorutine(){
        GameObject soundobj = Util.InstantiateTo(this.gameObject, bodyHitSoundObj);
        AudioSource audioSource = soundobj.GetComponent<AudioSource>();
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(soundobj);
    }
	
	// Update is called once per frame
	void Update () {
        if(mouseLookEnable){
			mouseLook.LookRotation(transform, mainCamera.transform);
        }
        HandModel[] hands = handController.GetAllPhysicsHands();
        for (int i = 0; i < hands.Length;++i){
            hands[i].palm.GetComponent<PunchedObject>().OnHit = this.OnPunchHit;
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


    void OnPunchHit(Collision collision){
        if(!isMove){
			isMove = true;
            StartCoroutine(PlayHitSoundCorutine());
			ShootCharacter();
        }
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
