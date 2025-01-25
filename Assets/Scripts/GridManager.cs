using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class GridManager : MonoBehaviour
    {
    public static float highest = 0;

    public static void HighestPointUpdate(Transform block)
    {
        foreach (Transform child in block)
        {
            if (child.position.y > highest)
            {
                highest = child.position.y;
            }
        }
    }
}

