using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncSceneManager : MonoBehaviour
{
    [SerializeField] string sceneName;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void LoadScene(string name)
    {
        if (name.Length > 0)
        {
            SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
        }
        else
        {

            if (sceneName.Length > 0)
            {
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            }
            else
            {
                Debug.LogWarning("Bro you forgot to add a level name somewhere fuckin what are you doin");
            }
        }


    }
}
