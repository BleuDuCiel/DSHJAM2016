using UnityEngine;
using System.Collections;
using Inputs;

public class Player
{
    public int number { get; set; }
	private LeftHand left;
	private RightHand right;

	public Player(int n, LeftHand left, RightHand right)
    {
        number = n;
		this.left = left;
		this.right = right;
    }

	// Getters of player inputs
	// Note: Config below which hand for which button 
	public bool getJump() {
		return right.getJump();
	}

	public bool getItem() {
		return right.getItem();
	}

	public bool getAttack() {
		return right.getAttack();
	}

	public float getMove() {
		return left.getMove();
	}

	// Return the angle in rad [-π,π] of the left stick
	public double getAngle() {
		return left.getAngle ();
	}

	// Swap the left hand between this player and other player
	public void swapLeftHand(Player other) {
		LeftHand tmp = this.left;
		this.left = other.left;
		other.left = tmp;
	}

	// Swap the right hand between this player and other player
	public void swapRightHand(Player other) {
		RightHand tmp = this.right;
		this.right = other.right;
		other.right = tmp;
	}
}
