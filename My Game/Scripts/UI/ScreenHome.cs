using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScreenHome : BaseScreen
{
    public override void Init()
    {
        base.Init();
    }

    public override void Show(object data)
    {
        base.Show(data);

        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayBGM(AUDIO.BGM_MENU);
        }
    }

    public override void Hide()
    {
        base.Hide();
    }

    //public void OnClickSettingButton()
    //{
    //    if (UIManager.HasInstance)
    //    {
    //        UIManager.Instance.ShowPopup<PopupSetting>();
    //    }
    //}

    //public void OnClickTutorialButton()
    //{
    //    if (UIManager.HasInstance)
    //    {
    //        UIManager.Instance.ShowPopup<PopupTutorial>();
    //    }
    //}

    public void OnClickStartButton()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.ShowNotify<NotifyLoadingGame>();
        }

        Hide();
    }

    public void OnClickExitButton()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
