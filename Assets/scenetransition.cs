using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets._2D;

public class scenetransition : MonoBehaviour {

    public string sceneName;
    PlatformerCharacter2D script;
    public bool keycheck;

    // Use this for initialization
    void Start () {
        GameObject g = GameObject.Find("CharacterRobotBoy");
        script = g.GetComponent<PlatformerCharacter2D>();
        

    }
    void OnTriggerStay2D(Collider2D other)
    {
        //Check to see if a player entered the door.
        if (keycheck == true) {
            if (other.gameObject.tag == "Player" && Input.GetKeyDown("e"))
            {
                Debug.Log("hello");
                SceneManager.LoadScene(sceneName);

            }
        }
    }

    // Update is called once per frame
    void Update () {
        keycheck = script.key;
    }
}
