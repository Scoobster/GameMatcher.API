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
    public class MatchConfirmedMapping : EntityTypeConfiguration<MatchConfirmed>
    {
        public MatchConfirmedMapping()
        {
            ToTable("MatchConfirmed").HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(x => x.HostPlayerId).HasColumnName("host_player_id").IsRequired();
            Property(x => x.GuestPlayerId).HasColumnName("guest_player_id").IsRequired();
            Property(x => x.MatchStartTime).HasColumnName("match_time").IsRequired();
            Property(x => x.LengthInMins).HasColumnName("length").IsRequired();

            HasRequired(x => x.HostPlayer).WithMany(x => x.ConfirmedMatchesAsHost).HasForeignKey(x => x.HostPlayerId);
            HasRequired(x => x.GuestPlayer).WithMany(x => x.ConfirmedMatchesAsGuest).HasForeignKey(x => x.GuestPlayerId);
        }
    }
}
