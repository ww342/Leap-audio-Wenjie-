using UnityEngine;
using System;  // Needed for Math

public class Sinus : MonoBehaviour
{
	// un-optimized version
	public double frequency = 0;
	public double gain = 0.05;
	
	private double increment;
	private double phase;
	private double sampling_frequency = 48000;

	public void OnAudioFilterRead(float[] data, int channels)
	{
		// update increment in case frequency has changed
		increment = frequency * 2 * Math.PI / sampling_frequency;
		for (var i = 0; i < data.Length; i = i + channels)
		{
			phase = phase + increment;
			// this is where we copy audio data to make them “available” to Unity
			data[i] = (float)(gain*Math.Sin(phase));
			// if we have stereo, we copy the mono data to each channel
			if (channels == 2) data[i + 1] = data[i];
			if (phase > 2 * Math.PI) phase = 0;
		}
	}

	public void Reset()
	{
		frequency = 0;
		gain = 0;
	}

	public void SetPitch(float pitch)
	{
		pitch = Mathf.Clamp(pitch, 1, 100);
		frequency = 400.0 + (500.0 * pitch / 100.0); // ranges from 405 to 900
		gain = 0.01 + (0.09 * pitch / 100.0); // ranges from 0.0109 to 0.1
	}
} 

