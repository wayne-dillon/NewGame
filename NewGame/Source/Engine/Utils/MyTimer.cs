using System;
using System.Xml.Linq;

public class MyTimer
{
    public bool goodToGo;
    protected int mSec;
    protected TimeSpan timer = new();

    public MyTimer(int m) { 
        goodToGo = false;
        mSec = m;
    }

    public MyTimer(int m, bool STARTLOADED)
    {
        goodToGo = STARTLOADED;
        mSec = m;
    }

    public int MSec
    {
        get { return mSec; }
        set { mSec = value; }
    }

    public int Timer
    {
        get { return (int)timer.TotalMilliseconds; }
    }

    public int RemainingTime
    {
        get { return mSec - Timer; }
    }

    public void UpdateTimer()
    {
        timer += Globals.gameTime.ElapsedGameTime;
    }

    public void UpdateTimer(float SPEED)
    {
        timer += TimeSpan.FromTicks((long)(Globals.gameTime.ElapsedGameTime.Ticks * SPEED));
    }

    public virtual void AddToTimer(int MSEC)
    {
        timer += TimeSpan.FromMilliseconds(MSEC);
    }

    public bool Test() => timer.TotalMilliseconds >= mSec || goodToGo;

    public void Reset()
    {
        timer = timer.Subtract(new TimeSpan(0, 0, mSec/60000, mSec/1000, mSec%1000));
        if (timer.TotalMilliseconds < 0)
        {
            timer = TimeSpan.Zero;
        }
        goodToGo = false;
    }

    public void Reset(int NEWTIMER)
    {
        timer = TimeSpan.Zero;
        MSec = NEWTIMER;
        goodToGo = false;
    }

    public void ResetToZero()
    {
        timer = TimeSpan.Zero;
        goodToGo = false;
    }

    public virtual XElement ReturnXML() => new("Timer", 
                            new XElement("mSec", mSec),
                            new XElement("timer", timer));

    public void SetTimer(TimeSpan TIME)
    {
        timer = TIME;
    }

    public virtual void SetTimer(int MSEC)
    {
        timer = TimeSpan.FromMilliseconds(MSEC);
    }
}
