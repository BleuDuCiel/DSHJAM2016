using UnityEngine;
using System.Collections;
using Inputs;

public class Player
{


    public int number { get; set; }
    //public string prefixsuffix { get; set; }
	private LeftHand left;
	private RightHand right;

	public Player(int n, LeftHand left, RightHand right)
    {
        number = n;
		this.left = left;
		this.right = right;
        //prefixsuffix = ps;
    }

	public bool getJump() {
		return left.getJump();
	}

	public bool getItem() {
		return right.getItem();
	}

	public bool getAttack() {
		return right.getItem();
	}

	public float getMove() {
		return left.getMove();
	}
	/*
    public string getInputs()
    {
        return prefixsuffix;
    }
    */


}
