using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace DAL
{
    public class ChatRepository
    {
        private string _connectionString;

        public ChatRepository()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();
            _connectionString = config["defaultConnection"];
        }

        public IEnumerable<Message> GetAll()
        {
            string commandText = "SELECT * FROM [Messages] ORDER BY [Time]";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            yield return Message.FromReader(reader);
                        }
                    }
                }
            }
        }

        public int GetLikesCount(int messageId)
        {
            string commandText = "SELECT COUNT(*) FROM [Emotions] WHERE [MessageId] = @MessageId AND [IsLike] = 1";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@MessageId", messageId);

                    connection.Open();

                    return (int)command.ExecuteScalar();
                }
            }
        }

        public int GetDislikesCount(int messageId)
        {
            string commandText = "SELECT COUNT(*) FROM [Emotions] WHERE [MessageId] = @MessageId AND [IsLike] = 0";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@MessageId", messageId);

                    connection.Open();

                    return (int)command.ExecuteScalar();
                }
            }
        }
    }
}
