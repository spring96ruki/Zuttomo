using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

public class Chat : MonoBehaviour
{
    public int playerId = 0;
    public int PlayersNum = 4;
    public GameObject[] Players;
    public GameObject player0;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;

    private TransportTCP	m_transport;
	
	
	public ChatState		m_state = ChatState.HOST_TYPE_SELECT;
	
	private	string 			m_hostAddress = "172.19.74.105";
	
	private const int 		m_port = 50765;

	
	private string			m_sendComment = "";
	private string			m_prevComment = "";

	private string			m_chatMessage = "";

	private List<string>[]	m_message;

	private	bool			m_isServer = false;

	public Texture 			texture_title = null;
	public Texture 			texture_bg = null;

	// バルーン表示用テクスチャ.
	public Texture texture_main    = null;
	public Texture texture_belo    = null;
	public Texture texture_kado_lu = null;
	public Texture texture_kado_ru = null;
	public Texture texture_kado_ld = null;
	public Texture texture_kado_rd = null;
	public Texture texture_tofu    = null;
	public Texture texture_daizu   = null;

	private static float	KADO_SIZE = 16.0f;
	private static float	FONT_SIZE   = 13.0f;
	private static float	FONG_HEIGHT = 18.0f;
	private static int		MESSAGE_LINE = 18;
	private static int		CHAT_MEMBER_NUM = 2;

	public enum ChatState {
		HOST_TYPE_SELECT = 0,	// ルーム選択.
		CHATTING,				// チャット中.
		LEAVE,					// 退出.
		ERROR,					// エラー.
	};
	
	
	
	// Use this for initialization
	void Start()
	{
        Players = new GameObject[PlayersNum];
        try
        {
            Players[0] = player0;
            Players[1] = player1;
            Players[2] = player2;
            Players[3] = player3;
        }
        catch (Exception e1)
        {
            Debug.Log("error:" + e1.ToString());
        }

        IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
		System.Net.IPAddress hostAddress = hostEntry.AddressList[0];
		Debug.Log(hostEntry.HostName);
		//m_hostAddress = hostAddress.ToString ();

		GameObject go = new GameObject("Network");
		m_transport = go.AddComponent<TransportTCP>();

		m_transport.RegisterEventHandler(OnEventHandling);

		m_message = new List<string>[CHAT_MEMBER_NUM];
		for (int i = 0; i < CHAT_MEMBER_NUM; ++i) {
			m_message[i] = new List<string>();
		}
	}
	
	// Update is called once per frame
	void Update()
	{
        bool isSent = false;
        Vector3 nowPosition = Players[playerId].transform.position;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Players[playerId].transform.Rotate(new Vector3(0, 5, 0));
            isSent = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Players[playerId].transform.Rotate(new Vector3(0, -5,0));
            isSent = true;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Players[playerId].transform.position = nowPosition + new Vector3(0,0, 0.1f);
            isSent = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Players[playerId].transform.position = nowPosition + new Vector3(0, 0, -0.1f);
            isSent = true;
        }
        if (isSent)
        {
            string message = playerId + "," +
                Players[playerId].transform.position.x + "," +
                Players[playerId].transform.position.y + "," +
                Players[playerId].transform.position.z + "," +
                Players[playerId].transform.eulerAngles.x + "," +
                Players[playerId].transform.eulerAngles.y + "," +
                Players[playerId].transform.eulerAngles.z;
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);
            m_transport.Send(buffer, buffer.Length);
        }

        switch (m_state) {
		case ChatState.HOST_TYPE_SELECT:
			for (int i = 0; i < CHAT_MEMBER_NUM; ++i) {
				m_message[i].Clear();
			}
			break;

		case ChatState.CHATTING:
			UpdateChatting();
			break;
			
		case ChatState.LEAVE:
			UpdateLeave();
			break;
		}
	}
	
	void UpdateChatting()
	{
		byte[] buffer = new byte[1400];

		int recvSize = m_transport.Receive(ref buffer, buffer.Length);
		if (recvSize > 0) {
			string message = System.Text.Encoding.UTF8.GetString(buffer);
			Debug.Log("Recv data:" + message );
			m_chatMessage += message + "   ";// + "\n";

			int id = (m_isServer == true)? 1 : 0;
			AddMessage(ref m_message[id], message);

            var playerData = message.Split(',');
            if (playerData.Length == 7)
            {
                int i = int.Parse(playerData[0]);
                float x = float.Parse(playerData[1]);
                float y = float.Parse(playerData[2]);
                float z = float.Parse(playerData[3]);
                float rx = float.Parse(playerData[4]);
                float ry = float.Parse(playerData[5]);
                float rz = float.Parse(playerData[6]);
                try
                {
                    Debug.Log("transform:"+i+"," + rx + "," + ry + "," + rz);
                    Players[i].transform.position = new Vector3(x, y, z);
                    Players[i].transform.rotation = Quaternion.Euler(rx, ry, rz);
                }
                catch (Exception e2)
                {
                    Debug.Log("error:"+e2.ToString());
                }
            }
        }	
	}
	
	void UpdateLeave()
	{
		if (m_isServer == true) {
			m_transport.StopServer();
		}
		else {
			m_transport.Disconnect();
		}

		// メッセージの削除.
		for (int i = 0; i < 2; ++i) {
			m_message[i].Clear();
		}
		
		m_state = ChatState.HOST_TYPE_SELECT;
	}
	
	void OnGUI()
	{
		switch (m_state) {
		case ChatState.HOST_TYPE_SELECT:
			//GUI.DrawTexture(new Rect(0, 0, 800, 600), this.texture_title);
			SelectHostTypeGUI();
			break;
			
		case ChatState.CHATTING:
			//GUI.DrawTexture(new Rect(0, 0, 800, 600), this.texture_bg);
			ChattingGUI();
			break;

		case ChatState.ERROR:
			//GUI.DrawTexture(new Rect(0, 0, 800, 600), this.texture_title);
			ErrorGUI();
			break;
		}
	}
	
	
	void SelectHostTypeGUI()
	{
        float sx = 800.0f;
        float sy = 600.0f;
        float px = sx * 0.5f - 100.0f;
        float py = sy * 0.75f;

        if (GUI.Button(new Rect(px, py, 200, 30), "チャットルームの作成")) {

			m_transport.StartServer(m_port, 1);

			m_state = ChatState.CHATTING;
			m_isServer = true;
		}


        Rect labelRect = new Rect(px, py + 80, 200, 30);
		GUIStyle style = new GUIStyle();
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = Color.white;
		GUI.Label(labelRect, "あいてのIPあどれす", style);
		labelRect.y -= 2;
		style.fontStyle = FontStyle.Normal;
		style.normal.textColor = Color.black;
		GUI.Label(labelRect, "あいてのIPアドレス", style);

		Rect textRect = new Rect(px, py + 100, 200, 30);
        m_hostAddress = GUI.TextField(textRect, m_hostAddress);


        if (GUI.Button(new Rect(px, py + 40, 200, 30), "チャットルームへの参加")) {
			bool ret = m_transport.Connect(m_hostAddress, m_port);
			if (ret) {
				m_state = ChatState.CHATTING;
			}
			else {
				m_state = ChatState.ERROR;
			}
		}
	}
	
	void ChattingGUI()
	{
		Rect commentRect = new Rect(220, 450, 300, 30);
		m_sendComment = GUI.TextField(commentRect, m_sendComment, 15);

		bool isSent = GUI.Button(new Rect (530, 450, 100, 30), "しゃべる");
		if (Event.current.isKey && 
		    Event.current.keyCode == KeyCode.Return) { 
			if (m_sendComment == m_prevComment) {
				isSent = true;
				m_prevComment = "";
			}
			else {
				m_prevComment = m_sendComment;
			}
		}
			
		if (isSent == true) {
			string message = "[" + DateTime.Now.ToString("HH:mm:ss") + "] " + m_sendComment;
			byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);		
			m_transport.Send(buffer, buffer.Length);
			AddMessage(ref m_message[(m_isServer == true)? 0 : 1], message);
			m_sendComment = "";
		}
		
		if (GUI.Button (new Rect (700, 560, 80, 30), "退出")) {
			m_state = ChatState.LEAVE;
		}	

		// とうふやさん(サーバ側)のメッセージ表示.
		if (m_transport.IsServer() ||
			m_transport.IsServer() == false && m_transport.IsConnected()) {
			DispBalloon(ref m_message[0], new Vector2(200.0f, 200.0f), new Vector2(340.0f, 360.0f), Color.cyan, true);
			GUI.DrawTexture(new Rect(50.0f, 370.0f, 145.0f, 200.0f), this.texture_tofu);
		}

		if (m_transport.IsServer() == false ||
			m_transport.IsServer() && m_transport.IsConnected()) {
			// だいずやさんの(クライアント側)のメッセージ表示. 
			DispBalloon(ref m_message[1], new Vector2(600.0f, 200.0f), new Vector2(340.0f, 360.0f), Color.green, false);
			GUI.DrawTexture(new Rect(600.0f, 370.0f, 145.0f, 200.0f), this.texture_daizu);
		}
	}

	void ErrorGUI()
	{
		float sx = 800.0f;
		float sy = 600.0f;
		float px = sx * 0.5f - 150.0f;
		float py = sy * 0.5f;
		
		if (GUI.Button(new Rect(px, py, 300, 80), "接続できませんでした.\n\nぼたんをおしてね")) {
			m_state = ChatState.HOST_TYPE_SELECT;
		}	
	}

	void AddMessage(ref List<string> messages, string str)
	{
		while (messages.Count >= MESSAGE_LINE) {
			messages.RemoveAt(0);
		}

		messages.Add(str);
	}

	void DispBalloon(ref List<string> messages, Vector2 position, Vector2 size, Color color, bool left) 
	{
		// 吹き出し枠を描画します.
		DrawBaloonFrame(position, size, color, left);

		// チャットの文章を表示します.	
		foreach (string s in messages) {
			DrawText(s, position, size);
			position.y += FONG_HEIGHT;
		}
	}

	void DrawBaloonFrame(Vector2 position, Vector2 size, Color color, bool left) 
	{
		GUI.color = color;
		
		float		kado_size = KADO_SIZE;
		
		Vector2		p, s;
		
		s.x = size.x - kado_size*2.0f;
		s.y = size.y;
		
		// 真ん中.
		p = position - s/2.0f;
		GUI.DrawTexture(new Rect(p.x, p.y, s.x, s.y), this.texture_main);
		
		// 左.
		p.x = position.x - s.x/2.0f - kado_size;
		p.y = position.y - s.y/2.0f + kado_size;
		GUI.DrawTexture(new Rect(p.x, p.y, kado_size, size.y - kado_size*2.0f), this.texture_main);
		
		// 右.
		p.x = position.x + s.x/2.0f;
		p.y = position.y - s.y/2.0f + kado_size;
		GUI.DrawTexture(new Rect(p.x, p.y, kado_size, size.y - kado_size*2.0f), this.texture_main);
		
		// 左上.
		p.x = position.x - s.x/2.0f - kado_size;
		p.y = position.y - s.y/2.0f;
		GUI.DrawTexture(new Rect(p.x, p.y, kado_size, kado_size), this.texture_kado_lu);
		
		// 右上.
		p.x = position.x + s.x/2.0f;
		p.y = position.y - s.y/2.0f;
		GUI.DrawTexture(new Rect(p.x, p.y, kado_size, kado_size), this.texture_kado_ru);
		
		// 左下.
		p.x = position.x - s.x/2.0f - kado_size;
		p.y = position.y + s.y/2.0f - kado_size;
		GUI.DrawTexture(new Rect(p.x, p.y, kado_size, kado_size), this.texture_kado_ld);
		
		// 右下.
		p.x = position.x + s.x/2.0f;
		p.y = position.y + s.y/2.0f - kado_size;
		GUI.DrawTexture(new Rect(p.x, p.y, kado_size, kado_size), this.texture_kado_rd);
		
		// べろ.
		p.x = position.x - kado_size;
		p.y = position.y + s.y/2.0f;
		GUI.DrawTexture(new Rect(p.x, p.y, kado_size, kado_size), this.texture_belo);
		
		GUI.color = Color.white;
	}

	void DrawText(string message, Vector2 position, Vector2 size)
	{
		if(message == "") {
			return;
		}

		GUIStyle style = new GUIStyle();
		style.fontSize = 16;
		style.normal.textColor =  Color.white;

		Vector2		balloon_size, text_size;
		
		text_size.x = message.Length*FONT_SIZE;
		text_size.y = FONG_HEIGHT;
		
		balloon_size.x = text_size.x + KADO_SIZE*2.0f;
		balloon_size.y = text_size.y + KADO_SIZE;

		Vector2		p;
		
		p.x = position.x - size.x/2.0f + KADO_SIZE;
		p.y = position.y - size.y/2.0f + KADO_SIZE;
		//p.x = position.x - text_size.x/2.0f;
		//p.y = position.y - text_size.y/2.0f;

		GUI.Label(new Rect(p.x, p.y, text_size.x, text_size.y), message, style);
	}

	void OnApplicationQuit() {
		if (m_transport != null) {
			m_transport.StopServer();
		}
	}

	public void OnEventHandling(NetEventState state)
	{
		switch (state.type) {
		case NetEventType.Connect:
			if (m_transport.IsServer()) {
				AddMessage(ref m_message[1], "だいずやさんがやってきました");
			}
			else {
				AddMessage(ref m_message[0], "とうふやさんとおでゃべりできます");
			}
			break;

		case NetEventType.Disconnect:
			if (m_transport.IsServer()) {
				AddMessage(ref m_message[0], "だいずやさんがかえっていきました");
			}
			else {
				AddMessage(ref m_message[1], "とうふやさんとがかえっていきました");
			}
			break;
		}
	}
}
