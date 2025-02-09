using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class количество : MonoBehaviour
{
    public int maxBulletCount = 3; // Максимальное количество пуль
    private int bulletCount;
    // Start is called before the first frame update
    void Start()
    {
        bulletCount = maxBulletCount; // Устанавливаем максимальное количество пуль
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
