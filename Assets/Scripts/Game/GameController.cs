using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public MenuController menu;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            PauseResume();
        }
    }

    public void PauseResume(){
        if(Time.timeScale == 1){
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
        menu.ToggleMenu();
    }
}
