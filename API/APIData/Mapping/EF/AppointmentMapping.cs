using UniversalWeather.CMS.API.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalWeather.CMS.API.Data.Mapping.EF
{
    public class AppointmentMapping : EntityTypeConfiguration<Appointment>
    {
        public AppointmentMapping()
        {
            ToTable("Appointment");
            HasKey(k => k.AppointmentId);
            Property(p => p.Name).HasColumnName("NAME");
            Property(p => p.PhoneNumber).HasColumnName("PHONE_NUMBER");
            Property(p => p.Type).HasColumnName("TYPE");
            Property(p => p.Comment).HasColumnName("COMMENT");
            Property(p => p.XML).HasColumnName("XML");
            Property(p => p.RequestedDateTime).HasColumnName("REQUESTED_DATE_TIME");
            Property(p => p.AppointmentTaken).HasColumnName("APPOINTMENT_TAKEN");
            Property(p => p.ClaimedBy).HasColumnName("CLAIMED_BY");
            Property(p => p.IsCallNow).HasColumnName("IS_CALL_NOW");
            Property(p => p.IsPublished).HasColumnName("IS_PUBLISHED");
            Property(p => p.IsDeleted).HasColumnName("Is_DELETED");
            Property(p => p.DateCreated).HasColumnName("DATE_CREATED");
            Property(p => p.DateModified).HasColumnName("DATE_MODIFIED");

        }
    }
}
