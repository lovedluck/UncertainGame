using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.UI;
using UnityEngine.UI;

public class LoginUI : UIBase
{
    public InputField userNameInput;
    public InputField passWord;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OpenUI()
    {
        base.OpenUI();  
    
    }

    public override void CloseUI()
    {
        base.CloseUI();
    }

    public override void Preopen()
    {
        base.Preopen();
    }

    public override void HideUI()
    {
        base.HideUI();
    }
}


