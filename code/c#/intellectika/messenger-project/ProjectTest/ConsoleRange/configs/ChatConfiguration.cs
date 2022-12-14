using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetworkingAuxiliaryLibrary.Objects.Entities;

namespace MyApp
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {

        public void Configure(EntityTypeBuilder<Chat> chatBuilder)
        {
            chatBuilder.HasKey(x => x.Id);

            chatBuilder.HasMany(c => c.UserList).WithMany(u => u.ChatList);
            chatBuilder.HasMany(c => c.MessageList).WithOne(m => m.Chat);
        }

    }
}
