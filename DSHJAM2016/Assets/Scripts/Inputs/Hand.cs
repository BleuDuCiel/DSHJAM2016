using System;
using UnityEngine;

namespace Inputs
{
	public abstract class Hand
	{
		private string input; // joystick "JS" or keyboard "KB"
		private string id; // id of the input, from InputManager

		public Hand (string input, string id)
		{
			this.input = input;
			this.id = id;
		}

		public bool getJump() {
			return Input.GetButtonDown(input + "Jump" + id);
		}

		public bool getItem() {
			return Input.GetButtonDown(input + "Item" + id);
		}

		public bool getAttack() {
			return Input.GetButtonDown(input + "Attack" + id);
		}

		public float getMove() {
			return Input.GetAxis(input + "Move" + id);
		}

		// Return the angle in rad [-π,π] of the left stick
		public double getAngle() {
			double x = Input.GetAxis (input + "Move" + id);
			double y = - Input.GetAxis (input + "Vertical" + id);

			// default value
			if (x == 0 && y == 0) return 0;
			// angle value
			return Math.Atan2 (y, x);
		}
	}

	public class RightHand : Hand
	{
		public RightHand (string input = "JS", string id = "0") : base(input, id) {}
	}

	public class LeftHand : Hand
	{
		public LeftHand (string input = "JS", string id = "0") : base(input, id) {}
	}
}

