using MoonSharp.Interpreter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitUserData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UserData.RegisterType<Collider2D>();
        UserData.RegisterType<GameObject>();
        UserData.RegisterType<Enemy>();
    }
}
