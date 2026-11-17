using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FitnessApp.Data;
using FitnessApp.Models;

namespace FitnessApp.Repositories
{
    public class SetLogRepository
    {
        private readonly AppDbContext _context;

        public SetLogRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Save(SetLog entity)
        {
            if (entity.set_log_id == 0)
                _context.SetLogs.Add(entity);
            else
                _context.Entry(entity).State = EntityState.Modified;

            _context.SaveChanges();
        }

        public List<SetLog> GetBySessionId(int sessionId)
        {
            return _context.SetLogs
                .Where(s => s.workout_session_id == sessionId)
                .OrderBy(s => s.set_number)
                .ToList();
        }
    }
}