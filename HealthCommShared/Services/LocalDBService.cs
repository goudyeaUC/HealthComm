using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using HealthComm.Models;

namespace HealthCommShared.Services
{
    public class LocalDBService
    {
        private const string DB_NAME = "demo_db.db4";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDBService()
        {
            var dbPath = Path.Combine(
                Environment.CurrentDirectory,
                DB_NAME);

            _connection = new SQLiteAsyncConnection(dbPath);
        }

        private async Task Init()
        {
            await _connection.CreateTableAsync<Session>();
        }

        public async Task<List<Session>> GetSessions()
        {
            await Init();
            return await _connection.Table<Session>().ToListAsync();
        }
        public async Task<Session> GetById(int id)
        {
            await Init();
            return await _connection.Table<Session>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(Session session)
        {
            await Init();
            await _connection.InsertAsync(session);
        }

        public async Task Update(Session session)
        {
            await Init();
            await _connection.UpdateAsync(session);
        }

        public async Task Delete(Session session)
        {
            await Init();
            await _connection.DeleteAsync(session);
        }

        public async Task<List<Session>> GetFavorites()
        {
            await Init();
            return await _connection.Table<Session>().Where(s => s.IsFavorite).ToListAsync();
        }

        public async Task ToggleFavorite(Session session)
        {
            await Init();
            session.IsFavorite = !session.IsFavorite;
            await _connection.UpdateAsync(session);
        }
    }
}
