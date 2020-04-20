using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{

    private Camera cam;
    public GameObject gameUI, startBarrier, MenuUI;


    private void Awake() {
        cam = Camera.main;
        startBarrier.SetActive(true);
    }

    private void Start() {
        cam.GetComponent<Animator>().Play("CameraStart");

    }

    public void OnStart() {
        gameUI.SetActive(true);
        Destroy(startBarrier.gameObject);
        Destroy(MenuUI.gameObject);
    }

}
