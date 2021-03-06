//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: protocol.proto
namespace protocol
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"MeteorMsg")]
  public partial class MeteorMsg : global::ProtoBuf.IExtensible
  {
    public MeteorMsg() {}
    
    private MeteorMsg.MsgType _cmd;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"cmd", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public MeteorMsg.MsgType cmd
    {
      get { return _cmd; }
      set { _cmd = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"MsgType")]
    public enum MsgType
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"GetRoomReq", Value=100)]
      GetRoomReq = 100,
            
      [global::ProtoBuf.ProtoEnum(Name=@"GetRoomRsp", Value=101)]
      GetRoomRsp = 101,
            
      [global::ProtoBuf.ProtoEnum(Name=@"CreateRoomReq", Value=102)]
      CreateRoomReq = 102,
            
      [global::ProtoBuf.ProtoEnum(Name=@"CreateRoomRsp", Value=103)]
      CreateRoomRsp = 103,
            
      [global::ProtoBuf.ProtoEnum(Name=@"JoinRoomReq", Value=104)]
      JoinRoomReq = 104,
            
      [global::ProtoBuf.ProtoEnum(Name=@"JoinRoomRsp", Value=105)]
      JoinRoomRsp = 105,
            
      [global::ProtoBuf.ProtoEnum(Name=@"OnJoinRoomRsp", Value=106)]
      OnJoinRoomRsp = 106,
            
      [global::ProtoBuf.ProtoEnum(Name=@"EnterLevelReq", Value=107)]
      EnterLevelReq = 107,
            
      [global::ProtoBuf.ProtoEnum(Name=@"EnterLevelRsp", Value=108)]
      EnterLevelRsp = 108,
            
      [global::ProtoBuf.ProtoEnum(Name=@"OnEnterLevelRsp", Value=109)]
      OnEnterLevelRsp = 109,
            
      [global::ProtoBuf.ProtoEnum(Name=@"LeaveRoomReq", Value=110)]
      LeaveRoomReq = 110,
            
      [global::ProtoBuf.ProtoEnum(Name=@"OnLeaveRoomRsp", Value=111)]
      OnLeaveRoomRsp = 111,
            
      [global::ProtoBuf.ProtoEnum(Name=@"InputReq", Value=112)]
      InputReq = 112,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SyncInput", Value=113)]
      SyncInput = 113,
            
      [global::ProtoBuf.ProtoEnum(Name=@"KeyFrameReq", Value=114)]
      KeyFrameReq = 114,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SyncKeyFrame", Value=115)]
      SyncKeyFrame = 115
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RoomInfo")]
  public partial class RoomInfo : global::ProtoBuf.IExtensible
  {
    public RoomInfo() {}
    
    private uint _roomId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"roomId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint roomId
    {
      get { return _roomId; }
      set { _roomId = value; }
    }
    private string _roomName;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"roomName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string roomName
    {
      get { return _roomName; }
      set { _roomName = value; }
    }
    private RoomInfo.RoomRule _rule;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"rule", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public RoomInfo.RoomRule rule
    {
      get { return _rule; }
      set { _rule = value; }
    }
    private uint _levelIdx;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"levelIdx", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint levelIdx
    {
      get { return _levelIdx; }
      set { _levelIdx = value; }
    }
    private uint _Group1;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"Group1", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint Group1
    {
      get { return _Group1; }
      set { _Group1 = value; }
    }
    private uint _Group2;
    [global::ProtoBuf.ProtoMember(6, IsRequired = true, Name=@"Group2", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint Group2
    {
      get { return _Group2; }
      set { _Group2 = value; }
    }
    private uint _playerCount;
    [global::ProtoBuf.ProtoMember(7, IsRequired = true, Name=@"playerCount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint playerCount
    {
      get { return _playerCount; }
      set { _playerCount = value; }
    }
    private uint _maxPlayer;
    [global::ProtoBuf.ProtoMember(8, IsRequired = true, Name=@"maxPlayer", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint maxPlayer
    {
      get { return _maxPlayer; }
      set { _maxPlayer = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"RoomPattern")]
    public enum RoomPattern
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"Normal", Value=1)]
      Normal = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Replay", Value=2)]
      Replay = 2
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"RoomRule")]
    public enum RoomRule
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"SinglePlayer", Value=1)]
      SinglePlayer = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"AllDead", Value=2)]
      AllDead = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"LeaderMustDead", Value=3)]
      LeaderMustDead = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"DefenceBase", Value=4)]
      DefenceBase = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"Loot", Value=5)]
      Loot = 5
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"OnLeaveRoomRsp")]
  public partial class OnLeaveRoomRsp : global::ProtoBuf.IExtensible
  {
    public OnLeaveRoomRsp() {}
    
    private uint _playerId;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"playerId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint playerId
    {
      get { return _playerId; }
      set { _playerId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GetRoomRsp")]
  public partial class GetRoomRsp : global::ProtoBuf.IExtensible
  {
    public GetRoomRsp() {}
    
    private readonly global::System.Collections.Generic.List<RoomInfo> _RoomInLobby = new global::System.Collections.Generic.List<RoomInfo>();
    [global::ProtoBuf.ProtoMember(2, Name=@"RoomInLobby", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<RoomInfo> RoomInLobby
    {
      get { return _RoomInLobby; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CreateRoomReq")]
  public partial class CreateRoomReq : global::ProtoBuf.IExtensible
  {
    public CreateRoomReq() {}
    
    private uint _maxPlayer;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"maxPlayer", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint maxPlayer
    {
      get { return _maxPlayer; }
      set { _maxPlayer = value; }
    }
    private uint _levelIdx;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"levelIdx", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint levelIdx
    {
      get { return _levelIdx; }
      set { _levelIdx = value; }
    }
    private uint _rule;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"rule", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint rule
    {
      get { return _rule; }
      set { _rule = value; }
    }
    private string _roomName;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"roomName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string roomName
    {
      get { return _roomName; }
      set { _roomName = value; }
    }
    private uint _hpMax;
    [global::ProtoBuf.ProtoMember(6, IsRequired = true, Name=@"hpMax", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint hpMax
    {
      get { return _hpMax; }
      set { _hpMax = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CreateRoomRsp")]
  public partial class CreateRoomRsp : global::ProtoBuf.IExtensible
  {
    public CreateRoomRsp() {}
    
    private uint _result;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"result", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint result
    {
      get { return _result; }
      set { _result = value; }
    }
    private uint _roomId;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"roomId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint roomId
    {
      get { return _roomId; }
      set { _roomId = value; }
    }
    private uint _levelId;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"levelId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint levelId
    {
      get { return _levelId; }
      set { _levelId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"JoinRoomReq")]
  public partial class JoinRoomReq : global::ProtoBuf.IExtensible
  {
    public JoinRoomReq() {}
    
    private uint _roomId;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"roomId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint roomId
    {
      get { return _roomId; }
      set { _roomId = value; }
    }
    private string _userNick;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"userNick", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string userNick
    {
      get { return _userNick; }
      set { _userNick = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"JoinRoomRsp")]
  public partial class JoinRoomRsp : global::ProtoBuf.IExtensible
  {
    public JoinRoomRsp() {}
    
    private uint _result;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"result", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint result
    {
      get { return _result; }
      set { _result = value; }
    }
    private uint _reason;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"reason", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint reason
    {
      get { return _reason; }
      set { _reason = value; }
    }
    private uint _levelIdx;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"levelIdx", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint levelIdx
    {
      get { return _levelIdx; }
      set { _levelIdx = value; }
    }
    private uint _roomId;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"roomId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint roomId
    {
      get { return _roomId; }
      set { _roomId = value; }
    }
    private uint _playerId;
    [global::ProtoBuf.ProtoMember(6, IsRequired = true, Name=@"playerId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint playerId
    {
      get { return _playerId; }
      set { _playerId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"EnterLevelReq")]
  public partial class EnterLevelReq : global::ProtoBuf.IExtensible
  {
    public EnterLevelReq() {}
    
    private uint _camp;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"camp", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint camp
    {
      get { return _camp; }
      set { _camp = value; }
    }
    private uint _model;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"model", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint model
    {
      get { return _model; }
      set { _model = value; }
    }
    private uint _weapon;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"weapon", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint weapon
    {
      get { return _weapon; }
      set { _weapon = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"EnterLevelRsp")]
  public partial class EnterLevelRsp : global::ProtoBuf.IExtensible
  {
    public EnterLevelRsp() {}
    
    private SceneInfo _scene = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"scene", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public SceneInfo scene
    {
      get { return _scene; }
      set { _scene = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"OnEnterRoomRsp")]
  public partial class OnEnterRoomRsp : global::ProtoBuf.IExtensible
  {
    public OnEnterRoomRsp() {}
    
    private string _playerNick;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"playerNick", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string playerNick
    {
      get { return _playerNick; }
      set { _playerNick = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"OnEnterLevelRsp")]
  public partial class OnEnterLevelRsp : global::ProtoBuf.IExtensible
  {
    public OnEnterLevelRsp() {}
    
    private Player_ _player;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"player", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Player_ player
    {
      get { return _player; }
      set { _player = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Vector2_")]
  public partial class Vector2_ : global::ProtoBuf.IExtensible
  {
    public Vector2_() {}
    
    private int _x;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"x", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int x
    {
      get { return _x; }
      set { _x = value; }
    }
    private int _y;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"y", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int y
    {
      get { return _y; }
      set { _y = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Vector3_")]
  public partial class Vector3_ : global::ProtoBuf.IExtensible
  {
    public Vector3_() {}
    
    private int _x;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"x", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int x
    {
      get { return _x; }
      set { _x = value; }
    }
    private int _y;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"y", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int y
    {
      get { return _y; }
      set { _y = value; }
    }
    private int _z;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"z", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int z
    {
      get { return _z; }
      set { _z = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Quaternion_")]
  public partial class Quaternion_ : global::ProtoBuf.IExtensible
  {
    public Quaternion_() {}
    
    private int _x;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"x", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int x
    {
      get { return _x; }
      set { _x = value; }
    }
    private int _y;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"y", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int y
    {
      get { return _y; }
      set { _y = value; }
    }
    private int _z;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"z", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int z
    {
      get { return _z; }
      set { _z = value; }
    }
    private int _w;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"w", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int w
    {
      get { return _w; }
      set { _w = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"SceneInfo")]
  public partial class SceneInfo : global::ProtoBuf.IExtensible
  {
    public SceneInfo() {}
    
    private readonly global::System.Collections.Generic.List<SceneItem_> _items = new global::System.Collections.Generic.List<SceneItem_>();
    [global::ProtoBuf.ProtoMember(1, Name=@"items", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<SceneItem_> items
    {
      get { return _items; }
    }
  
    private readonly global::System.Collections.Generic.List<Player_> _players = new global::System.Collections.Generic.List<Player_>();
    [global::ProtoBuf.ProtoMember(2, Name=@"players", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Player_> players
    {
      get { return _players; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"SceneItem_")]
  public partial class SceneItem_ : global::ProtoBuf.IExtensible
  {
    public SceneItem_() {}
    
    private string _model;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"model", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string model
    {
      get { return _model; }
      set { _model = value; }
    }
    private Vector3_ _pos;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"pos", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Vector3_ pos
    {
      get { return _pos; }
      set { _pos = value; }
    }
    private Quaternion_ _rotation;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"rotation", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Quaternion_ rotation
    {
      get { return _rotation; }
      set { _rotation = value; }
    }
    private int _frame;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"frame", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int frame
    {
      get { return _frame; }
      set { _frame = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Player_")]
  public partial class Player_ : global::ProtoBuf.IExtensible
  {
    public Player_() {}
    
    private uint _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint id
    {
      get { return _id; }
      set { _id = value; }
    }
    private string _name;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }
    private uint _weapon1;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"weapon1", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint weapon1
    {
      get { return _weapon1; }
      set { _weapon1 = value; }
    }
    private uint _weapon2;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"weapon2", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint weapon2
    {
      get { return _weapon2; }
      set { _weapon2 = value; }
    }
    private uint _weapon;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"weapon", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint weapon
    {
      get { return _weapon; }
      set { _weapon = value; }
    }
    private uint _weapon_pos;
    [global::ProtoBuf.ProtoMember(6, IsRequired = true, Name=@"weapon_pos", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint weapon_pos
    {
      get { return _weapon_pos; }
      set { _weapon_pos = value; }
    }
    private Vector3_ _pos;
    [global::ProtoBuf.ProtoMember(7, IsRequired = true, Name=@"pos", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Vector3_ pos
    {
      get { return _pos; }
      set { _pos = value; }
    }
    private Quaternion_ _rotation;
    [global::ProtoBuf.ProtoMember(8, IsRequired = true, Name=@"rotation", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Quaternion_ rotation
    {
      get { return _rotation; }
      set { _rotation = value; }
    }
    private int _model;
    [global::ProtoBuf.ProtoMember(9, IsRequired = true, Name=@"model", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int model
    {
      get { return _model; }
      set { _model = value; }
    }
    private int _aniSource;
    [global::ProtoBuf.ProtoMember(10, IsRequired = true, Name=@"aniSource", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int aniSource
    {
      get { return _aniSource; }
      set { _aniSource = value; }
    }
    private int _frame;
    [global::ProtoBuf.ProtoMember(11, IsRequired = true, Name=@"frame", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int frame
    {
      get { return _frame; }
      set { _frame = value; }
    }
    private int _hpMax;
    [global::ProtoBuf.ProtoMember(12, IsRequired = true, Name=@"hpMax", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int hpMax
    {
      get { return _hpMax; }
      set { _hpMax = value; }
    }
    private int _hp;
    [global::ProtoBuf.ProtoMember(13, IsRequired = true, Name=@"hp", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int hp
    {
      get { return _hp; }
      set { _hp = value; }
    }
    private int _angry;
    [global::ProtoBuf.ProtoMember(14, IsRequired = true, Name=@"angry", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int angry
    {
      get { return _angry; }
      set { _angry = value; }
    }
    private int _Camp;
    [global::ProtoBuf.ProtoMember(15, IsRequired = true, Name=@"Camp", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int Camp
    {
      get { return _Camp; }
      set { _Camp = value; }
    }
    private int _SpawnPoint;
    [global::ProtoBuf.ProtoMember(16, IsRequired = true, Name=@"SpawnPoint", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int SpawnPoint
    {
      get { return _SpawnPoint; }
      set { _SpawnPoint = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"InputReq")]
  public partial class InputReq : global::ProtoBuf.IExtensible
  {
    public InputReq() {}
    
    private readonly global::System.Collections.Generic.List<Input_> _input = new global::System.Collections.Generic.List<Input_>();
    [global::ProtoBuf.ProtoMember(1, Name=@"input", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Input_> input
    {
      get { return _input; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Input_")]
  public partial class Input_ : global::ProtoBuf.IExtensible
  {
    public Input_() {}
    
    private uint _playerId;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"playerId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint playerId
    {
      get { return _playerId; }
      set { _playerId = value; }
    }
    private Vector2_ _JoyStick;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"JoyStick", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Vector2_ JoyStick
    {
      get { return _JoyStick; }
      set { _JoyStick = value; }
    }
    private Vector2_ _MouseDelta;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"MouseDelta", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public Vector2_ MouseDelta
    {
      get { return _MouseDelta; }
      set { _MouseDelta = value; }
    }
    private byte[] _w;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"w", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public byte[] w
    {
      get { return _w; }
      set { _w = value; }
    }
    private byte[] _s;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"s", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public byte[] s
    {
      get { return _s; }
      set { _s = value; }
    }
    private byte[] _a;
    [global::ProtoBuf.ProtoMember(6, IsRequired = true, Name=@"a", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public byte[] a
    {
      get { return _a; }
      set { _a = value; }
    }
    private byte[] _d;
    [global::ProtoBuf.ProtoMember(7, IsRequired = true, Name=@"d", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public byte[] d
    {
      get { return _d; }
      set { _d = value; }
    }
    private byte[] _jump;
    [global::ProtoBuf.ProtoMember(8, IsRequired = true, Name=@"jump", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public byte[] jump
    {
      get { return _jump; }
      set { _jump = value; }
    }
    private byte[] _break;
    [global::ProtoBuf.ProtoMember(9, IsRequired = true, Name=@"break", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public byte[] @break
    {
      get { return _break; }
      set { _break = value; }
    }
    private byte[] _attack;
    [global::ProtoBuf.ProtoMember(10, IsRequired = true, Name=@"attack", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public byte[] attack
    {
      get { return _attack; }
      set { _attack = value; }
    }
    private byte[] _e;
    [global::ProtoBuf.ProtoMember(11, IsRequired = true, Name=@"e", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public byte[] e
    {
      get { return _e; }
      set { _e = value; }
    }
    private byte[] _r;
    [global::ProtoBuf.ProtoMember(12, IsRequired = true, Name=@"r", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public byte[] r
    {
      get { return _r; }
      set { _r = value; }
    }
    private byte[] _y;
    [global::ProtoBuf.ProtoMember(13, IsRequired = true, Name=@"y", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public byte[] y
    {
      get { return _y; }
      set { _y = value; }
    }
    private byte[] _space;
    [global::ProtoBuf.ProtoMember(14, IsRequired = true, Name=@"space", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public byte[] space
    {
      get { return _space; }
      set { _space = value; }
    }
    private byte[] _c;
    [global::ProtoBuf.ProtoMember(15, IsRequired = true, Name=@"c", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public byte[] c
    {
      get { return _c; }
      set { _c = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"KeyFrame")]
  public partial class KeyFrame : global::ProtoBuf.IExtensible
  {
    public KeyFrame() {}
    
    private uint _frameIndex;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"frameIndex", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint frameIndex
    {
      get { return _frameIndex; }
      set { _frameIndex = value; }
    }
    private readonly global::System.Collections.Generic.List<Input_> _Inputs = new global::System.Collections.Generic.List<Input_>();
    [global::ProtoBuf.ProtoMember(2, Name=@"Inputs", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Input_> Inputs
    {
      get { return _Inputs; }
    }
  
    private readonly global::System.Collections.Generic.List<Player_> _Players = new global::System.Collections.Generic.List<Player_>();
    [global::ProtoBuf.ProtoMember(3, Name=@"Players", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Player_> Players
    {
      get { return _Players; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}