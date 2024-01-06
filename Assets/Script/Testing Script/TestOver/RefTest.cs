using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Person
{
    public int age;
    public int[] sex;
    public string name;
}

public class RefTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Person jay = new Person();
        jay.age = 20;
        jay.sex = new int[1];
        jay.sex[0] = 0;
        jay.name = "j";

        Person sara = new Person();
        sara.age = jay.age;
        sara.sex = new int[1];
        sara.sex[0] = 0;
        sara.name = jay.name;

        Debug.Log(sara.age);
        Debug.Log(sara.sex[0]);
        Debug.Log(sara.name);

        jay.age = 21;
        sara.sex[0] = 1;
        jay.name = "change";

        Debug.Log(sara.age);
        Debug.Log(sara.sex[0]);
        Debug.Log(sara.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
