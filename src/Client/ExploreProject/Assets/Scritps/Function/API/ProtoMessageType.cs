using System;
using api;

public class PBMessageType
{
	public static Type GetMessageType(MessageMapping messageId)
	{
		switch (messageId){
			case MessageMapping.Msg_CreatePlayerCG: return typeof(CreatePlayerCG); 
			case MessageMapping.Msg_CreatePlayerGC: return typeof(CreatePlayerGC); 
			case MessageMapping.Msg_LoginCG: return typeof(LoginCG); 
			case MessageMapping.Msg_LoginGC: return typeof(LoginGC); 
		}
		return null;
	}
}
