using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemTask : TaskBase
{
    // Start is called before the first frame update
    public System.Type _requiredItemType;
    public GetItemTask(System.Type type)
    {
        _taskDescription = "Get Item";
        _remaining = 1;
        _requiredItemType = type;
        QuestActions.AddIntentoryItem+=CheckItem;

    }
    void CheckItem(IItem item){
        if(_requiredItemType == item.GetType()){
            DecrementRemaining();
        }
    }

}

