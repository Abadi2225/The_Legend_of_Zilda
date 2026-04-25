using System.Collections.Generic;

public static class DungeonState
{
    private static Dictionary<string, RoomState> rooms = new();

    public static RoomState GetRoomState(string roomID)
    {
        if (!rooms.ContainsKey(roomID))
            rooms[roomID] = new RoomState();
        
        // Check what rooms are stored in DunegonState
        // System.Console.WriteLine("Rooms in DungeonState");
        // foreach (KeyValuePair<string, RoomState> item in rooms)
        // {
        //     System.Console.WriteLine("- " + item.Key);
        // }
        // System.Console.WriteLine("rooms in dungeonState\n");
        

        return rooms[roomID];
    }
}