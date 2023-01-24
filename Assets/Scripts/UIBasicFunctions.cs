using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace FarmManagerWorld.UI
{
    public class UIBasicFunctions : MonoBehaviour
    {

        public bool visible = false;
        
        public void LoadScene(string level)
        {
            SceneManager.LoadScene(level);
        }

        public void ExitScene()
        {
            Debug.Log("Exited");
            Application.Quit();
        }
        
        public void Load()
        {
            if (visible)
            {
                this.gameObject.SetActive(false); visible = false;
            }
            else
            {
                this.gameObject.SetActive(true);
                visible = true;
            }
        }
        public void Disable()
        {
            this.gameObject.SetActive(false); visible = false;
        }

        public void Enable()
        {
            this.gameObject.SetActive(false); visible = true;
        }

        


    }
}   