using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwinManager : MonoBehaviour
{
    public static SpwinManager instance;

    public int RoomsCount;
    private void Awake() {
        instance=this;
    }

}