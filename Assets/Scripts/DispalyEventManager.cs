using System;
using UnityEngine;

public sealed class DispalyEventManager : MonoBehaviour
{
    public Action On_R_clicked;
    public Action On_C_clicked;
    public Action On_ESC_clicked;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            On_R_clicked?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            On_C_clicked?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            On_ESC_clicked?.Invoke();
        }
    }
}
