using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{

    private Camera cam;
    private GameObject gameUI;
    private GameObject StartBarrier;


    private void Awake() {
        cam = Camera.main;
        gameUI = GameObject.FindGameObjectWithTag("GameUI");
        StartBarrier = GameObject.FindGameObjectWithTag("Barrier");
        gameUI.SetActive(false);
        StartBarrier.SetActive(true);
    }

    private void Start() {
        cam.GetComponent<Animation>().Play("CameraStart");
    }

    public void OnStart() {
        gameUI.SetActive(true);
        Destroy(StartBarrier.gameObject);
    }


}
