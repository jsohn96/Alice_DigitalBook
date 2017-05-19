using UnityEngine;
using System.Collections;

public class Timer {

	float prevActivationTime;
	float cooldownTime;
	float pauseTimePassed;
	bool paused = false;

	public Timer(float c) {
		cooldownTime = c;
		prevActivationTime = -c;
	}
	public float PercentTimePassed { get { 
			if (paused) {
				return pauseTimePassed / cooldownTime;
			} else {
				return TimePassed / cooldownTime; 
			}
		} }
	public float PercentTimeLeft { get { 
			if (paused) {
				return (cooldownTime - pauseTimePassed) / cooldownTime;
			} else {
				return TimeLeft / cooldownTime; 
			}
		} }
	public float TimeLeft { get { 
			if (paused) {
				return cooldownTime - pauseTimePassed;
			} else {
				return cooldownTime - Mathf.Min (Time.time - prevActivationTime, cooldownTime);
			}
		} }
	public float TimePassed { get { 
			if (paused) {
				return pauseTimePassed;
			} else {
				return Mathf.Min (Time.time - prevActivationTime, cooldownTime); 
			}
		} }
	public bool IsOffCooldown { get { 
			if (paused) {
				return false;
			} else {
				return Time.time - prevActivationTime > cooldownTime; 
			}
		} }
	public float CooldownTime { get { return cooldownTime; } set { cooldownTime = value; } }
	public void Reset() { prevActivationTime = Time.time; }

	public void Pause(){
		if (!paused) {
			paused = true;
			pauseTimePassed = Mathf.Min (Time.time - prevActivationTime, cooldownTime);
		}
	}

	public void Resume(){
		if (paused) {
			if (pauseTimePassed == cooldownTime) {
				pauseTimePassed = 0.0f;
			}
			paused = false;
			prevActivationTime = Time.time - pauseTimePassed;
		}
	}

	public bool IsPaused { get { return paused; } }

}