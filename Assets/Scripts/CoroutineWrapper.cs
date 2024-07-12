using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineWrapper
{
    public bool IsRunning { get; private set; }
    public bool IsCompleted { get; private set; }

    private IEnumerator coroutine;
    private MonoBehaviour owner;

    public CoroutineWrapper(MonoBehaviour owner, IEnumerator coroutine)
    {
        this.owner = owner;
        this.coroutine = CoroutineRunner(coroutine);
    }

    public void Start()
    {
        owner.StartCoroutine(coroutine);
    }

    public void Stop()
    {
        owner.StopCoroutine(coroutine);
        IsRunning = false;
    }

    private IEnumerator CoroutineRunner(IEnumerator coroutine)
    {
        IsRunning = true;
        IsCompleted = false;

        yield return coroutine;

        IsRunning = false;
        IsCompleted = true;
    }
}