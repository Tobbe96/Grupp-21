using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    // Update is called once per frame
    void Update()
    {
        if (GetComponent < Quest_DoorToNextLvl> () ==true)
        {
            transition.SetTrigger ("Start");
        }

    }

    IEnumerator LoadLevel (int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        
    }
}
