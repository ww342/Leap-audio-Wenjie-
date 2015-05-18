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
		if (pitch > 10 && pitch <= 20) {
			gain = 0.01;
			frequency = 450;
		}
		if (pitch > 20 && pitch <= 30) {
			gain = 0.02;
			frequency = 500;
		}
		if (pitch > 30 && pitch <= 40) {
			gain = 0.03;
			frequency = 550;
		}
		if (pitch > 40 && pitch <= 50) {
			gain = 0.04;
			frequency = 600;
		}
		if (pitch > 50 && pitch <= 60) {
			gain = 0.05;
			frequency = 650;
		}
		if (pitch > 60 && pitch <= 70) {
			gain = 0.06;
			frequency = 700;
		}
		if (pitch > 70 && pitch <= 80) {
			gain = 0.07;
			frequency = 750;
		}
		if (pitch > 80 && pitch <= 90) {
			gain = 0.08;
			frequency = 800;
		}
		if (pitch > 90 && pitch <= 100) { 
			gain = 0.09;
			frequency = 850;
		}
	}
} 

