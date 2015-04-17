using UnityEngine;
using System.Collections;
using Leap;

public class bonestart : MonoBehaviour
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

			float boneStart_x = bone.PrevJoint.x;
			float boneStart_y = bone.PrevJoint.y;
			float boneStart_z = bone.PrevJoint.z;
			float boneLength = bone.Length;
			Vector3 boneStart = new Vector3 (boneStart_x, boneStart_y, -boneStart_z);

			transform.position = boneStart * 0.05f;					
		}
	}
}
