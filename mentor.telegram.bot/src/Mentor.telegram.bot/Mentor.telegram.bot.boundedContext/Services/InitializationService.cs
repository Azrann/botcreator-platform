using System.Data.SqlClient;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Mentor.telegram.bot.boundedContext.Services
{
    public class InitializationService : IInitializacionService
    {
        private readonly string _connectionString;

        public InitializationService(MentorDbContext mentorDbContext)
        {
            _connectionString = mentorDbContext.Database.GetDbConnection().ConnectionString;
        }

        public void DatabaseEnsureCreated()
        {
        }

        public int InitializeStoreProcs()
        {
            
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                conn.Execute(@"
                    IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Add_Client')
                    DROP PROCEDURE Add_Client
                    ");
                conn.Execute(@"
                    CREATE PROCEDURE Add_Client 
	                        @UserId VARCHAR(35)
                            ,@Firstname  VARCHAR(25)
                            ,@Lastname  VARCHAR(25)
                            ,@Username  VARCHAR(25)
                            ,@ChatId Bigint
                            ,@AccessHash Bigint
                            ,@BotId nvarchar(max)
                            ,@UserEmail VARCHAR(35)

                    AS
                    BEGIN
	                    SET NOCOUNT ON;
                        IF EXISTS (SELECT * FROM [dbo].[Clients] WHERE [BotId] = @BotId AND [UserId] = @UserId)
	                        RETURN

                        IF NOT EXISTS (SELECT * FROM TelegramUser WHERE Id = @UserId)
                        BEGIN
                        INSERT INTO [dbo].[TelegramUser] ([CreatedOn],[LastUpdated],[Id],[AccessHash],[FirstName],[LastName],[UserName]) VALUES
                                   (getutcdate(),getutcdate(),@UserId,@AccessHash,@Firstname,@Lastname,@Username)
                        END
   
                        INSERT INTO [dbo].[Clients]
                                   ([Id]
                                   ,[CreatedOn]
                                   ,[LastUpdated]
                                   ,[UserId]
                                   ,[UserEmail]
                                   ,[ChatId]
                                   ,[BotId])
                             VALUES
                                   (NEWID()
                                   ,getutcdate()
                                   ,getutcdate()
                                   ,@UserId
                                   ,@UserEmail
                                   ,@ChatId
                                   ,@BotId)
                       END
                    ");

                conn.Execute(@"
                    IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Add_Mentor')
                    DROP PROCEDURE Add_Mentor
                    ");
                return conn.Execute(@"
                    CREATE PROCEDURE Add_Mentor 
	                        @UserId VARCHAR(35)
                            ,@Firstname  VARCHAR(25)
                            ,@Lastname  VARCHAR(25)
                            ,@Username  VARCHAR(25)
                            ,@ChatId Bigint
                            ,@AccessHash Bigint
                            ,@BotId nvarchar(max)
                            ,@UserEmail VARCHAR(35)

                    AS
                    BEGIN
	                    SET NOCOUNT ON;
                        IF EXISTS (SELECT * FROM [dbo].[Mentors] WHERE [BotId] = @BotId AND [UserId] = @UserId)
	                        RETURN

                        IF NOT EXISTS (SELECT * FROM TelegramUser WHERE Id = @UserId)
                        BEGIN
                        INSERT INTO [dbo].[TelegramUser] ([CreatedOn],[LastUpdated],[Id],[AccessHash],[FirstName],[LastName],[UserName]) VALUES
                                   (getutcdate(),getutcdate(),@UserId,@AccessHash,@Firstname,@Lastname,@Username)
                        END
   
                        INSERT INTO [dbo].[Mentors]
                                   ([Id]
                                   ,[CreatedOn]
                                   ,[LastUpdated]
                                   ,[UserId]
                                   ,[UserEmail]
                                   ,[ChatId]
                                   ,[BotId])
                             VALUES
                                   (NEWID()
                                   ,getutcdate()
                                   ,getutcdate()
                                   ,@UserId
                                   ,@UserEmail
                                   ,@ChatId
                                   ,@BotId)
                       END
                    ");
            }
        }
    }

    public interface IInitializacionService
    {
        int InitializeStoreProcs();
        void DatabaseEnsureCreated();
    }
}
