using System;
using System.Collections.Generic;
using Human_Resource_Management.Models;

namespace Human_Resource_Management.Service
{
    public interface IPosition
    {
         List<Position> GetAll();
        bool Create(Position position);
        Position GetPosition(int positionId);
        bool Update(Position position);
        bool Delete(Position position);

    }
}
