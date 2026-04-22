using System.Collections;
using Battleship.Enum;
using Battleship.Interfaces;

namespace Battleship.Models;

public class Ship : IShip
{
    public ShipType ShipType { get; set;}
    public int Size { get; set; }
    public int Hits { get; set; }
    public Orientation Orientation { get; set; }
    public Position Position {get; set;}

    public Ship(ShipType shipType, Position position, Orientation orientation)
    {
        ShipType = shipType;
        Hits = 0;
        Orientation = orientation;
        Position = position;

        switch (shipType)
        {
            case ShipType.Carrier:
             Size = 5;
             break;
            case ShipType.Battleship:
             Size = 4;
             break;
            case ShipType.Cruiser:
             Size = 3;
             break;
            case ShipType.Destroyer:
             Size = 3;
             break;
            case ShipType.PatrolBoat:
             Size = 2;
             break;
        }
    }
    
}