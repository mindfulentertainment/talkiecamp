using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class CheckQuest : MonoBehaviour
{

    void Start()
    {
        StateManager.Instance.OnResourcesLoad.AddListener(Subscribe);

    }

    private void Subscribe(Resource resource, Buildings buildings)
    {
        bool hasTent=false;
        foreach (var item in buildings.buildings)
        {
            if (item.buildingName == "Tent")
            {
                hasTent = true;

            }
        }
        if (!hasTent)
        {
            DataManager.instance.OnNewBuilding += Check;

        }
    }

    private void OnDisable()
    {
        DataManager.instance.OnNewBuilding -= Check;
        StateManager.Instance.OnResourcesLoad.RemoveListener(Subscribe);

    }


    void Check(Resource resource, Buildings buildings)
    {
        foreach (var item in buildings.buildings)
        {
            if (item.buildingName == "Tent")
            {
                Notification.instance.ShowMessage("Ya tenemos donde dormir!", 4);
                DataManager.instance.OnNewBuilding -= Check;

            }
        }
    }

}
