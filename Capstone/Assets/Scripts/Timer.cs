using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Timer
{
    private bool timerSet;
    private bool timerOver;
    private float count;

    public Timer (float _count)
    {
        this.count = _count;
        this.timerSet = false;
        this.timerOver = false;
    }

    public IEnumerator Start()
    {
        this.timerSet = true;
        this.timerOver = false;

        yield return new WaitForSeconds(this.count);

        this.timerOver = true;
    }

    public bool Set()
    {
        return this.timerSet;
    }

    public bool Over()
    {
        this.timerSet = false;
        return this.timerOver;
    }
}