using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code Done By: Celest Goh Zi Xuan
// ================================
// This script is for managing and controling coroutines
// Able to start, stop, and check the running status of a coroutine

public class CoroutineWrapper
{
    private MonoBehaviour owner;
    private IEnumerator coroutine;
    public bool IsRunning { get; private set; }

    public CoroutineWrapper(MonoBehaviour owner, IEnumerator coroutine)
    {
        this.owner = owner;
        this.coroutine = coroutine;
    }

    public void Start()
    {
        IsRunning = true;
        owner.StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        yield return owner.StartCoroutine(coroutine);
        IsRunning = false;
    }
}
