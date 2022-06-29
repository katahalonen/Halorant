using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    // Gun properties
    public bool isAutomatic;
    public float timeBetweenShots = .1f, heatPerShot = 1f;
    public GameObject muzzleFlash;
}
