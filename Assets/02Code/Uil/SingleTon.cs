using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MonoBehaviour를 상속받는 T 타입일때만 작동
public class SingleTon<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Inst { get; private set; }//내부적으로는 Set만 가능, 외부적으로는 get만 가능

    protected virtual void Awake()
    {
        if(Inst == null)
        {
            Inst = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DoAwake();
    }

    protected virtual void DoAwake()// 파생 클래스에서 초기화가 필요하면 여기에서 사용
    {

    }
}

public class SingletonDestory<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Inst { get; private set; }//내부적으로는 Set만 가능, 외부적으로는 get만 가능

    protected virtual void Awake()
    {
        if (Inst == null)
        {
            Inst = this as T;
            
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DoAwake();
    }

    protected virtual void DoAwake()// 파생 클래스에서 초기화가 필요하면 여기에서 사용
    {

    }
}
