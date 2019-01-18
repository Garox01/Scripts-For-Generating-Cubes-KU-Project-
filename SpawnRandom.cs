using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using UnityEngine.UI;

public class SpawnRandom : MonoBehaviour
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

    //When the button is clicked.
    public void TaskOnClick()
    {
        if (cooldown == false)
        {
            //Uses the height from the player.
            float y = Information.height;
            
            //Creates 10 cubes. 
            for (int i = 0; i < 10;)
            {

                //Number of blocks destroyed by the user.
                int dc = Information.blocks_destroyed;

                //Generating random x,y,z values for the block: 
                float x_s = Random.Range(-1.2f, 1.2f);
                float y_s = Random.Range(min_height(), max_height());
                float z_s = Random.Range(-1.2f, 1.2f);
                

                Vector3 Randompoint = new Vector3(x_s, y_s, z_s);
                Vector3 Userpoint = new Vector3(0.0f, y, 0.0f);

                //If the distance is smaller than the person's arm reach. It gets divided by ten since each block destroyed is 10 cm.   
                if (dc / 10f >= Mathf.Sqrt((Mathf.Pow((0f - x_s), 2f) + Mathf.Pow((0f - z_s), 2f) + Mathf.Pow((y - y_s), 2f))))
                {
                    //Creates the block.
                    Instantiate(cubes[i], new Vector3(x_s, y_s, z_s), Quaternion.identity);
                    //Changes Text.                
                    distance[i].text = ((Vector3.Distance(Randompoint, Userpoint) * 100f).ToString());
                    reachable[i].text = "true";
                }
                else
                {
                    //Creates the block.
                    Instantiate(cubes[i], new Vector3(x_s, y_s, z_s), Quaternion.identity);
                    //Changes Text.
                    distance[i].text = ((Vector3.Distance(Randompoint, Userpoint) * 100f).ToString());
                    reachable[i].text = "false";
                }

                i++;

            }
           
            Invoke("reset_cooldown", 1.0f);
            cooldown = true;
        }
    }

    //Makes sure that blocks won't spawn under the floor or above ceiling
    float min_height()
    {
        int dc = Information.blocks_destroyed;
        float y = Information.height;

        if (y - (dc / 10.0f) < 0)
        {
            return 0.0f;
        }
        else
        {
            return y - (dc / 10.0f);
        }
    }

    float max_height()
    {
        int dc = Information.blocks_destroyed;
        float y = Information.height;

        if (y + (dc / 10f) > 2.8f)
        {
            return 2.8f;
        }
        else
        {
            return y + (dc / 10f);
        }
    }


    void reset_cooldown()
    {
        cooldown = false;
        
    }

}
