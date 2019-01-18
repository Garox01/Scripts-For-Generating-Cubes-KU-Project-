using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using UnityEngine.UI;

public class SpawnStatic : MonoBehaviour
{

    //List over the cubes that would be spawned
    public GameObject[] cubes;

    //List over the text that needs to be updated.
    public Text[] distance;
    public Text[] reachable;

    //The UI button that would spawn the cube.
    public Button spawn_cubes;

    //Bool for activating a cooldown for spawning cubes. 
    private bool cooldown = false;


    // Use this for initialization
    void Start()
    {
        //Makes the button useable.
        spawn_cubes.onClick.AddListener(TaskOnClick);
    }


    public void TaskOnClick()
    {

        if (cooldown == false)
        {
            //Uses the height from the player.
            float y = Information.height;

            for (float i = 0.0f; i < 10.0f;)
            {
                
                //Number of blocks destroyed by the user.
                int dc = Information.blocks_destroyed;
                
                //Generating a static pattern of cubes: 
                float x_s = -0.5f + (i/10);
                float y_s = y;
                float z_s = -1.5f + (i/10);

                
                Vector3 Randompoint = new Vector3(x_s, y_s, z_s);
                Vector3 Userpoint = new Vector3(0.0f, y, 0.0f);

                //If the distance is smaller than the person's arm reach. It gets divided by ten since each block destroyed is 10 cm.   
                if (dc / 10f >= Mathf.Sqrt((Mathf.Pow((0f - x_s), 2f) + Mathf.Pow((0f - z_s), 2f) + Mathf.Pow((y - y_s), 2f))))
                {
                    //Creates the block.
                    Instantiate(cubes[(int)i], new Vector3(x_s, y_s, z_s), Quaternion.identity);
                    //Changes Text. 
                    distance[(int)i].text = ((Vector3.Distance(Randompoint, Userpoint) * 100f).ToString());
                    reachable[(int)i].text = "true";

                }
                else
                {
                    //Creates the block
                    Instantiate(cubes[(int)i], new Vector3(x_s, y_s, z_s), Quaternion.identity);
                    //Changes Text. 
                    distance[(int)i].text = ((Vector3.Distance(Randompoint, Userpoint) * 100f).ToString());
                    reachable[(int)i].text = "false";

                }

                i++;

            }
            
            Invoke("reset_cooldown", 1.0f);
            cooldown = true;
        }

    }


    void reset_cooldown()
    {
        cooldown = false;

    }

}
