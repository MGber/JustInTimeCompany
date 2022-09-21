using JustInTimeCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace JustInTimeCompany.Data
{
    public class UserMessageSeed
    {
        private static readonly Guid User1Id = new("bb76dfd3-10d3-4224-b4b3-712aafa7ccaa");
        private static readonly Guid User2Id = new("51d08f0c-0c50-4e47-8e8b-55f7125019bc");
        private static readonly Guid User3Id = new("4b40d3d6-bd20-43f2-a5b8-082009c13682");

        public static readonly Guid Mess1 = new("59b2ecaa-b258-4e87-9d0c-33d87c17cfd9");
        public static readonly Guid Mess2 = new("2fba385c-5aac-4d98-8b2e-b3c3c23b6acc");
        public static readonly Guid Mess3 = new("2e27db70-be58-4c64-9c85-17320d79d1ce");
        public static readonly Guid Mess4 = new("d64d8d14-2e2f-4044-995e-3c28a66b1d0e");
        public static readonly Guid Mess5 = new("46314476-0cbd-4535-b257-1ab51b582ec2");
        public static readonly Guid Mess6 = new("d4812a5f-54dc-4ab6-8e0a-7018744f2722");

        public static void Seed(ModelBuilder modelBuilder)
        {
            var message1 = new { Id = Mess1, Message = "This is a testMessage", UserId = User1Id.ToString() };
            var message2 = new { Id = Mess2, Message = "This is a testMessage2", UserId = User2Id.ToString() };
            var message3 = new { Id = Mess3, Message = "This is a testMessage3", UserId = User3Id.ToString() };
            var message4 = new { Id = Mess4, Message = "This is a testMessage4", UserId = User1Id.ToString() };
            var message5 = new { Id = Mess5, Message = "This is a testMessage5", UserId = User2Id.ToString() };
            var message6 = new { Id = Mess6, Message = "This is a testMessage6", UserId = User3Id.ToString() };
            modelBuilder.Entity<UserMessage>()
                .HasData(message1, message2, message3, message4, message5, message6);
        }
    }
}