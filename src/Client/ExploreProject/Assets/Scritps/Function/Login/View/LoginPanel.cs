using api;
using GameCore;
using GameExplore;
using UnityEngine;

public class LoginPanel : BasePanel
{
    public UIInput Username;
    public UIInput Password;

    public UIButton EnterGame;

    void Start()
    {
        if(EnterGame != null) {
            UIEventListener.Get(EnterGame.gameObject).onClick += OnEnterGameClick;
        }
    }

    void OnEnterGameClick(GameObject go)
    {
        string name = Username.value;
        string word = Password.value;

        Debug.Log(string.Format("username:{0}", name));
        Debug.Log(string.Format("password:{0}", word));

        if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(word)) {
            Debug.LogError("username or password is null!");
            return;
        }


        CreatePlayerCG msg2 = new CreatePlayerCG();
        msg2.passportId = "aa";
        msg2.profession = 1;
        msg2.roleName = "abaojin";
        msg2.sessionId = "id";

        LoginCG msg = new LoginCG();
        msg.username = name;
        msg.password = word;

        NetworkManager.Instance.SendMessage(msg);

        // Test
        GameFacade.SendNotification(BagGetBagCommand.NAME);
    }
}
