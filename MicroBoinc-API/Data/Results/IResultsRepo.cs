using MicroBoincAPI.Models.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MicroBoincAPI.Data.Results
{
    public interface IResultsRepo
    {
        public void AttachEntity(object obj);
        public void CreateResult(Result result);
        public IEnumerable<Result> GetResultsForTask(long taskID);
        public void StreamResults(long projectID, StreamWriter writer);

        public void ResetContext();
        public bool SaveChanges();
    }
}
