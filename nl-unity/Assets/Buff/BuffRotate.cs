﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffRotate : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(15,30,45) * Time.deltaTime);
    }
}