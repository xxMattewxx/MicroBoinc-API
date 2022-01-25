using MicroBoincAPI.Models.Binaries;
using MicroBoincAPI.Models.Platforms;
using MicroBoincAPI.Models.Projects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public Project GetProjectByID(long id)
        {
            return _context.Projects
                .Include(x => x.Group)
                .FirstOrDefault(x => x.ID == id);
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
