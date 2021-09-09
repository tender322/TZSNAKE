using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
    public void move(Vector3 _pos,float _mousepos);
    public Vector3 getPos();

    public void AddSnake();
}
