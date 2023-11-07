using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    [SerializeField] Texture2D cursor;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursor, new Vector2(50,50), CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
