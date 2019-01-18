using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using UnityEngine.UI;

public class SpawnCubesReach : MonoBehaviour {

    //Postion of the controller, used for measuring height
    public Transform Spawnpoint;
    
    //The cube that would be spawned
    public GameObject Prefab;
    
    //The UI button that would spawn the cube.
    public Button spawn_cubes; 

    //Bool for activating a cooldown for spawning cubes. 
    private bool cooldown = false;
     

	// Use this for initialization
	void Start () {
        //Makes the button useable. 
        spawn_cubes.onClick.AddListener(TaskOnClick);
    }
	
    //When the button is clicked.
    public void TaskOnClick()
    {
        //Finds the user's shoulder height.
        float y = Spawnpoint.position.y;

        //If there is no cooldown, then spawn the cubes. 
        if (cooldown == false)
        {
            //Resets the amount of blue blocks destroyed.
            Information.blocks_destroyed = 0;
            //Saves the user's height. 
            Information.height = y;

            //Creates 20 cubes in a straight line, at the height of the user's shoulder.
            for (float i = 0.0f; i < 2;)
            {
    
                Instantiate(Prefab, new Vector3(0.0f, y, -(i)), Quaternion.identity);
                i += 0.1f;
            }
            //Resets the cooldown.
            Invoke("reset_cooldown", 1.0f);
            cooldown = true;
        }
    }


    void reset_cooldown(){
        cooldown = false;
        
    }

}
