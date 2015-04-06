using UnityEngine;
using System.Collections;

public class Gain1 : MonoBehaviour {
	
	public float gain;
	
	
	void OnAudioFilterRead(float[] data, int channels)
	{
		for (var i = 0; i < data.Length; ++i)
			data[i] = data[i] * gain;			
	}
}