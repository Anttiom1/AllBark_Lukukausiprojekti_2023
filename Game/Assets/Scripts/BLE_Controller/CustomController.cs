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
 * of this software and associated documentation files (the ìSoftwareÅE, to
 * deal in the Software without restriction, including without limitation the
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
 * sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED ìAS ISÅE WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
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
    private string z;
    private string posNeg;
    private int xQuart;
    private int zQuart;
    private string startSignal;
    private Player player;
    private int counter;
    private bool enter;

    /***************************************************************************
     * UNITY MESSAGES
     **************************************************************************/
    private void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("controller");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        player = FindAnyObjectByType<Player>();
        DontDestroyOnLoad(this);
        Debug.Log("customcontroller start");
        counter = 0;

    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        Debug.Log("counter" + counter);

        if (player == null)
        {
            Debug.Log("player is empty");
            player = FindAnyObjectByType<Player>();
        }
        byte[] data = bleConnection.Data;

        if (data != null)
            inputValue = BitConverter.ToInt32(data, 0);
        else
            return;

        string rawBinary = Convert.ToString(inputValue, 2);
        x = rawBinary.Substring(4, 8);
        z = rawBinary.Substring(rawBinary.Length - 8);
        posNeg = rawBinary.Substring(0, 3);
        startSignal = rawBinary.Substring(3, 1);
        xQuart = Convert.ToInt32(x, 2);
        zQuart = Convert.ToInt32(z, 2) * -1;

        if (posNeg == "110")
        {
            xQuart *= -1;
        }
        if(posNeg == "101")
        {
            zQuart *= -1;
        }
        if (posNeg == "111")
        {
            xQuart *= -1;
            zQuart *= -1;
        }
        if (startSignal == "1")
        {
            counter++;
               
        }
        if (counter >= 111)
        {
            counter = 0;
        }


        //Debug.Log("x: "+ xQuart);
        //Debug.Log("y: "+ yQuart);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
    }

    public int InputValue
    {
        get { return inputValue; }
    }

    public int XQuart
    {
        get { return xQuart; }
    }
    public int ZQuart
    {
        get { return zQuart; }
    }
    public string StartSignal
    {
        get { return startSignal;  }
    }
    public int Counter
    {
        get { return counter; }
    }

}
