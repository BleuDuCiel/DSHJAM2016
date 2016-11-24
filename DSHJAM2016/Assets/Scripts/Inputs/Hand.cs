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

