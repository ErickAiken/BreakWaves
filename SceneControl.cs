using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    void Start(){
      unloadAllScenesExcept("Main Screen");
    }//

    //Main title screen
    public void scene0()
    {
        SceneManager.LoadScene("Main Screen");
        unloadAllScenesExcept("Main Screen");
    }

    //Sample scene
    public void scene1()
    {
        SceneManager.LoadScene("Game Screen");
        unloadAllScenesExcept("Game Screen");
    }

    //In game help menu
    public void scene2()
    {
        //SceneManager.LoadScene("Help Screen")
    }

    public void unloadAllScenesExcept(string sceneName){
        int N = SceneManager.sceneCount;
          for(int i = 0; i < N; i++){
              Scene scene = SceneManager.GetSceneAt(i);
              if(scene.name != sceneName){
                  SceneManager.UnloadSceneAsync(scene);
                  Debug.Log("Unloaded: " + scene.name);
              }//end if
          }//end i
    }//end unloadAllScenesExcept

}//end SceneControl
