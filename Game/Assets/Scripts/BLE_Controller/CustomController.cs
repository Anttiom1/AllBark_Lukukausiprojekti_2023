/******************************************************************************
 * FILE     : CustomController.cs
 * DESC     : This Class Replace Unity Input/Controller class
 * AUTHOR   : Toni Westerlund
 * VERSION  : 0.1 First Realease
 * LISENCE  : MIT
 * 
 * 
 * Copyright 2023 Toni Westerlund
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the “Software”), to
 * deal in the Software without restriction, including without limitation the
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
 * sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 * ****************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Custom Controller
/// </summary>
public class CustomController : MonoBehaviour
{

    /***************************************************************************
     * ATTRIBUTES
     **************************************************************************/
    /// <summary>
    /// RAW DATA
    /// </summary>
    [SerializeField] private BleConnection bleConnection;

    /// <summary>
    /// input Value
    /// </summary>
    static int inputValue;
    private string x;
    private string y;
    private string posNeg;
    private int xQuart;
    private int yQuart;
    private string startSignal;
    private Player player;

    /***************************************************************************
     * UNITY MESSAGES
     **************************************************************************/
    private void Start()
    {
        player = FindAnyObjectByType<Player>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        byte[] data = bleConnection.Data;

        if (data != null)
            inputValue = BitConverter.ToInt32(data, 0);
        else
            return;
        string rawBinary = Convert.ToString(inputValue, 2);
        x = rawBinary.Substring(4, 8);
        y = rawBinary.Substring(rawBinary.Length - 8);
        posNeg = rawBinary.Substring(0, 3);
        startSignal = rawBinary.Substring(3, 1);
        xQuart = Convert.ToInt32(x, 2);
        yQuart = Convert.ToInt32(y, 2);
        
        if(posNeg == "110")
        {
            xQuart *= -1;
        }
        if(posNeg == "101")
        {
            yQuart *= -1;
        }
        if (posNeg == "111")
        {
            xQuart *= -1;
            yQuart *= -1;
        }
        if (startSignal == "1")
        {
            player.EngineOn = true;
        }
        Debug.Log("x: "+ xQuart);
        Debug.Log("y: "+ yQuart);
    }

    public int InputValue
    {
        get { return inputValue; }
    }

    public int XQuart
    {
        get { return xQuart; }
    }
    public int YQuart
    {
        get { return yQuart; }
    }
}
