using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;
// se film omkring singleton
    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject Player;
}
