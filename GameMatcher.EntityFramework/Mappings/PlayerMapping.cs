using GameMatcher.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMatcher.EntityFramework.Mappings
{
    public class PlayerMapping: EntityTypeConfiguration<Player>
    {
        public PlayerMapping()
        {
            ToTable("Player").HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(x => x.FirstName).HasColumnName("first_name").IsRequired();
            Property(x => x.LastName).HasColumnName("last_name").IsRequired();
            Property(x => x.ClubId).HasColumnName("club_id").IsRequired();
            Property(x => x.Ability).HasColumnName("ability_level").IsRequired();
            Property(x => x.PhoneNumber).HasColumnName("phone_number").IsOptional();
            Property(x => x.DeviceToken).HasColumnName("device_token").IsOptional();

            HasRequired(x => x.Club).WithMany(x => x.Players).HasForeignKey(x => x.ClubId);
            HasMany(x => x.ConfirmedMatchesAsHost).WithRequired(x => x.HostPlayer).HasForeignKey(x => x.HostPlayerId);
            HasMany(x => x.ConfirmedMatchesAsGuest).WithRequired(x => x.GuestPlayer).HasForeignKey(x => x.GuestPlayerId);

            Ignore(x => x.ConfirmedMatches);
        }
    }
}
