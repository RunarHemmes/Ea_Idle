using Ea_API.Interfaces;
using Ea_API.Models;

namespace Ea_API.Services
{
    public class GameProgressService : IGameProgressService
    {
        private readonly IGameProgressRepository _progressRepo;
        private readonly IAccountRepository _accountRepo;

        public GameProgressService(IGameProgressRepository repo, IAccountRepository accountRepo)
        {
            _progressRepo = repo;
            _accountRepo = accountRepo;
        }

        public (bool succes, GameProgress? progress, string? message) GetProgress(int accountId)
        {
            Account? account = _accountRepo.Get(accountId);
            if (account == null)
            {
                return (false, null, "This Id doesn't belong to an account.");
            }
            GameProgress? progress = _progressRepo.GetByAccountId(accountId);
            if (progress == null)
            {
                return (false, null, "This account doesn't have a save yet.");
            }
            return (true, progress, null);
        }

        public (bool succes, GameProgress? progress, string? message) UpdateProgress(GameProgress progress)
        {
            Account? account = _accountRepo.Get(progress.AccountId);
            if (account == null)
            {
                return (false, null, "This Id doesn't belong to an account.");
            }
            GameProgress? oldProgress = _progressRepo.GetByAccountId(progress.AccountId);
            if (oldProgress == null)
            {
                return (false, null, "This account doesn't have a save yet.");
            }
            GameProgress? newProgress = _progressRepo.Update(progress);
            if (newProgress == null)
            {
                return (false, null, "Update save failed.");
            }
            return (true, newProgress, null);
        }

        public (bool succes, GameProgress? progress, string? message) NewProgress(int accountId)
        {
            Account? account = _accountRepo.Get(accountId);
            if (account == null)
            {
                return (false, null, "This Id doesn't belong to an account.");
            }
            GameProgress? oldProgress = _progressRepo.GetByAccountId(accountId);
            if (oldProgress != null)
            {
                return (false, null, "This account already has a save.");
            }
            GameProgress newProgress = new GameProgress(accountId);
            newProgress = _progressRepo.Add(newProgress);
            return (true, newProgress, null);
        }
    }
}
