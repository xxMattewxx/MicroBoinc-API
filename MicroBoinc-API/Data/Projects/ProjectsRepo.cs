using MicroBoincAPI.Models.Tasks;
using MicroBoincAPI.Models.Binaries;
using MicroBoincAPI.Models.Projects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroBoincAPI.Data.Projects
{
    public class ProjectsRepo : IProjectsRepo
    {
        private readonly AppDbContext _context;

        public ProjectsRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateProject(Project project)
        {
            _context.Projects.Add(project);
        }

        public IEnumerable<long> GetProjectsIDs()
        {
            return _context.Projects.Select(x => x.ID);
        }

        public Project GetProjectByID(long id)
        {
            return _context.Projects
                .Include(x => x.Group)
                .FirstOrDefault(x => x.ID == id);
        }

        public (int totalDone, int totalGenerated) GetProjectProgress(long id)
        {
            var tasks = _context.Tasks.Where(x => x.ProjectID == id);
            var completedTasks = tasks.Where(x => x.Status == TaskStatus.Completed);
            return (completedTasks.Count(), tasks.Count());
        }

        //DEPRECATED / TODO
        public IEnumerable<ProjectBinary> GetProjectsBinariesForPlatforms(IEnumerable<long> acceptedPlatforms)
        {
            return _context.ProjectsBinaries
                .Where(x => acceptedPlatforms.Contains(x.PlatformID));
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
