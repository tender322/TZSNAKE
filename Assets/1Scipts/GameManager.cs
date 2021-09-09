using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PlayerFab;
    [SerializeField] [Range(0, 500)] private float speed;
    private IController Controller;
    private Camera cam;
    private void Start()
    {
        GameObject fab = Instantiate(PlayerFab);
        fab.transform.position = new Vector3(0,1.1f,0);
        Controller = fab.GetComponent<IController>();
        cam = Camera.main;
    }


    private void Update()
    {
        Vector3 _vector = new Vector3(0, 0, 1);
        Vector3 _mouse = Vector3.zero;

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            _mouse = hit.point;
        }

        Controller.move(_vector * speed * Time.deltaTime, _mouse.x);
        _mouse = Vector3.zero;
        Vector3 pos = Controller.getPos();
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, pos.z - 2);
    }
}
