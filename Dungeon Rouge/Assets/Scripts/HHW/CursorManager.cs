using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorIcon;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorIcon, Vector2.zero, CursorMode.Auto); // Cursor.SetCoursor 메서드를 통해 변경된 커서는 게임 전역에서 유지가 됨 -> 한 씬에서 커서를 설정하면 다른 씬으로 넘어가도 그 설정이 유지됨
    }
}
