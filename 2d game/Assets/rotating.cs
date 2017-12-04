using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;


public class rotating : MonoBehaviour {

    PlatformerCharacter2D script;
    public bool rotatecheck = false;
	// Use this for initialization
	void Start () {
        GameObject g = GameObject.Find("CharacterRobotBoy");
        script = g.GetComponent<PlatformerCharacter2D>();

    }
	
	// Update is called once per frame
	void Update () {
        rotatecheck = script.rotatey;
    

        if (rotatecheck)
        {
            transform.Rotate(Vector3.forward * -90f);
            script.rotatey = false;
        }
    }
}
