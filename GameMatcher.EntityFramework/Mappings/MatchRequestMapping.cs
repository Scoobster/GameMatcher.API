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
    public class MatchRequestMapping: EntityTypeConfiguration<MatchRequest>
    {
        public MatchRequestMapping()
        {
            ToTable("MatchRequest").HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(x => x.HostPlayerId).HasColumnName("host_player_id").IsRequired();
            Property(x => x.MatchStartTime).HasColumnName("match_time").IsRequired();
            Property(x => x.LengthInMins).HasColumnName("length").IsRequired();
            Property(x => x.MinAbility).HasColumnName("ability_min").IsOptional();
            Property(x => x.MaxAbility).HasColumnName("ability_max").IsOptional();

            HasRequired(x => x.HostPlayer).WithMany(x => x.MatchRequests).HasForeignKey(x => x.HostPlayerId);
        }
    }
}
