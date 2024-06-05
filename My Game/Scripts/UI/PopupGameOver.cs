using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupGameOver: BasePopup
{
    public override void Init()
    {
        base.Init();
    }

    public override void Show(object data)
    {
        base.Show(data);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public override void Hide()
    {
        base.Hide();
    }

    public void OnClickRestartButton()
    {
        Hide();
        GameManager.Instance.NewGame();
        Time.timeScale = 1f;
    }

    public void OnClickMenuButton()
    {
        Hide();
        GameManager.Instance.RestartGame();
        Time.timeScale = 1f;
    }
}
