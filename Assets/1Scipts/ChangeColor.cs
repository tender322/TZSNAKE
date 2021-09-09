using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeColor : MonoBehaviour
{
  public Material ChangeMat;

  private void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player" && gameObject.name == "finish")
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    if (other.tag == "Player")
      other.GetComponent<MeshRenderer>().material = ChangeMat;
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.tag == "Player")
      other.GetComponent<MeshRenderer>().material = ChangeMat;
  }
}
