using UnityEngine;

public class CircularBuffer <T>
{ 
    public T[] dataArray;
    int bufferCurrentPosition = -1;
    int bufferCapacity;
    float howManyRecordsPerSecond;

    int bufferBeforePosition = -1;

    /// <summary>
    /// Use circular buffer structure for time rewinding
    /// </summary>
    public CircularBuffer()
    {
        try
        {
            howManyRecordsPerSecond = Time.timeScale / Time.fixedDeltaTime;
            bufferCapacity = (int)(RewindManager.howManySecondsToTrack *howManyRecordsPerSecond);
            dataArray = new T[bufferCapacity];
            RewindManager.RestoreBuffers += OnBuffersRestore;
            bufferBeforePosition = bufferCapacity;
        }
        catch
        {
            Debug.LogError("Circular buffer cannot use field initialization (Time.fixedDeltaTime is unknown yet). Initialize Circular buffer in Start() method!");
        }        
    }
    
    public CircularBuffer(InstancedRewindManager manager)
    {
        try
        {
            howManyRecordsPerSecond = Time.timeScale / Time.fixedDeltaTime;
            bufferCapacity = (int)(manager.howManySecondsToTrack *howManyRecordsPerSecond);
            dataArray = new T[bufferCapacity];
            manager.RestoreBuffers += OnBuffersRestore;
        }
        catch
        {
            Debug.LogError("Circular buffer cannot use field initialization (Time.fixedDeltaTime is unknown yet). Initialize Circular buffer in Start() method!");
        }        
    }

    ~CircularBuffer()
    {
        RewindManager.RestoreBuffers -= OnBuffersRestore;
    }
    
    /// <summary>
    /// Write value to the last position of the buffer
    /// </summary>
    /// <param name="val"></param>
    public void WriteLastValue(T val)
    {
        bufferCurrentPosition++;
        if (bufferCurrentPosition >= bufferCapacity)
        {
            bufferCurrentPosition = 0;
            dataArray[bufferCurrentPosition] = val;
        }
        else
        {
            dataArray[bufferCurrentPosition] = val;
        }
    }

    public void WriteValueBefore(T val)
    {
        bufferBeforePosition--;
        if (bufferBeforePosition < 0)
        {
            bufferBeforePosition = bufferCapacity - 1;
            dataArray[bufferBeforePosition] = val;
        }
        else
        {
            dataArray[bufferBeforePosition] = val;
        }
    }
    /// <summary>
    /// Read last value that was written to buffer
    /// </summary>
    /// <returns></returns>
    public T ReadLastValue()
    {
        return dataArray[bufferCurrentPosition];
    }
    /// <summary>
    /// Read specified value from circular buffer
    /// </summary>
    /// <param name="seconds">Variable defining how many seconds into the past should be read (eg. seconds=5 then function will return the values that tracked object had exactly 5 seconds ago)</param>
    /// <returns></returns>
    public T ReadFromBuffer(float seconds)
    {
        int howManyBeforeLast = (int)(howManyRecordsPerSecond * seconds);

        if((bufferCurrentPosition-howManyBeforeLast) <0)
        {
            int showingIndex = bufferCapacity - (howManyBeforeLast - bufferCurrentPosition);
            return dataArray[showingIndex];
        }
        else
        {
            return dataArray[bufferCurrentPosition - howManyBeforeLast];
        }
    }
    private void MoveLastBufferPosition(float seconds)
    {
        int howManyBeforeLast=(int)(howManyRecordsPerSecond*seconds);

        if ((bufferCurrentPosition - howManyBeforeLast) < 0)
        {
            bufferCurrentPosition = bufferCapacity - (howManyBeforeLast - bufferCurrentPosition);
        }
        else
        {
            bufferCurrentPosition -= howManyBeforeLast;
        }     
    }
    private void OnBuffersRestore(float seconds)
    {
        MoveLastBufferPosition(seconds);
    }
    
    

}
