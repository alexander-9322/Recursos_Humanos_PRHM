﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DB.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RHEntities : DbContext
    {
        public RHEntities()
            : base("name=RHEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<aprobacione> aprobaciones { get; set; }
        public virtual DbSet<calculo_aguinaldo> calculo_aguinaldo { get; set; }
        public virtual DbSet<canton> cantons { get; set; }
        public virtual DbSet<catalago_cedula> catalago_cedula { get; set; }
        public virtual DbSet<catalago_desempeño> catalago_desempeño { get; set; }
        public virtual DbSet<catalago_tipo_horas_extras> catalago_tipo_horas_extras { get; set; }
        public virtual DbSet<catalago_tipo_liquidacion> catalago_tipo_liquidacion { get; set; }
        public virtual DbSet<catalogo_control_permisos_colaborador> catalogo_control_permisos_colaborador { get; set; }
        public virtual DbSet<catalogo_tipo_incapacidades> catalogo_tipo_incapacidades { get; set; }
        public virtual DbSet<detalles_liquidacion> detalles_liquidacion { get; set; }
        public virtual DbSet<detalles_planilla> detalles_planilla { get; set; }
        public virtual DbSet<direccion> direccions { get; set; }
        public virtual DbSet<distrito> distritoes { get; set; }
        public virtual DbSet<empleado> empleados { get; set; }
        public virtual DbSet<evaluacion_remndimiento_colaborador> evaluacion_remndimiento_colaborador { get; set; }
        public virtual DbSet<horas_extras> horas_extras { get; set; }
        public virtual DbSet<incapacidade> incapacidades { get; set; }
        public virtual DbSet<liquidacion> liquidacions { get; set; }
        public virtual DbSet<persona> personas { get; set; }
        public virtual DbSet<planilla> planillas { get; set; }
        public virtual DbSet<provincia> provincias { get; set; }
        public virtual DbSet<vacacione> vacaciones { get; set; }
        public virtual DbSet<telefono> telefonoes { get; set; }
        public virtual DbSet<usuario> usuarios { get; set; }
        public virtual DbSet<control_de_permiso_colaborador> control_de_permiso_colaborador { get; set; }
        public virtual DbSet<catalago_deducciones> catalago_deducciones { get; set; }
    }
}
