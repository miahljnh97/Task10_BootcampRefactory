using System;
using Npgsql;
using DI.Model;
using System.Collections.Generic;

namespace DI
{
    public interface IDatabase
    {

        List<Post> ReadPost();
        List<Post> ReadById(int id);
        int Create(Post post);
        int UpdatePost(Post post, int id);
        void DeletePost(int id);


    }

    public class Database : IDatabase
    {
        NpgsqlConnection _connection;

        public Database(NpgsqlConnection connection)
        {
            _connection = connection;
            _connection.Open();
        }

        public List<Post> ReadPost()
        {
            var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM post";
            var result = command.ExecuteReader();
            var newPost = new List<Post>();

            while (result.Read())
                newPost.Add(new Post()
                {
                    Id = (int)result[0],
                    Title = (string)result[1],
                    Content = (string)result[2],
                    Tags = (string)result[3],
                    Status = (bool)result[4],
                    Create_time = (DateTime)result[5],
                    Update_time = (DateTime)result[6]
                });

            _connection.Close();
            return newPost;
        }

        public List<Post> ReadById(int id)
        {
            var command = _connection.CreateCommand();
            command.CommandText = "SELECT * FROM post WHERE id = @id";

            command.Parameters.AddWithValue("@id", id);
            var result = command.ExecuteReader();
            var newPost = new List<Post>();

            while (result.Read())
                newPost.Add(new Post()
                {
                    Id = (int)result[0],
                    Title = (string)result[1],
                    Content = (string)result[2],
                    Tags = (string)result[3],
                    Status = (bool)result[4],
                    Create_time = (DateTime)result[5],
                    Update_time = (DateTime)result[6]
                });

            _connection.Close();
            return newPost;
        }

        public int Create(Post post)
        {
            var command = _connection.CreateCommand();
            command.CommandText = "INSERT INTO Post (title, content, tags, status, create_time, update_time) VALUES (@title, @content, @tags, @status, @create_time, @update_time) RETURNING id";

            command.Parameters.AddWithValue("@title", post.Title);
            command.Parameters.AddWithValue("@content", post.Content);
            command.Parameters.AddWithValue("@tags", post.Tags);
            command.Parameters.AddWithValue("@status", post.Status);
            command.Parameters.AddWithValue("@create_time", post.Create_time);
            command.Parameters.AddWithValue("@update_time", post.Update_time);

            command.Prepare();
            var result = command.ExecuteScalar();
            _connection.Close();

            return (int)result;
        }

        public int UpdatePost(Post post, int id)
        {
            var command = _connection.CreateCommand();
            command.CommandText = "UPDATE Post SET title = @title, content =@content, tags = @tags, status = @status, create_time = @create_time, update_time = @update_time WHERE id = @id";

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@title", post.Title);
            command.Parameters.AddWithValue("@content", post.Content);
            command.Parameters.AddWithValue("@tags", post.Tags);
            command.Parameters.AddWithValue("@status", post.Status);
            //command.Parameters.AddWithValue("@create_time", post.Create_time);
            //command.Parameters.AddWithValue("@update_time", post.Update_time);

            command.Prepare();
            var result = command.ExecuteScalar();
            _connection.Close();

            return (int)result;
        }

        public void DeletePost(int id)
        {
            var command = _connection.CreateCommand();

            command.CommandText = $"DELETE FROM Post WHERE id = {id}";

            var result = command.ExecuteNonQuery();
            _connection.Close();
        }
    }
   
}
