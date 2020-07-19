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
    public class ClubMapping: EntityTypeConfiguration<Club>
    {
        public ClubMapping()
        {
            ToTable("Club").HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
            Property(x => x.Name).HasColumnName("name").IsRequired();
            Property(x => x.NumOfCourts).HasColumnName("numOfCourts").IsRequired();
            Property(x => x.Sport).HasColumnName("sport").IsOptional();

            HasMany(x => x.Players).WithRequired(x => x.Club).HasForeignKey(x => x.ClubId);
        }
    }
}
