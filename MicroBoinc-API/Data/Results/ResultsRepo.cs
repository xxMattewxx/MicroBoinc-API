using MicroBoincAPI.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IO;

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

        public void StreamResults(long projectID, StreamWriter writer)
        {
            using var connection = _context.Database.GetDbConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = $"SELECT \"StdOut\" FROM \"Results\" WHERE \"ProjectID\" = {projectID}";

            using var reader = command.ExecuteReader();

            while(reader.Read())
            {
                writer.WriteLine(reader.GetString(0));
            }
            writer.Flush();
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
