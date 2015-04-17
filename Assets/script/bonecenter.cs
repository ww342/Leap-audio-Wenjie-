using UnityEngine;
using System.Collections;
using Leap;

public class bonecenter : MonoBehaviour
{
	Controller Controller = new Controller ();
	public float smooth;
	public Finger.FingerType fingerType;
	public Bone.BoneType BoneType;
	
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
			transform.position = xBasis * 0.05f;
		}
	}
}
