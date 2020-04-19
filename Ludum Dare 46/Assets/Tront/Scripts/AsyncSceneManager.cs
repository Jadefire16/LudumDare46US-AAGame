using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AsyncSceneManager : MonoBehaviour
{


    public CanvasGroup FadeTransitionPanel;
    public TextMeshProUGUI text;

    public UnityEngine.UI.Image loadingbar;
    [SerializeField] string sceneName;


    // Start is called before the first frame update
    void Start()
    {
        FadeTransitionPanel.alpha = 0;
        loadingbar.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator LoadScene()
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        //Don't let the Scene activate until you allow it to
        //asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            text.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";
            loadingbar.fillAmount = asyncOperation.progress;
            yield return new WaitForEndOfFrame();

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                // asyncOperation.allowSceneActivation = true;
                //Change the Text to show the Scene is ready
                text.text = "ALMOST THERE LOL";
                //Wait to you press the space key to activate the Scene
                // if (Input.GetKeyDown(KeyCode.Space))
                //Activate the Scene
                //  asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public void LoadScene(string name)
    {
        FadeTransitionPanel.alpha = 1;

        StartCoroutine(LoadScene());
    }
}
