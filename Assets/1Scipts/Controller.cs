using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour,IController
{
    private Rigidbody rb;
    public GameObject TailFab;
    private List<GameObject> _tails = new List<GameObject>();
    private List<Vector3> _positions = new List<Vector3>();
    [SerializeField] private int maxDistance;
    private bool Fever;
    private int Crystal;
    private int People;
    private Text CrystalText;
    private Text PeopleText;


    private int iterpolation = 45;
    int elapTime = 0;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _tails.Add(this.gameObject);
        
        CrystalText = GameObject.Find("CrystalText").GetComponent<Text>();
        PeopleText = GameObject.Find("People").GetComponent<Text>();

    }

    public float smoothing;
    public void move(Vector3 _pos, float _mousepos)
    {
        if (!Fever)
        {
            for (int i = 1; i < _tails.Count; i++)
            {
                _tails[i].transform.position = _positions[(_positions.Count - 1) - i * maxDistance];
            }

           // transform.position = new Vector3(_mousepos, transform.position.y, transform.position.z + _pos.z);
           Vector3 _vec = new Vector3(_mousepos, transform.position.y, transform.position.z + _pos.z);
           transform.position = Vector3.Lerp(transform.position, _vec, smoothing * Time.deltaTime);
        }
        else
        {
            for (int i = 1; i < _tails.Count; i++)
            {
                _tails[i].transform.position = _positions[(_positions.Count - 1) - i * maxDistance / 3];
            }

            //transform.position = new Vector3(0, transform.position.y, transform.position.z + _pos.z * 3);
            Vector3 _vec = new Vector3(0, transform.position.y, transform.position.z + _pos.z*3);
            transform.position = Vector3.Lerp(transform.position, _vec, smoothing * Time.deltaTime);
        }

        _positions.Add(transform.position);
        //if(_positions.Count > 6301)
        //    _positions.RemoveAt(0);
        if (_positions.Count > (_tails.Count+1) * maxDistance)
     {
         for (int i = 0; i < _positions.Count - (_tails.Count + 1) * maxDistance; i++)
         {
             _positions.RemoveAt(0);
         }
     }
    }

    public Vector3 getPos()
    {
        return transform.position;
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private int onlycrystal;
    private void OnTriggerEnter(Collider other)
    {
        if (!Fever)
        {
            if (other.tag == "Food")
            {
                if (other.gameObject.GetComponent<MeshRenderer>().material.name ==
                    GetComponent<MeshRenderer>().material.name)
                {
                    Destroy(other.gameObject);
                    People++;
                    PeopleText.text = People.ToString();
                    onlycrystal = 0;
                    AddSnake();
                }
                else Restart();
            }

            if (other.tag == "Wall")
            {
                Destroy(other.gameObject);
                onlycrystal = 0;
                Restart();
            }

            if (other.tag == "Crystal")
            {
                Destroy(other.gameObject);
                Crystal++;
                onlycrystal++;
                CrystalText.text = Crystal.ToString();
                CheckFever();
            }
        }
        else
        {
            if(other.tag == "Player")
                return;
            if(other.tag == "donteat")
                return;
                
                
            Destroy(other.gameObject);
        }
    }

    private void CheckFever()
    {
        if (onlycrystal >= 3)
        {
            Fever = true;
            StartCoroutine(reloadFever());
        }
    }

    IEnumerator reloadFever()
    {
        yield return new WaitForSeconds(5f);
        Fever = false;
        Crystal = 0;
        onlycrystal = 0;
    }

    public void AddSnake()
    {
        if (People % 4 == 0)
        {
            GameObject fab = Instantiate(TailFab);
            fab.GetComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;
            _tails.Add(fab);
        }
    }
}


