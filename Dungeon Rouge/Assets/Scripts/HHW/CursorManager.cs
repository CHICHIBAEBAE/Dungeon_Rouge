using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorIcon;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorIcon, Vector2.zero, CursorMode.Auto); // Cursor.SetCoursor �޼��带 ���� ����� Ŀ���� ���� �������� ������ �� -> �� ������ Ŀ���� �����ϸ� �ٸ� ������ �Ѿ�� �� ������ ������
    }
}
