using UnityEngine;
using System.Collections;
using Leap;

public class bonestart1 : MonoBehaviour {
	Controller Controller = new Controller();
	public float smooth ;
	public Finger.FingerType fingerType;
	public Bone.BoneType BoneType ;
	public Hands script;

	// Use this for initialization
	
	void Start () {
		Controller = new Controller ();
	}
	
	// Update is called once per frame
	void Update () {
		Frame startframe = Controller.Frame();
		Hand leftmost = startframe.Hands.Leftmost ;
		
		
		//if (script.levelcount == 2 || script.levelcount == 3) {	
						if ((leftmost.IsLeft) && (startframe.Hands.Count > 0)) {

				Finger finger_ = leftmost.Fingers[(int)fingerType];
				Bone bone = finger_.Bone(BoneType);
				
				float Roll = bone.Direction.Roll* 180.0f/Mathf.PI ;
				float Yaw = bone.Basis.yBasis.Yaw* 180.0f/Mathf.PI ;
				float Pitch = bone.Direction.Pitch* 180.0f/Mathf.PI ;
				Quaternion twist = Quaternion.Euler (-Pitch, Yaw, Roll);
				//float bonemove_x= bone.Direction.
				
				
				float boneStart_x = bone.PrevJoint.x;
				float boneStart_y = bone.PrevJoint.y;
				float boneStart_z = bone.PrevJoint.z;
				float boneLength = bone.Length;
				Vector3 boneStart = new Vector3 (boneStart_x , boneStart_y , -boneStart_z);

			
								//transform.rotation = Quaternion.Slerp (transform.rotation, twist, Time.deltaTime * smooth); 
								transform.position = boneStart * 0.05f;
								/*
						 if (NUM_JOINTS >= NUM_BONES) {
								transform.position = finger_.Bone ((Bone.BoneType.TYPE_DISTAL)).NextJoint;
						}
						if (NUM_JOINTS < NUM_BONES) {
								transform.position = finger_.Bone ((Bone.BoneType.TYPE_DISTAL)).PrevJoint;
						}
			            */
			
						}


				//}
	}
	
}

