using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static readonly UnityEvent OnCommentsDownloadStart = new UnityEvent();
    public static readonly UnityEvent OnCommentDownloadEnd = new UnityEvent();
    public static readonly UnityEvent OnStartReceivingData = new UnityEvent();
    public static readonly UnityEvent<DotInitialInfo> OnDotDataReceived = new UnityEvent<DotInitialInfo>();
    public static readonly UnityEvent OnStopReceivingData = new UnityEvent();
    public static readonly UnityEvent<Vector3[]> OnDotsBatchDataReceived = new UnityEvent<Vector3[]>();

    
}
