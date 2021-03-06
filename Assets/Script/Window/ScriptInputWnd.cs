﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScriptInputWnd:Window<ScriptInputWnd>
{
    InputField scriptInput;

    Text result;
    protected override bool OnOpen()
    {
        scriptInput = Control("InputField").GetComponent<InputField>();
        Control("Close").GetComponent<Button>().onClick.AddListener(() => { Close(); });
        Control("DoScript").GetComponent<Button>().onClick.AddListener(() => {
            try
            {
                if (scriptInput.text == "666")
                {
                    GameObject obj = GameObject.Instantiate(Resources.Load("Bullet")) as GameObject;
                    result.text = "成功召唤滑稽王";
                }
                ScriptMng.ins.CallString(scriptInput.text);
                result.text = "脚本成功执行";
            }
            catch (Exception exp)
            {
                result.text = "执行出错:" + exp.Message + "-" + exp.StackTrace;
            }
        });
        result = Control("Result").GetComponent<Text>();
        return true;
    }
}
