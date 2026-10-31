using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using HealthComm.Models;
using System.IO;


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

        public async Task<List<Session>> ImportSessionsFromCsv()
        {
            await Init();
            string fileName = "Sessions.csv";
            string folderPath = Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData);

            string filePath = Path.Combine(
                folderPath,
                "Sessions.csv");

            var lines = await File.ReadAllLinesAsync(filePath);

            if (lines.Length <= 1)
                return null;

            var sessions = new List<Session>();

            foreach (string line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var fields = ParseCsvLine(line);

                if (fields.Count < 8)
                    continue;

                if (!int.TryParse(fields[0], out int id))
                    continue;

                sessions.Add(new Session
                {
                    Id = id,
                    AppointmentDate = fields[1],
                    VisitType = fields[2],
                    PatientName = fields[3],
                    DrugName = fields[4],
                    Dosage = fields[5],
                    Email = fields[6],
                    IsFavorite = bool.TryParse(
                        fields[7], out bool favorite) && favorite
                });
            }

            foreach (var session in sessions)
            {
                // Insert only if this ID does not already exist.
                var existing = await _connection
                    .Table<Session>()
                    .Where(s => s.Id == session.Id)
                    .FirstOrDefaultAsync();

                if (existing == null)
                {
                    // Preserve the imported ID.
                    await _connection.InsertAsync(session);
                }
            }

            return sessions;
        }

        private static List<string> ParseCsvLine(string line)
        {
            var fields = new List<string>();
            bool inQuotes = false;
            var field = new System.Text.StringBuilder();

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    if (inQuotes && i + 1 < line.Length
                                 && line[i + 1] == '"')
                    {
                        field.Append('"');
                        i++;
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == ',' && !inQuotes)
                {
                    fields.Add(field.ToString());
                    field.Clear();
                }
                else
                {
                    field.Append(c);
                }
            }

            fields.Add(field.ToString());
            return fields;
        }

        public async Task<string> ExportSessionsToCsv()
        {
            List<Session> sessions = await GetSessions();

            string fileName = "Sessions.csv";
            string folderPath = Environment.GetFolderPath(
    Environment.SpecialFolder.LocalApplicationData);

            string filePath = Path.Combine(
                folderPath,
                "Sessions.csv");
            
            using (var writer = new StreamWriter(
                filePath, false, Encoding.UTF8))
            {
                // CSV header
                await writer.WriteLineAsync(
                    "Id,AppointmentDate,VisitType,PatientName,DrugName,Dosage,IsFavorite");

                foreach (var session in sessions)
                {
                    string line = string.Join(",",
                        session.Id,
                        EscapeCsv(session.AppointmentDate),
                        EscapeCsv(session.VisitType),
                        EscapeCsv(session.PatientName),
                        EscapeCsv(session.DrugName),
                        EscapeCsv(session.Dosage),
                        EscapeCsv(session.Email),
                        session.IsFavorite);

                    await writer.WriteLineAsync(line);
                }
            }

            return filePath;
        }

        private static string EscapeCsv(string value)
        {
            value ??= string.Empty;

            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        public async Task<Session> GetSessionByEmail(string email)
        {
            await Init();

            return await _connection.Table<Session>()
                .Where(s => s.Email == email)
                .FirstOrDefaultAsync();
        }
    }
}
