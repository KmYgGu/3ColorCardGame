using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]// 이 클래스를 직렬화로 만들어 익스펙터에서 수정이 가능
public class colorCardData_Entity
{
    public int no;
    public string type;
    public string name;
    public string explanation;
    public string cardicon;
}

[System.Serializable]
public class eventCardData_Entity
{
    public int no;
    public string type;
    public string name;
    public string explanation;
    public string cardicon;
}
