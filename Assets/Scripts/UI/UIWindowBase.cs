using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowBase : MonoBehaviour
{
    public event Action<UIWindowBase> OnClose;
    public void Close()
    {
        OnClose?.Invoke(this);
        Destroy(gameObject);
    }
}
