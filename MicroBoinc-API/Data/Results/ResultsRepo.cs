using MicroBoincAPI.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Data.Results
{
    public class ResultsRepo : IResultsRepo
    {
        private readonly AppDbContext _context;

        public ResultsRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateResult(Result result)
        {
            _context.Results.Add(result);
        }

        public IEnumerable<Result> GetResultsForTask(long taskID)
        {
            return _context.Results
                .Where(x => x.TaskID == taskID);
        }

        public void AttachEntity(object obj)
        {
            _context.Attach(obj);
        }

        public void ResetContext()
        {
            _context.ChangeTracker.Clear();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
