using Gifter.Models;
using Gifter.Utils;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;

namespace Gifter.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

        public List<UserProfile> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, [Name], Bio, Email, DateCreated, ImageUrl
                        FROM UserProfile
                        ORDER BY up.DateCreated";

                    var reader = cmd.ExecuteReader();

                    var profiles = new List<UserProfile>();
                    while (reader.Read())
                    {
                        profiles.Add(new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Bio = DbUtils.GetString(reader, "Bio"),
                            Email = DbUtils.GetString(reader, "Email"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                        });
                    }

                    reader.Close();

                    return profiles;
                }
            }
        }

        public UserProfile GetByEmail(string email)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                          SELECT Id, Name, Email, ImageUrl, Bio, DateCreated FROM UserProfile WHERE Email = @email";
                    cmd.Parameters.AddWithValue("@email", email);

                    var reader = cmd.ExecuteReader();

                    UserProfile user = null;
                    if (reader.Read())
                    {
                        user = new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "iD"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "Email"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl")
                        };
                    }

                    reader.Close();

                    return user;
                }
            }
        }

        public UserProfile GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, [Name], Bio, Email, DateCreated, ImageUrl
                        FROM UserProfile
                        WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    UserProfile profile = null;
                    if (reader.Read())
                    {
                        profile = new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Bio = DbUtils.GetString(reader, "Bio"),
                            Email = DbUtils.GetString(reader, "Email"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                        };
                    }

                    reader.Close();

                    return profile;
                }
            }
        }

        public UserProfile GetByIdWithPosts(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT up.Id AS 'ProfileId', up.Name, up.Bio, up.Email, up.DateCreated, up.ImageUrl,

                               p.Id AS PostId, p.Title, p.Caption, p.DateCreated AS PostDateCreated, 
                               p.ImageUrl AS PostImageUrl, p.UserProfileId
                        FROM UserProfile up
                            LEFT JOIN Post p ON up.Id = p.UserProfileId
                        WHERE up.ID = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();

                    UserProfile profile = null;
                    while(reader.Read())
                    {
                        if(profile == null)
                        {
                            profile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "ProfileId"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                Posts = new List<Post>()
                            };
                        }

                        if (DbUtils.IsNotDbNull(reader, "PostId"))
                        {
                            profile.Posts.Add(new Post()
                            {
                                Id = DbUtils.GetInt(reader, "PostId"),
                                Title = DbUtils.GetString(reader, "Title"),
                                Caption = DbUtils.GetString(reader, "Caption"),
                                DateCreated = DbUtils.GetDateTime(reader, "PostDateCreated"),
                                ImageUrl = DbUtils.GetString(reader, "PostImageUrl"),
                                UserProfileId = DbUtils.GetInt(reader, "ProfileId"),
                            });
                        }
                    }
                    reader.Close();

                    return profile;
                }
            }
        }

        public void Add(UserProfile profile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO UserProfile ([Name], Bio, Email, DateCreated, ImageUrl)
                        OUTPUT INSERTED.ID
                        VALUES (@Name, @Bio, @Email, @DateCreated, @ImageUrl)";

                    DbUtils.AddParameter(cmd, "@Name", profile.Name);
                    DbUtils.AddParameter(cmd, "@Bio", profile.Bio);
                    DbUtils.AddParameter(cmd, "@Email", profile.Email);
                    DbUtils.AddParameter(cmd, "@DateCreated", DateTime.Now);
                    DbUtils.AddParameter(cmd, "@ImageUrl", profile.ImageUrl);

                    profile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(UserProfile profile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE UserProfile
                           SET Name = @Name,
                               Bio = @Bio,
                               Email = @Email,
                               DateCreated = @DateCreated,
                               ImageUrl = @ImageUrl
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Name", profile.Name);
                    DbUtils.AddParameter(cmd, "@Bio", profile.Bio);
                    DbUtils.AddParameter(cmd, "@Email", profile.Email);
                    DbUtils.AddParameter(cmd, "@DateCreated", profile.DateCreated);
                    DbUtils.AddParameter(cmd, "@ImageUrl", profile.ImageUrl);
                    DbUtils.AddParameter(cmd, "@Id", profile.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM UserProfile WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
