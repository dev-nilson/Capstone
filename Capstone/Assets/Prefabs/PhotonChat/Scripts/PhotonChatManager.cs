using ExitGames.Client.Photon;
using Photon.Chat; //needed to add photon.chat functionality
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonChatManager : MonoBehaviour, IChatClientListener //IChatClientListener needs special callbacks in order to work
{
    #region Setup

    [SerializeField] GameObject joinChatButton;
    ChatClient chatClient;
    bool isConnected;
    [SerializeField] string username = PhotonNetwork.NickName;
    #endregion Setup

    #region General

    [SerializeField]
    GameObject chatPanel;

    string privateReceiver = "";
    string currentChat;

    [SerializeField] 
    InputField chatField;

    [SerializeField] 
    Text chatDisplay;

    [SerializeField]
    GameObject chatRoom;

    AuthenticationValues authValues = new AuthenticationValues();

    void Awake()
    {
        authValues.UserId = PhotonNetwork.NickName;
        Debug.Log(PhotonNetwork.NickName + "is ready to chat!");
        isConnected = true;
        chatClient = new ChatClient(this);
        //chatClient.ChatRegion = "US";
        //this line below has its on callback function connected to it
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, authValues);
        chatRoom.SetActive(true);
        Debug.Log("Chat should be showing now!");
        Debug.Log("Connenting chat clients");
    }

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (isConnected) //if the chat client is still active...
        {
            chatClient.Service(); //maintains connection to photon chat server
        }

        //When player is typing, it will call both of these funtions if they press enter/return
        //which will publish whatever we have currently typed in our input field
        if (chatField.text != "" && Input.GetKey(KeyCode.Return))
        {
            SubmitPublicChatOnClick();
            SubmitPrivateChatOnClick();
        }
    }

    #endregion General

    #region PublicChat
    /*
     * We check to see if privatReciever is empty string
     * PublishMessage, takes your region, and passes your currentChat to that reigon
     * The chat field and currentChat are the made empty strings
     * Is paired to send button
     */
    public void SubmitPublicChatOnClick()
    {
        if (privateReceiver == "")
        {
            chatClient.PublishMessage("RegionChannel", currentChat);
            chatField.text = "";
            currentChat = "";
        }
    }

    /*
     * Whatever you type into input field is saved into the currentchat string, happens dynamically
     */
    public void TypeChatOnValueChange(string valueIn)
    {
        currentChat = valueIn;
    }

    #endregion PublicChat

    #region PrivateChat

    /*
     * responsible for saving the name of the person who you want to specifically send a mesage to
     */
    public void ReceiverOnValueChange(string valueIn)
    {
        privateReceiver = valueIn;
    }

    /*
     * Calls a function that sends your privateReciever(the person you want to send to
     * the current chat you have stored
     */
    public void SubmitPrivateChatOnClick()
    {
        if (privateReceiver != "")
        {
            chatClient.SendPrivateMessage(privateReceiver, currentChat);
            chatField.text = "";
            currentChat = "";
        }
    }

    #endregion PrivateChat

    #region Callbacks

    public void DebugReturn(DebugLevel level, string message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnChatStateChange(ChatState state)
    {
        if(state == ChatState.Uninitialized)
        {
            isConnected = false;
            //joinChatButton.SetActive(true);
            chatPanel.SetActive(false);
        }

        //throw new System.NotImplementedException();
        //Debug.Log("Connected");
        isConnected = true;
        //joinChatButton.SetActive(false);
    }

    /*
     * When we make a successful connect, we disable the join chat button 
     * We then subscribe to a specific chat room, where our convo will happen
     */
    public void OnConnected()
    {
        Debug.Log("Connected");
        chatRoom.SetActive(true);
        //the photon chat plugin can only be conncetd to one region at a time
        chatClient.Subscribe(new string[] { "RegionChannel" });
    }

    public void OnDisconnected()
    {
        isConnected = false;
        //joinChatButton.SetActive(true);
        chatPanel.SetActive(false);
    }

    /*
     * Will be executed any time a public chat message is published to a 
     * channel you are currently subscribed to
     */
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        string msgs = "";
        for (int i = 0; i < senders.Length; i++)
        {
            //format for how our messages will appear in textbox
            msgs = string.Format("{0}: {1}", senders[i], messages[i]);

            chatDisplay.text += "\n" + msgs;

            Debug.Log(msgs);
        }

    }

    /*
    * Will be executed any time a private chat message is published
    */
    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        string msgs = "";

        msgs = string.Format("(Private) {0}: {1}", sender, message);

        chatDisplay.text += "\n " + msgs;

        Debug.Log(msgs);
        
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    //enables the chatpanel (or wherever your chat is) to be active
    //could display a bot message to chat to chat room
    //could even update as game goes
    public void OnSubscribed(string[] channels, bool[] results)
    {
        chatPanel.SetActive(true);
    }

    public void OnUnsubscribed(string[] channels)
    {
       // throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
      //  throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
       // throw new System.NotImplementedException();
    }

    #endregion Callbacks
}
