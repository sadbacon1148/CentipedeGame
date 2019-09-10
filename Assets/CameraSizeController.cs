using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeController : MonoBehaviour
{
    private Camera thisCamera;
    private Grid grid;
    // Start is called before the first frame update
    void Start()
    {
        thisCamera = GetComponent<Camera>();
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        if (grid != null)
        {
            thisCamera.orthographicSize = grid.gridSize.y - 10f;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
