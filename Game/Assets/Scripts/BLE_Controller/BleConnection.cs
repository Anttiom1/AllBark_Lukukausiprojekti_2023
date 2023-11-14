/******************************************************************************
 * FILE     : BleConnection.cs
 * DESC     : Main purpose of this class is handle BLE connection between Unity
 *            and DLL
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
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Bluetooth Low Energy Connection Handler
/// </summary>
public class BleConnection : MonoBehaviour
{

    /***************************************************************************
     * ATTRIBUTES
     **************************************************************************/

    /// <summary>
    /// BLE Device Name, Must be same with ESP32 Name
    /// </summary>
    private string deviceName = "AllBark";

    /// <summary>
    /// Service UUID
    /// </summary>
    private string serviceUuid = "{4fafc201-1fb5-459e-8fcc-c5c9c331914b}";


    /// <summary>
    /// Characteristic UUIDS
    /// </summary>
    private string[] characteristicUuids = {
         "{beb5483e-36e1-4688-b7f5-ea07361b26a8}",
         "{617c753e-5199-11eb-ae93-0242ac130002}"
    };

    /// <summary>
    /// BLE dll
    /// </summary>
    private BLE ble;

    /// <summary>
    /// BLE Scan results
    /// </summary>
    BLE.BLEScan scan;

    /// <summary>
    /// Scan and connect Thread
    /// </summary>
    private Thread scanAndConnectThread;

    /// <summary>
    /// Read Thread
    /// </summary>
    private Thread readThread;

    /// <summary>
    /// Device ID
    /// </summary>
    private string deviceId = null;

    /// <summary>
    /// Connected flag (true if connected)
    /// </summary>
    private bool connected = false;

    /// <summary>
    /// Data Array
    /// </summary>
    private byte[] data;

    /// <summary>
    /// Get Data Array
    /// </summary>
    public byte[] Data
    {
        get
        {
            return this.data;
        }
    }

    /***************************************************************************
     * UNITY MESSAGES
     **************************************************************************/

    /// <summary>
    /// Start is called on the frame when a script is enabled just before any
    /// of the Update methods are called the first time.
    /// </summary>
    void Start()
    {
        ble = new BLE();
        readThread = new Thread(ReadBleData);
        StartScan();

    }


    /// <summary>
    /// Sent to all GameObjects before the application quits.
    /// </summary>
    private void OnApplicationQuit()
    {
        connected = false;
        scanAndConnectThread.Abort();
        readThread.Abort();
    }


    /***************************************************************************
     * METHODS
     **************************************************************************/

    /// <summary>
    /// Start Scan
    /// </summary>
    private void StartScan()
    {
        scanAndConnectThread = new Thread(ScanAndConnect);
        scanAndConnectThread.Start();

    }

    /// <summary>
    /// 
    /// </summary>
    private void ScanAndConnect()
    {
        // Start Scan
        scan = BLE.ScanDevices();

        scan.Found = (deviceId, deviceName) =>
        {
            Debug.Log("device found");
            if (this.deviceId == null && deviceName == this.deviceName)
            {
                this.deviceId = deviceId;
            }
        };

        // Wait and try to find correct device
        while (deviceId == null)
        {
            Thread.Sleep(500);
        }

        // Cancel Scan
        scan.Cancel();
        Debug.Log("cancel");

        if (this.deviceId != null)
        {
            try
            {
                // Connect to founded device
                ble.Connect(deviceId,
                serviceUuid,
                characteristicUuids);
                Debug.Log("connecterd");
            }
            catch (Exception e)
            {
            }
        }

        scanAndConnectThread = null;

        // Start to Reading Data
        readThread.Start();
        connected = true;
    }

    /// <summary>
    /// Read Data (notify must be received)
    /// </summary>
    private void ReadBleData()
    {
        while (connected)
        {
            data = BLE.ReadBytes();
        }
    }
}
