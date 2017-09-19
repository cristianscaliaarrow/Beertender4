using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneSignalManager : MonoBehaviour {

    public ReadMessages readMessage;

    void Start()
    {
        OneSignal.StartInit("840b0c45-e9cc-47d7-8b40-94f703931e50").HandleNotificationOpened(HandleNotificationOpened).HandleNotificationReceived(HandleNotificationReceived).EndInit();
    }
   
    private void HandleNotificationOpened(OSNotificationOpenedResult result)
    {
        readMessage.LoadMessagesFromFile();
        Message m = new Message();
        m.title = result.notification.payload.title;
        m.message = result.notification.payload.body;
        readMessage.pendingMessages.Add(m);
        readMessage.UpdatePopUpPendingMessages();
        readMessage.SaveAllMessage();
    }

    private void HandleNotificationReceived(OSNotification notification)
    {
        OSNotificationPayload payload = notification.payload;
        string message = payload.body;
    }

}
