using MedManagementLibrary.DTO;
using MedManagementLibrary;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Clinic.Persistence
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper()
        {
            _connectionString = "Host=localhost;Port=5432;Username=lukestanton;Password=PASSWORD;Database=clinic";
        }

        // Create (Add) a new Physician
       public async Task<int> AddPhysicianAsync(Physician physician)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            string query = "INSERT INTO Physicians (Name, Birthday) VALUES (@name, @birthday) RETURNING Id";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("name", physician.Name);
            cmd.Parameters.AddWithValue("birthday", physician.Birthday ?? (object)DBNull.Value);

            var id = (int)await cmd.ExecuteScalarAsync();
            physician.Id = id;

            return id;
        }


        // Read (Get) all Physicians
        public async Task<List<Physician>> GetAllPhysiciansAsync()
        {
            var physicians = new List<Physician>();

            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            string query = "SELECT Id, Name, Birthday FROM Physicians";
            using var cmd = new NpgsqlCommand(query, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                physicians.Add(new Physician
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Birthday = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2)
                });
            }

            return physicians;
        }

        // Update an existing Physician
        public async Task UpdatePhysicianAsync(Physician physician)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            string query = "UPDATE Physicians SET Name = @name, Birthday = @birthday WHERE Id = @id";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("name", physician.Name);
            cmd.Parameters.AddWithValue("birthday", physician.Birthday ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("id", physician.Id);

            await cmd.ExecuteNonQueryAsync();
        }

        // Delete a Physician
        public async Task DeletePhysicianAsync(int physicianId)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            string query = "DELETE FROM Physicians WHERE Id = @id";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("id", physicianId);

            await cmd.ExecuteNonQueryAsync();
        }

        // Search Physicians by Name
        public async Task<List<Physician>> SearchPhysiciansAsync(string name)
        {
            var physicians = new List<Physician>();

            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            string query = "SELECT Id, Name, Birthday FROM Physicians WHERE Name ILIKE @name";
            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("name", $"%{name}%");

            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                physicians.Add(new Physician
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Birthday = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2)
                });
            }

            return physicians;
        }
    }
}
