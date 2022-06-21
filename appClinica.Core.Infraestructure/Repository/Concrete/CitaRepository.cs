﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using appClinica.Core.Domain.Models;
using appClinica.Core.Infraestructure.Repository.Abstract;
using appClinica.Adapters.SQLServerDataAccess.Contexts;

namespace appClinica.Core.Infraestructure.Repository.Concrete
{
    public class CitaRepository : IBaseRepository<Cita, Guid>
    {

        private ClinicaDB db;

        public CitaRepository(ClinicaDB db)
        {
            this.db = db;
        }

        public Cita Create(Cita entity)
        {
            entity.citaId = Guid.NewGuid();
            db.Citas.Add(entity);
            return entity;
        }

        public void Delete(Guid entityId)
        {
            var citaSeleccionado = db.Citas
                .Where(c => c.citaId == entityId)
                .FirstOrDefault();

            if (citaSeleccionado != null)
            {
                db.Citas.Remove(citaSeleccionado);
            }
        }

        public List<Cita> GetAll()
        {
            return db.Citas.ToList();
        }

        public Cita GetById(Guid entityId)
        {
            var citaSeleccionado = db.Citas
                .Where(c => c.citaId == entityId)
                .FirstOrDefault();

            return citaSeleccionado;
        }

        public void SaveAllChanges()
        {
            db.SaveChanges();
        }

        public Cita Update(Cita entity)
        {
            var citaSeleccionado = db.Citas
                .Where(c => c.citaId == entity.citaId)
                .FirstOrDefault();

            if (citaSeleccionado != null)
            {
                citaSeleccionado.fechaRegistro = entity.fechaRegistro;
                citaSeleccionado.fechaVisita = entity.fechaVisita;
                citaSeleccionado.sintomas = entity.sintomas;
                citaSeleccionado.especialistaId = entity.especialistaId;
                citaSeleccionado.pacienteId = entity.pacienteId;
            }

            return citaSeleccionado;
        }
    }
}
