using AutoMapper;
using MicroBoincAPI.Dtos.Binaries;
using MicroBoincAPI.Dtos.Projects;
using MicroBoincAPI.Models.Binaries;
using MicroBoincAPI.Models.Platforms;
using MicroBoincAPI.Models.Projects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroBoincAPI.Data.Binaries
{
    public class BinariesRepo : IBinariesRepo
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public BinariesRepo(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void CreateProjectBinary(ProjectBinary projectBinary)
        {
            _context.ProjectsBinaries.Add(projectBinary);
        }

        public void DeprecateAllBinaries(Project project, Platform platform)
        {
            var matching = _context.ProjectsBinaries
                .Where(x => x.ProjectID == project.ID)
                .Where(x => x.PlatformID == platform.ID);
            
            foreach(var aux in matching)
                aux.Deprecated = true;
        }

        public GetProjectsForPlatformsResponseDto GetProjectsBinariesForPlatforms(IEnumerable<long> platformsIDs)
        {
            var projectsBinaries = _context.ProjectsBinaries
                .Where(x => x.Deprecated == false)
                .Where(x => platformsIDs.Contains(x.PlatformID));

            return new GetProjectsForPlatformsResponseDto
            {
                ProjectsBinaries = _mapper.Map<IEnumerable<ProjectBinaryReadDto>>(
                    projectsBinaries
                        .Include(x => x.Binary)
                        .Include(x => x.Project)
                        .ToList()
                ),
                ProjectsIDs = projectsBinaries.Select(x => x.ProjectID).ToList()
            };
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
