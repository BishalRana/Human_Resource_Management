using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Human_Resource_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Human_Resource_Management.Service
{
    public class PositionService : IPosition
    {
        private ManagementContext _mgmtContext;

        public PositionService(ManagementContext managementContext)
        {
            _mgmtContext = managementContext;
        }

        public List<Position> GetAll()
        {
            var positions = from m in _mgmtContext.Position
                           select m;
            return positions.ToList();
        }

        public bool Create(Position position)
        {
            try
            {
                //checking if same position exists then return false to avoid duplicate creation of same position             
                List<Position> positionsExist = _mgmtContext.Position.Where(p => p.Position_Type == position.Position_Type).ToList();

                if (positionsExist.Count() == 1)
                {
                    return false;
                }

                _mgmtContext.Add(position);
                _mgmtContext.SaveChanges();
                return true;
            }
            catch (DbUpdateException due)
            {
                Debug.WriteLine(due.Message);
                return false;
            }

        }

        public Position GetPosition(int positionId)
        {
            try
            {
                return _mgmtContext.Position.Where(c => c.Id == positionId).ToList()[0];
            }
            catch (InvalidCastException ice)
            {
                Debug.WriteLine("Message " + ice.Message);
                return null;
            }
            catch (NullReferenceException nre)
            {
                Debug.WriteLine("Message " + nre.Message);
                return null;
            }
            catch(IndexOutOfRangeException iore)
            {
                Debug.Write("Message " + iore.Message);
                return null;
            }
        }

        public bool Update(Position position)
        {
            try
            {
                List<Position> positions = _mgmtContext.Position.Where(p => p.Id != position.Id && p.Position_Type == position.Position_Type).ToList();
                if (positions.Count() == 1)
                {
                    return false;
                }

                _mgmtContext.Update(position);
                _mgmtContext.SaveChangesAsync();

                return true;
            }
            catch (InvalidOperationException ioe)
            {
                Debug.WriteLine("Message " + ioe.Message);
                return false;
            }
            catch (DbUpdateConcurrencyException dce)
            {
                Debug.WriteLine("Message " + dce.Message);
                return false;
            }
        }

        public bool Delete(Position position)
        {
            try
            {
                _mgmtContext.Remove(position);
                _mgmtContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception  " + ex.Message);
                return false;
            }
        }



    }
}
