using UnityEngine;
using System.Collections;
using Leap;

public class boneend : MonoBehaviour
{
	Controller Controller = new Controller ();
	public float smooth ;
	public Finger.FingerType fingerType;
	public Bone.BoneType BoneType ;

	void Update ()
	{
		Frame startframe = Controller.Frame ();
		Hand rightmost = startframe.Hands.Rightmost;

		if ((rightmost.IsRight) && (startframe.Hands.Count > 0)) {
			Finger finger_ = rightmost.Fingers [(int)fingerType];
			Bone bone = finger_.Bone (BoneType);

			float Roll = bone.Direction.Roll * 180.0f / Mathf.PI;
			float Yaw = bone.Basis.yBasis.Yaw * 180.0f / Mathf.PI;
			float Pitch = bone.Direction.Pitch * 180.0f / Mathf.PI;
			Quaternion twist = Quaternion.Euler (Pitch, Yaw, -Roll);

			float boneEnd_x = bone.NextJoint.x;
			float boneEnd_y = bone.NextJoint.y;
			float boneEnd_z = bone.NextJoint.z;
			float boneLength = bone.Length;
			Vector3 boneEnd = new Vector3 (boneEnd_x, boneEnd_y, -boneEnd_z);

			transform.position = boneEnd * 0.05f;
		}
	}	
}

