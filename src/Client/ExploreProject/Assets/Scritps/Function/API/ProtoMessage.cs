//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Option: missing-value detection (*Specified/ShouldSerialize*/Reset*) enabled
    
// Generated from: CreateRole.proto
namespace api
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CreatePlayerCG")]
  public partial class CreatePlayerCG :PBBody, global::ProtoBuf.IExtensible
  {
    public CreatePlayerCG() {}
    
    private string _passportId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"passportId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string passportId
    {
      get { return _passportId; }
      set { _passportId = value; }
    }
    private string _sessionId;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"sessionId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string sessionId
    {
      get { return _sessionId; }
      set { _sessionId = value; }
    }
    private string _roleName;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"roleName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string roleName
    {
      get { return _roleName; }
      set { _roleName = value; }
    }
    private int _profession;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"profession", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int profession
    {
      get { return _profession; }
      set { _profession = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CreatePlayerGC")]
  public partial class CreatePlayerGC :PBBody, global::ProtoBuf.IExtensible
  {
    public CreatePlayerGC() {}
    
    private long _roleId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roleId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public long roleId
    {
      get { return _roleId; }
      set { _roleId = value; }
    }
    private string _roleName;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"roleName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string roleName
    {
      get { return _roleName; }
      set { _roleName = value; }
    }
    private int _level;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"level", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int level
    {
      get { return _level; }
      set { _level = value; }
    }
    private int _profession;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"profession", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int profession
    {
      get { return _profession; }
      set { _profession = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}
// Generated from: Login.proto
namespace api
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LoginCG")]
  public partial class LoginCG :PBBody, global::ProtoBuf.IExtensible
  {
    public LoginCG() {}
    
    private string _username;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"username", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string username
    {
      get { return _username?? ""; }
      set { _username = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool usernameSpecified
    {
      get { return this._username != null; }
      set { if (value == (this._username== null)) this._username = value ? this.username : (string)null; }
    }
    private bool ShouldSerializeusername() { return usernameSpecified; }
    private void Resetusername() { usernameSpecified = false; }
    
    private string _password;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"password", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string password
    {
      get { return _password?? ""; }
      set { _password = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool passwordSpecified
    {
      get { return this._password != null; }
      set { if (value == (this._password== null)) this._password = value ? this.password : (string)null; }
    }
    private bool ShouldSerializepassword() { return passwordSpecified; }
    private void Resetpassword() { passwordSpecified = false; }
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LoginGC")]
  public partial class LoginGC :PBBody, global::ProtoBuf.IExtensible
  {
    public LoginGC() {}
    
    private int? _result;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"result", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int result
    {
      get { return _result?? default(int); }
      set { _result = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool resultSpecified
    {
      get { return this._result != null; }
      set { if (value == (this._result== null)) this._result = value ? this.result : (int?)null; }
    }
    private bool ShouldSerializeresult() { return resultSpecified; }
    private void Resetresult() { resultSpecified = false; }
    
    private int? _id;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int id
    {
      get { return _id?? default(int); }
      set { _id = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool idSpecified
    {
      get { return this._id != null; }
      set { if (value == (this._id== null)) this._id = value ? this.id : (int?)null; }
    }
    private bool ShouldSerializeid() { return idSpecified; }
    private void Resetid() { idSpecified = false; }
    
    private string _token;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"token", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string token
    {
      get { return _token?? ""; }
      set { _token = value; }
    }
    [global::System.Xml.Serialization.XmlIgnore]
    [global::System.ComponentModel.Browsable(false)]
    public bool tokenSpecified
    {
      get { return this._token != null; }
      set { if (value == (this._token== null)) this._token = value ? this.token : (string)null; }
    }
    private bool ShouldSerializetoken() { return tokenSpecified; }
    private void Resettoken() { tokenSpecified = false; }
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}
// Generated from: MessageID.proto
namespace api
{
    [global::ProtoBuf.ProtoContract(Name=@"MessageMapping")]
    public enum MessageMapping
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"Msg_CreatePlayerCG", Value=200)]
      Msg_CreatePlayerCG = 200,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Msg_CreatePlayerGC", Value=201)]
      Msg_CreatePlayerGC = 201,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Msg_LoginCG", Value=100)]
      Msg_LoginCG = 100,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Msg_LoginGC", Value=101)]
      Msg_LoginGC = 101
    }
  
}