using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActiveOnDestroy : MonoBehaviour
{
    [System.Serializable]
    public struct Data
    {
        public bool done;
        public GameObject[] destroyed;
        public GameObject[] toActivate;
    }
    public Data[] datas;

    void FixedUpdate()
    {
        for (int i = 0; i < datas.Length; ++i)
        {
            if (datas[i].done)
                continue;

            bool check = true;
            ref var destroyed = ref datas[i].destroyed;
            for (int j = 0; j < destroyed.Length; ++j)
            {
                if (destroyed[j] != null)
                    check = false;
            }
            if (check)
                for (int j = 0; j < datas[i].toActivate.Length; ++j)
                    datas[i].toActivate[j].SetActive(true);
        }
    }
}
