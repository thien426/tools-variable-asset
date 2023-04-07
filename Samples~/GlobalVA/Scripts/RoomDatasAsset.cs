using System;
using T70.VariableAsset;
using UnityEngine;

[Serializable]
public class RoomData
{
    
}

public class RoomDataAsset : AssetT<RoomData>
{
    
}

[CreateAssetMenu(fileName = "List Room Data", menuName = VariableConst.MenuPath.ListRoomData, order = VariableConst.Order.ListRoomData)]
public class RoomDatasAsset : ListAsset<RoomData, RoomDataAsset>
{
    
}
