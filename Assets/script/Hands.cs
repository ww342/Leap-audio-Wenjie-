using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Hands : MonoBehaviour
{
	public Sounds Sounds;
	public Narrator Narrator;
	public Metrics Metrics;
	

	


	public void BellCount ()
	{
		Metrics.bellcount ++;

		if (Metrics.bellcount == 1) {
			Sounds.Ambience_B.minDistance = 10;
		}
		if (Metrics.bellcount == 3) {
			Sounds.Ambience_D.PlayOneShot (Sounds.lightning);
		}

		if (Metrics.bellcount == 4) {
		}
		
		if (Metrics.bellcount == 5) {

			Sounds.Ambience_D.PlayOneShot (Sounds.wind);
			Sounds.Ambience_B.clip = Sounds.grass;
			Sounds.Ambience_B .Play ();
		}

		if (Metrics.bellcount == 6) {

			Sounds.Ambience_D.PlayOneShot (Sounds.brake);
			//yield return Narrator.PlayAndWait(Narrator.bell6);

			//LevelCount ();
		}
	}
	
	public void StarCount ()
	{
		Metrics.starcount ++;

		if (Metrics.starcount == 1) {

			//yield return Narrator.PlayAndWait(Narrator.star1);

			Sounds.Ambience_D.PlayOneShot (Sounds.lightning);
			Sounds.Ambience_D.PlayOneShot (Sounds.footstep);
		}


		if (Metrics.starcount == 2) {

			//yield return Narrator.PlayAndWait(Narrator.star2);

			Sounds.Ambience_D.PlayOneShot (Sounds.footstep);
		}

		
		if (Metrics.starcount == 3) {
			//yield return Narrator.PlayAndWait(Narrator.star3);

			Invoke ("Glow", 2);
			Invoke ("Timetravel", 6);
			Invoke ("watchstop", 8);
			//LevelCount ();
		}
	}

}
