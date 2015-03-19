using UnityEngine;
using System.Collections;
using Leap;

public class GrabandDrop : MonoBehaviour {
	//Controller Controller = new Controller ();
	GameObject grabbedObject;
	float grabbedObjectSize;

	// Use this for initialization
	void Start () {

		/*
		Controller.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
		Controller.Config.SetFloat("Gesture.ScreenTap.MinForwardVelocity", 30.0f);
		Controller.Config.SetFloat("Gesture.ScreenTap.HistorySeconds", .5f);
		Controller.Config.SetFloat("Gesture.ScreenTap.MinDistance", 1.0f);
		Controller.Config.Save();
		*/

	}

	GameObject GetMouseHoverObject(float range){

		Vector3 position = gameObject.transform.position;

		RaycastHit raycastHit;

		Vector3 target = position + Camera.main.transform.forward * range;


		if (Physics.Linecast (position, target, out raycastHit))

		return raycastHit.collider.gameObject;

		return null;

	}

	void TryGrabObject(GameObject grabObject)
	{
				if (grabObject == null)

				return;

				grabbedObject = grabObject;

		        grabbedObjectSize = grabObject.renderer.bounds.size.magnitude;

	}

	void DropObject()
	{
				if (grabbedObject == null)
				return;
				grabbedObject = null;
    }


	// Update is called once per frame
	void Update () {

		Debug.Log (GetMouseHoverObject (5));

			if (Input.GetMouseButtonDown (1)) {

						if (grabbedObject == null) 

							TryGrabObject (GetMouseHoverObject (5));
						 
			            else 
								DropObject ();

						}
				
		                if(grabbedObject != null)

		                {

			            Vector3 newPosition = gameObject.transform.position + Camera.main.transform.forward * grabbedObjectSize;

			            grabbedObject.transform.position =  newPosition;

              }


	}
}