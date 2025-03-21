using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MonoBehaviour�� ��ӹ޴� T Ÿ���϶��� �۵�
public class SingleTon<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Inst { get; private set; }//���������δ� Set�� ����, �ܺ������δ� get�� ����

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

    protected virtual void DoAwake()// �Ļ� Ŭ�������� �ʱ�ȭ�� �ʿ��ϸ� ���⿡�� ���
    {

    }
}

public class SingletonDestory<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Inst { get; private set; }//���������δ� Set�� ����, �ܺ������δ� get�� ����

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

    protected virtual void DoAwake()// �Ļ� Ŭ�������� �ʱ�ȭ�� �ʿ��ϸ� ���⿡�� ���
    {

    }
}
