using Microsoft.AspNetCore.Mvc;
using MicroBoincAPI.Data.Accounts;
using MicroBoincAPI.Data.Projects;
using MicroBoincAPI.Data.Leaderboards;
using MicroBoincAPI.Dtos.Leaderboards;
using Microsoft.AspNetCore.Authorization;

namespace MicroBoincAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LeaderboardsController : ControllerBase
    {
        private readonly IAccountsRepo _accountsRepo;
        private readonly ILeaderboardsRepo _repository;
        private readonly IProjectsRepo _projectsRepo;

        public LeaderboardsController(IAccountsRepo accountsRepo, ILeaderboardsRepo repository, IProjectsRepo projectsRepo)
        {
            _accountsRepo = accountsRepo;
            _repository = repository;
            _projectsRepo = projectsRepo;
        }

        [HttpGet]
        [Authorize]
        [Route("ByProject/{projectID}")]
        public ActionResult<CurrentLeaderboardReadDto> GetCurrentLeaderboard(long projectID)
        {
            var project = _projectsRepo.GetProjectByID(projectID);
            if (project == null)
                return NotFound(new { Message = "Project not found" });

            var entries = _repository.GetSummedLeaderboardEntries(project);
            return Ok(new CurrentLeaderboardReadDto
            {
                ProjectName = project.Name,
                Entries = entries
            });
        }

        [HttpGet]
        [Authorize]
        [Route("ByProject/{projectID}/{discordID}")]
        public ActionResult<CurrentLeaderboardReadDto> GetCurrentLeaderboard(long projectID, long discordID)
        {
            var project = _projectsRepo.GetProjectByID(projectID);
            if (project == null)
                return NotFound(new { Message = "Project not found" });

            var account = _accountsRepo.GetAccountByDiscordID(discordID);
            if(account == null)
                return NotFound(new { Message = "Account not found" });

            var entries = _repository.GetLeaderboardEntries(project, account);
            return Ok(new CurrentLeaderboardReadDto
            {
                ProjectName = project.Name,
                Entries = entries
            });
        }

        [HttpGet]
        [Authorize]
        [Route("ByProject/{projectID}/Historical")]
        public ActionResult<HistoricalLeaderboardReadDto> GetHistoricalLeaderboard(long projectID)
        {
            var project = _projectsRepo.GetProjectByID(projectID);
            if (project == null)
                return NotFound(new { Message = "Project not found" });

            var entries = _repository.GetHistoricalLeaderboard(project);
            return Ok(new HistoricalLeaderboardReadDto
            {
                ProjectName = project.Name,
                Entries = entries
            });
        }
    }
}
