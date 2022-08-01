using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 宏指令  在指令中存放了多个类型，相当于将c层的集合归到自己的身上
/// </summary>
public class MacroCommand : Notifier, ICommand, INotifier
{
    private IList<Type> Commands = new List<Type>();

    public MacroCommand()
    {
        this.InitializeMacroCommand();
    }

    protected void AddSubCommand(Type commandType)
    {
        this.Commands.Add(commandType);
    }


    protected virtual void InitializeMacroCommand()
    {
    }
    public void Execute(INotification notification)
    {
        while (this.Commands.Count > 0)
        {
            Type type = this.Commands[0];
            object obj2 = Activator.CreateInstance(type);
            if (obj2 is ICommand)
            {
                ((ICommand)obj2).Execute(notification);
            }
            this.Commands.RemoveAt(0);
        }
    }
}
