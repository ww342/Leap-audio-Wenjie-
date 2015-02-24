using UnityEngine;
using System.Collections;
using Leap;

public class bonecenter : MonoBehaviour
{
	Controller Controller = new Controller ();
	public float smooth;
	public Finger.FingerType fingerType;
	public Bone.BoneType BoneType;
	
	// Use this for initialization
	void Start ()
	{
		Controller = new Controller ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Frame startframe = Controller.Frame ();
		Hand rightmost = startframe.Hands.Rightmost;
		
		Finger finger_ = startframe.Fingers [(int)fingerType];
		Bone bone = finger_.Bone (BoneType);
		
		float xBasis_x = bone.Direction.x;
		float xBasis_y = bone.Direction.y;
		float xBasis_z = bone.Direction.z;
		Vector3 xBasis = new Vector3 (-xBasis_x, xBasis_y, xBasis_z);

		if ((rightmost.IsRight) && (startframe.Hands.Count > 0)) {
			//transform.rotation = Quaternion.Slerp (transform.rotation, twist, Time.deltaTime * smooth); 
			transform.position = xBasis * 0.05f;
/*
			if (NUM_JOINTS >= NUM_BONES) {
				transform.position = finger_.Bone ((Bone.BoneType.TYPE_DISTAL)).NextJoint;
			}
			if (NUM_JOINTS < NUM_BONES) {
				transform.position = finger_.Bone ((Bone.BoneType.TYPE_DISTAL)).PrevJoint;
			}
*/
		}
	}
}
